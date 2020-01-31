using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestableObject : MonoBehaviour
{
    [SerializeField] float HarvestRange = 10;
    [SerializeField] EResource ResourceType = EResource.Dirt;
    [SerializeField] int ResourceAmount = 5;

    GameObject PlayerChar;
    Canvas ButtonCanvas;

    bool IsHarvesting = false; 

    // Start is called before the first frame update
    void Start()
    {
        PlayerChar = GameObject.Find("Character").gameObject;
        ButtonCanvas = transform.Find("ButtonCanvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsHarvesting)
        {
            if(Vector3.Distance(PlayerChar.transform.position, transform.position) < HarvestRange)
            {
                ButtonCanvas.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    GameObject.Find("Game").GetComponent<Game>().Resources.AddResources(ResourceType,ResourceAmount);
                    Destroy(this.gameObject, 2.0f); //change to animation time
                    StartCoroutine(Shrink(2.0f));
                    IsHarvesting = true;
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
        Vector3 basePosition = transform.position;
        float scaleStep = 0;
        while (true)
        {
            scaleStep += Time.deltaTime / scalingDuration;
            float newScale = 1 - scaleStep;
            transform.localScale = new Vector3(newScale, newScale, newScale);
            transform.position = new Vector3(basePosition.x, basePosition.y - (1 - transform.localScale.y), basePosition.z);

            yield return null;
        }
    }
}
