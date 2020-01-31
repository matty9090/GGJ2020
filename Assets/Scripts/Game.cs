using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Camera MainCamera = null;
    [SerializeField] private Light DirectionalLight = null;
    [SerializeField] private Gradient DayNightGradient = null;
    [SerializeField] private GameObject LightRotator = null;
    [SerializeField] private Resources Resources = null;
    [SerializeField] private float DayNightSpeed = 0.04f;

    private float DayNightT = 0.0f;

    void Start()
    {
        StartCoroutine(DayNightCycle());
    }

    private IEnumerator DayNightCycle()
    {
        while (true)
        {
            Color col = DayNightGradient.Evaluate(DayNightT);
            RenderSettings.ambientLight = col;
            DirectionalLight.color = col;
            MainCamera.backgroundColor = col;
            DayNightT += DayNightSpeed * Time.deltaTime;
            LightRotator.transform.Rotate(DayNightSpeed * 360.0f * Time.deltaTime, 0.0f, 0.0f);

            if (DayNightT > 1.0f)
                DayNightT = 0.0f;

            Resources.SubtractFromMeter(EMeter.Water, 0.1f);

            yield return null;
        }
    }
}
