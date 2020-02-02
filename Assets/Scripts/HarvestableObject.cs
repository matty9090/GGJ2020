using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestableObject : MonoBehaviour
{
    [SerializeField] float HarvestRange = 10;
    [SerializeField] EResource ResourceType = EResource.Metal;
    [SerializeField] int ResourceAmount = 5;
    [SerializeField] ParticleSystem HarvestedEffect = null;
    [SerializeField] bool RequiresTool = false;
    [SerializeField] string Tool = "";

    GameObject PlayerChar = null;
    Canvas ButtonCanvas = null;

    bool IsHarvesting = false; 

    // Start is called before the first frame update
    void Start()
    {
        PlayerChar = GameObject.Find("Character");
        ButtonCanvas = transform.Find("ButtonCanvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsHarvesting && PlayerChar)
        {
            if(Vector3.Distance(PlayerChar.transform.position, transform.position) < HarvestRange)
            {
                ButtonCanvas.gameObject.SetActive(true);
                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    var game = GameObject.Find("Game").GetComponent<Game>();
                    bool canHarvest = true;

                    if (RequiresTool)
                    {
                        canHarvest = game.Tools.Contains(Tool);
                    }

                    if (canHarvest)
                    {
                        if (HarvestedEffect != null)
                        {
                            ParticleSystem explosionEffect = Instantiate(HarvestedEffect) as ParticleSystem;
                            explosionEffect.transform.position = transform.position;
                            explosionEffect.Play();
                            Destroy(explosionEffect.gameObject, explosionEffect.main.startLifetime.constant);
                        }

                        game.Resources.AddResources(ResourceType, ResourceAmount);
                        StartCoroutine(Shrink(2.0f));
                        IsHarvesting = true;
                    }
                }
            }
            else
                ButtonCanvas.gameObject.SetActive(false);
        }
        else
            ButtonCanvas.gameObject.SetActive(false);
    }

    IEnumerator Shrink(float scalingDuration)
    {
        GetComponent<AudioSource>().Play();
        Vector3 basePosition = transform.position;
        float scaleStep = 0;
        while (scaleStep < 0.5f) 
        {
            scaleStep += Time.deltaTime / scalingDuration;
            float newScale = 1 - scaleStep;
            transform.localScale = new Vector3(newScale, newScale, newScale);
            transform.position = new Vector3(basePosition.x, basePosition.y - (1 - transform.localScale.y), basePosition.z);

            yield return null;
        }
        Destroy(this.gameObject); 
    }

}
