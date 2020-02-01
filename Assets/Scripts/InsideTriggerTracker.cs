using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideTriggerTracker : MonoBehaviour
{
    public bool IsPlayerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        IsPlayerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        IsPlayerInside = false;
    }
}
