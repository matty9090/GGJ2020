using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomeLights : MonoBehaviour
{
    [SerializeField] private List<Light> Lights = null;
    [SerializeField] private float FlashSpeed = 1.0f;
    [SerializeField] private Color LightFixedCol = Color.white;
    [SerializeField] private Color LightBrokenCol = Color.red;

    bool mIsFixed = false;
    bool LightsOn = true;
    private float FlashTimer = 0.0f;

    public bool IsFixed {
        set {
            mIsFixed = value;

            foreach (var light in Lights)
            {
                light.enabled = mIsFixed;
                light.color = mIsFixed ? LightFixedCol : LightBrokenCol;
            }
        }
        get {
            return mIsFixed;
        }
    }

    void Update()
    {
        if (!IsFixed)
        {
            FlashTimer -= Time.deltaTime;

            if (FlashTimer < 0.0f)
            {
                FlashTimer = FlashSpeed;
                LightsOn = !LightsOn;

                foreach (var light in Lights)
                {
                    light.enabled = LightsOn;
                }
            }
        }
    }
}
