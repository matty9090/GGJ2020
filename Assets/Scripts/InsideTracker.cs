using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideTracker : MonoBehaviour
{
    private List<InsideTriggerTracker> mColliders;
    public bool IsInside { get; private set; }

    void Start()
    {
        mColliders = new List<InsideTriggerTracker>(GetComponentsInChildren<InsideTriggerTracker>());
    }

    void Update()
    {
        IsInside = false;

        foreach (var collider in mColliders)
        {
            if (collider.IsPlayerInside)
            {
                IsInside = true;
                break;
            }
        }
    }
}
