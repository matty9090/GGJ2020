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

public enum EMeter
{
    Water,
    Oxygen,
    _MaxMeters
}

[CreateAssetMenu(fileName = "Resources", menuName = "ScriptableObjects/Resources", order = 1)]
public class Resources : ScriptableObject
{
    public float MaxWater = 1000.0f;
    public float MaxOxygen = 100.0f;

    private Dictionary<EMeter, float> mMeters;
    private Dictionary<EResource, int> mResources;

    public UnityEvent MeterChangedEvent = new UnityEvent();
    public UnityEvent ResourcesChangedEvent = new UnityEvent();

    Resources()
    {
        mMeters = new Dictionary<EMeter, float>();
        mResources = new Dictionary<EResource, int>();

        mMeters.Add(EMeter.Water, MaxWater);
        mMeters.Add(EMeter.Oxygen, MaxOxygen);

        for (int i = 0; i < (int)EResource._MaxResources; ++i)
        {
            mResources.Add((EResource)i, 0);
        }
    }

    public int GetRes(EResource res)
    {
        return mResources[res];
    }

    public float GetMeter(EMeter meter)
    {
        return mMeters[meter];
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

    public void AddToMeter(EMeter meter, float amount)
    {
        mMeters[meter] += amount;
        MeterChangedEvent.Invoke();
    }

    public void SubtractFromMeter(EMeter meter, float amount)
    {
        mMeters[meter] -= amount;
        MeterChangedEvent.Invoke();
    }
}
