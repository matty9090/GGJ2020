using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private Gradient DayNightGradient;
    [SerializeField] private GameObject LightRotator;
    [SerializeField] private float DayNightSpeed;

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

            Debug.Log(DayNightT);

            yield return null;
        }
    }
}
