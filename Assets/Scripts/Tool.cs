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
    bool pickedUp = false;
    Game game = null;

    void Start()
    {
        PlayerChar = GameObject.Find("Character");
        ButtonCanvas = transform.Find("ButtonCanvas").GetComponent<Canvas>();

        var tmpGame = GameObject.Find("Game");
        if(tmpGame )
            game = tmpGame.GetComponent<Game>();

        ButtonCanvas.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "E to pick up " + ToolName;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerChar && !pickedUp)
        {
            if (Vector3.Distance(PlayerChar.transform.position, transform.position) < PickUp)
            {
                ButtonCanvas.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    game.AddTool(ToolName);
                    game.ToolAquiredEvent.Invoke();
                    StartCoroutine(Shrink(2.0f));
                    pickedUp = true;
                }
            }
            else
                ButtonCanvas.gameObject.SetActive(false);
        }
    }

    IEnumerator Shrink(float scalingDuration)
    {
        ButtonCanvas.gameObject.SetActive(false);
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
