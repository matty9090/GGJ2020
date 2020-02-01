using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Resources Resources = null;
    [SerializeField] private Transform MeterWater = null;
    [SerializeField] private Transform MeterOxygen = null;
    [SerializeField] private TMPro.TextMeshProUGUI TxtSoil = null;
    [SerializeField] private TMPro.TextMeshProUGUI TxtDirt = null;
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
    }

    public void OnResourcesChanged()
    {
        TxtSoil.text = "" + Resources.GetRes(EResource.Soil);
        TxtDirt.text = "" + Resources.GetRes(EResource.Dirt);
        TxtMetal.text = "" + Resources.GetRes(EResource.Metal);
    }
}
