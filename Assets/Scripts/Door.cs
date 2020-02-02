using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI Text = null;
    [SerializeField] private float DoorSpeed = 20.0f;
    [SerializeField] private float DoorHeight = 6.0f;
    [SerializeField] private float TipRange = 10.0f;
    [SerializeField] EResource ResourceType = EResource.Metal;
    [SerializeField] int ResourceCost = 10;
    [SerializeField] string RequiredTool = "Welder";

    bool HasExited = false, IsFixed = false;
    private Vector3 RestPos = Vector3.zero;
    GameObject PlayerChar = null;
    Game mGame;

    private void Start()
    {
        RestPos = transform.position;
        PlayerChar = GameObject.Find("Character");
        var tmpGame = GameObject.Find("Game");
        if (tmpGame)
            mGame = tmpGame.GetComponent<Game>();
        Text.text += "\nMetal: " + ResourceCost.ToString() + "\nRequires tool: " + RequiredTool;
        Text.enabled = false;
    }

    private void Update()
    {
        if (PlayerChar && Vector3.Distance(PlayerChar.transform.position, transform.position) < TipRange && !IsFixed)
        {
            var screen = Camera.main.WorldToScreenPoint(transform.position);
            Text.transform.position = screen;
            Text.enabled = true;

            if (mGame.Resources.GetRes(ResourceType) >= ResourceCost && mGame.CheckTool(RequiredTool))
            {
                Text.color = Color.green;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    IsFixed = true;
                    mGame.Resources.SubtractResources(ResourceType, ResourceCost);
                    StartCoroutine(OpenDoor());
                }
            }
            else
                Text.color = Color.red;
        }
        else
            Text.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        HasExited = false;

        if (IsFixed)
        {
            StartCoroutine(OpenDoor());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        HasExited = true;

        if (IsFixed)
        {
            StartCoroutine(CloseDoor());
        }
    }

    private IEnumerator OpenDoor()
    {
        while (transform.position.y > RestPos.y - DoorHeight && !HasExited)
        {
            transform.position -= Vector3.up * DoorSpeed * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator CloseDoor()
    {
        while (transform.position.y < RestPos.y && HasExited)
        {
            transform.position += Vector3.up * DoorSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
