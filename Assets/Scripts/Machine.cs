using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Machine : MonoBehaviour
{
    [SerializeField] float FixRadius = 10;
    [SerializeField] EResource ResourceType = EResource.Metal;
    [SerializeField] int ResourceCost = 10;


    private bool _IsBroken = true;

    GameObject PlayerChar = null;
    ParticleSystem[] particlesSystems = null;
    Game game = null;

    //texts canvas'
    Canvas ButtonCanvas = null;
    [SerializeField] TMPro.TextMeshProUGUI CostText = null;


    public bool IsBroken
    {
        get { return _IsBroken; }
        set 
        { 
            _IsBroken = value;
            if (_IsBroken)
                StartParticles();
            else
                StopParticles();
        }
    }

    

    // Start is called before the first frame update
    void Start()
    {
        PlayerChar = GameObject.Find("Character").gameObject;
        particlesSystems = GetComponentsInChildren<ParticleSystem>();
        game = GameObject.Find("Game").GetComponent<Game>();
        ButtonCanvas = transform.Find("ButtonCanvas").GetComponent<Canvas>();
        CostText.text = "Metal: " + ResourceCost.ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(IsBroken)
        {
            if (Vector3.Distance(PlayerChar.transform.position, transform.position) < FixRadius)
            {
                ButtonCanvas.gameObject.SetActive(true);
                if(game.Resources.GetRes(ResourceType) > ResourceCost)
                {
                    CostText.color = Color.green;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        IsBroken = !IsBroken;
                    }
                }
                else
                    CostText.color = Color.red;
            }
            else
                ButtonCanvas.gameObject.SetActive(false);
        }  
    }

    void StartParticles()
    {
        foreach(ParticleSystem system in particlesSystems)
        {
            system.Play();
        }
    }

    void StopParticles()
    {
        foreach (ParticleSystem system in particlesSystems)
        {
            system.Stop();
        }
    }
}
