using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EResource
{
    Soil,
    Dirt,
    Metal,
    _MaxResources
}

[CreateAssetMenu(fileName = "Resources", menuName = "ScriptableObjects/Resources", order = 1)]
public class Resources : ScriptableObject
{
    private Dictionary<EResource, int> mResources;
    public UnityEvent ResourcesChangedEvent = new UnityEvent();

    Resources()
    {
        mResources = new Dictionary<EResource, int>();

        for (int i = 0; i < (int)EResource._MaxResources; ++i)
        {
            mResources.Add((EResource)i, 0);
        }
    }

    public void AddResources(EResource res, int amount)
    {
        mResources[res] += amount;
        ResourcesChangedEvent.Invoke();
    }

    public void SubtractResources(EResource res, int amount)
    {
        mResources[res] -= amount;
        ResourcesChangedEvent.Invoke();
    }
}
