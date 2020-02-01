using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class Game : MonoBehaviour
{
    [SerializeField] private Camera MainCamera = null;
    [SerializeField] private Camera OverviewCamera = null;
    [SerializeField] private Canvas GameUI = null;
    [SerializeField] private Canvas OverviewUI = null;
    [SerializeField] private Light DirectionalLight = null;
    [SerializeField] private Gradient DayNightGradient = null;
    [SerializeField] private GameObject LightRotator = null;
    [SerializeField] private float DayNightSpeed = 0.04f;
    
    public Resources Resources = null;
    public int DayNumber { get; private set; }
    public UnityEvent DayChangedEvent = new UnityEvent();

    private float DayNightT = 0.0f;

    private enum EState { MainGame, Overview };
    private EState mState = EState.MainGame;

    //list of tools picked up
    List<string> tools;

    void Start()
    {
        SwitchToMainGame();
        StartCoroutine(DayNightCycle());

        Vignette vig;
        var pp = MainCamera.GetComponent<PostProcessVolume>();
        pp.sharedProfile.TryGetSettings(out vig);
        vig.intensity.Override(0.0f);
        vig.smoothness.Override(1.0f);
        vig.roundness.Override(1.0f);

        tools = new List<string>();
        DayNumber = 1;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            if (mState == EState.MainGame)
                SwitchToOverview();
            else
                SwitchToMainGame();
        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            StartCoroutine(GameOverSequence());
        }
    }

    private void SwitchToMainGame()
    {
        mState = EState.MainGame;
        GameUI.enabled = true;
        OverviewUI.enabled = false;
        MainCamera.enabled = true;
        OverviewCamera.enabled = false;
    }

    private void SwitchToOverview()
    {
        mState = EState.Overview;
        GameUI.enabled = false;
        OverviewUI.enabled = true;
        MainCamera.enabled = false;
        OverviewCamera.enabled = true;
    }

    private IEnumerator GameOverSequence()
    {
        Vignette vig;

        var pp = MainCamera.GetComponent<PostProcessVolume>();
        pp.sharedProfile.TryGetSettings(out vig);
        vig.enabled.Override(true);

        while (vig.intensity < 1.0f)
        {
            vig.intensity.Override(vig.intensity.value + Time.deltaTime * 0.1f);
            yield return null;
        }

        SceneManager.LoadScene("GameOver");
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
            {
                ++DayNumber;
                DayChangedEvent.Invoke();
                DayNightT = 0.0f;
            }

            Resources.SubtractFromMeter(EMeter.Water, 0.1f);

            yield return null;
        }
    }

    public void AddTool (string toolName)
    {
        tools.Add(toolName);
    }
}
