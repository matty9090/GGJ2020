using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolCheck : MonoBehaviour
{
    [SerializeField] float Radius = 10;
    [SerializeField] string RequiredTool = "Welder";
    [SerializeField] float TextHeightDifference = 5.0f;

    GameObject PlayerChar = null;
    Game game = null;

    //texts canvas'
    Canvas ButtonCanvas = null;
    [SerializeField] TMPro.TextMeshProUGUI CostText = null;


    // Start is called before the first frame update
    void Start()
    {
        PlayerChar = GameObject.Find("Character").gameObject;
        game = GameObject.Find("Game").GetComponent<Game>();
        ButtonCanvas = transform.Find("ButtonCanvas").GetComponent<Canvas>();
        CostText.text += "\nRequires tool: " + RequiredTool;
        CostText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerChar)
        {
            if (Vector3.Distance(PlayerChar.transform.position, transform.position) < Radius)
            {
                var screen = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * TextHeightDifference);
                CostText.transform.position = screen;
                CostText.enabled = true;
                if (game.CheckTool(RequiredTool))
                {
                    CostText.color = Color.green;

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Destroy(this.gameObject);
                    }
                }
                else
                    CostText.color = Color.red;
            }
            else
                CostText.enabled = false;
        }
    }

}
