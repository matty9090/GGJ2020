using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Terminal : MonoBehaviour
{
    [SerializeField] List<Machine> Generators = null;
    [SerializeField] TMPro.TextMeshProUGUI FinTxt = null;
    [SerializeField] TMPro.TextMeshProUGUI CanvasRepair = null;

    void Start()
    {
        CanvasRepair.enabled = true;
        FinTxt.enabled = false;
    }

    void Update()
    {
        foreach (var gen in Generators)
        {
            if (gen.IsBroken)
            {
                return;
            }
        }

        CanvasRepair.enabled = false;
        FinTxt.enabled = true;

        var player = GameObject.Find("Character").transform.position;

        if (Input.GetKey(KeyCode.E) && Vector3.Distance(player, transform.position) < 10.0f)
        {
            SceneManager.LoadScene("EndGame");
        }
    }
}
