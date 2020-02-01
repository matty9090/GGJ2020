using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelTriggerTracker : InsideTriggerTracker
{
    [SerializeField] private List<DomeTriggerTracker> Domes = null;

    private void OnTriggerStay(Collider other)
    {
        foreach (var dome in Domes)
        {
            if (!dome.AreMachinesFixed)
            {
                return;
            }
        }

        IsPlayerInside = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var dome in Domes)
        {
            if (!dome.AreMachinesFixed)
            {
                return;
            }
        }

        IsPlayerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        IsPlayerInside = false;
    }
}
