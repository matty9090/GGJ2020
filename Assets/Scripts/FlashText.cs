using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashText : MonoBehaviour
{
    [SerializeField] float Blinktime = 1;
    TMPro.TextMeshProUGUI BlinkingText = null;
    float timer = 0;
    bool IsOff = false;
    // Start is called before the first frame update
    void Start()
    {
        BlinkingText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= Blinktime)
        {
            IsOff = !IsOff;
            BlinkingText.enabled = IsOff;
            timer = 0;
        }
        
    }
}
