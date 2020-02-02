﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomeTriggerTracker : InsideTriggerTracker
{
    [SerializeField] private List<Machine> Machines = null;
    [SerializeField] private DomeLights Lights = null;

    public bool AreMachinesFixed = false;

    private void OnTriggerStay(Collider other)
    {
        if (!Machines[0].IsBroken)
        {
            Lights.ShouldFlash = true;
        }

        foreach (var machine in Machines)
        {
            if (machine.IsBroken)
            {
                return;
            }
        }

        AreMachinesFixed = true;
        IsPlayerInside = true;
        Lights.IsFixed = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var machine in Machines)
        {
            if (machine.IsBroken)
            {
                return;
            }
        }

        AreMachinesFixed = true;
        IsPlayerInside = true;
        Lights.IsFixed = true;
    }

    private void OnTriggerExit(Collider other)
    {
        IsPlayerInside = false;
    }
}
