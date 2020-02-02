using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialStory : MonoBehaviour
{
    [SerializeField] private List<TMPro.TextMeshProUGUI> Stories = null;
    [SerializeField] private int Current = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (Current < Stories.Count - 1)
            {
                ++Current;
                Stories[Current].enabled = true;
                Stories[Current - 1].enabled = false;
            }
            else
            {
                SceneManager.LoadScene("Game");
            }
        }

    }
}
