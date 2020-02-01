using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float DoorSpeed = 20.0f;
    [SerializeField] private float DoorHeight = 6.0f;

    bool HasExited = false;
    private Vector3 RestPos = Vector3.zero;

    private void Start()
    {
        RestPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        HasExited = false;
        StartCoroutine(OpenDoor());
    }

    private void OnTriggerExit(Collider other)
    {
        HasExited = true;
        StartCoroutine(CloseDoor());
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
