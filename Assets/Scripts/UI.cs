﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Resources Resources = null;
    [SerializeField] private Transform MeterWater = null;
    [SerializeField] private Transform MeterOxygen = null;
    [SerializeField] private Gradient OxygenGradient = null;
    [SerializeField] private TMPro.TextMeshProUGUI TxtWood = null;
    [SerializeField] private TMPro.TextMeshProUGUI TxtMetal = null;

    private void Start()
    {
        Resources.MeterChangedEvent.AddListener(OnMeterChanged);
        Resources.ResourcesChangedEvent.AddListener(OnResourcesChanged);
    }

    public void OnMeterChanged()
    {
        float scale = Mathf.Clamp01(Resources.GetMeter(EMeter.Water) / Resources.MaxWater);
        MeterWater.localScale = new Vector3(MeterWater.localScale.x, scale, MeterWater.localScale.z);

        scale = Mathf.Clamp01(Resources.GetMeter(EMeter.Oxygen) / Resources.MaxOxygen);
        MeterOxygen.localScale = new Vector3(MeterOxygen.localScale.x, scale, MeterOxygen.localScale.z);
        MeterOxygen.GetComponent<Image>().color = OxygenGradient.Evaluate(1.0f - scale);
    }

    public void OnResourcesChanged()
    {
        TxtWood.text = "" + Resources.GetRes(EResource.Wood);
        TxtMetal.text = "" + Resources.GetRes(EResource.Metal);
    }
}
