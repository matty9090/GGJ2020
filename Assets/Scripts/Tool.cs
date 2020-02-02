using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tool : MonoBehaviour
{
    [SerializeField] float PickUp = 10;
    [SerializeField] private string ToolName = "Shovel";
    GameObject PlayerChar = null;
    Canvas ButtonCanvas = null;
    Game game = null;

    void Start()
    {
        PlayerChar = GameObject.Find("Character").gameObject;
        ButtonCanvas = transform.Find("ButtonCanvas").GetComponent<Canvas>();
        game = GameObject.Find("Game").GetComponent<Game>();

        ButtonCanvas.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "E to pick up " + ToolName;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(PlayerChar.transform.position, transform.position) < PickUp)
        {
            ButtonCanvas.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                game.AddTool(ToolName);
                game.ToolAquiredEvent.Invoke();
                Destroy(this.gameObject);
            }
        }
        else
            ButtonCanvas.gameObject.SetActive(false);
    }

    
}
