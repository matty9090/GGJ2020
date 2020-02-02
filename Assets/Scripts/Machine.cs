using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Machine : MonoBehaviour
{
    [SerializeField] float FixRadius = 10;
    [SerializeField] EResource ResourceType = EResource.Metal;
    [SerializeField] int ResourceCost = 10;
    [SerializeField] string RequiredTool = "Welder";
    [SerializeField] float TextHeightDifference = 5.0f;

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
            {
                StopParticles();
                CostText.enabled = false;
            }
        }
    }    

    // Start is called before the first frame update
    void Start()
    {
        PlayerChar = GameObject.Find("Character").gameObject;
        particlesSystems = GetComponentsInChildren<ParticleSystem>();
        game = GameObject.Find("Game").GetComponent<Game>();
        ButtonCanvas = transform.Find("ButtonCanvas").GetComponent<Canvas>();
        CostText.text += "\nMetal: " + ResourceCost.ToString() + "\nRequires tool: " + RequiredTool;
        CostText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsBroken && PlayerChar)
        {
            if (Vector3.Distance(PlayerChar.transform.position, transform.position) < FixRadius)
            {
                var screen = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * TextHeightDifference);
                CostText.transform.position = screen;
                CostText.enabled = true;
                if (game.Resources.GetRes(ResourceType) >= ResourceCost && game.CheckTool(RequiredTool))
                {
                    CostText.color = Color.green;

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        IsBroken = !IsBroken;
                        game.Resources.SubtractResources(ResourceType, ResourceCost);
                    }
                }
                else
                    CostText.color = Color.red;
            }
            else
                CostText.enabled = false;
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
