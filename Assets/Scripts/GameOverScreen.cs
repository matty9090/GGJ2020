using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class GameOverScreen : MonoBehaviour
{
    private void Start()
    {
        Vignette vig;
        var pp = Camera.main.GetComponent<PostProcessVolume>();
        pp.sharedProfile.TryGetSettings(out vig);
        vig.intensity.Override(0.0f);

        Cursor.lockState = CursorLockMode.None;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
