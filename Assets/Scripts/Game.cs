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
    [SerializeField] private InsideTracker Tracker = null;
    [SerializeField] private float DayNightSpeed = 0.04f;
    [SerializeField] private float OxygenReplenishSpeed = 0.1f;
    [SerializeField] private float OxygenDepletionSpeed = 0.05f;
    [SerializeField] private float VignetteStartPercent = 0.1f;
    
    public Resources Resources = null;
    public int DayNumber { get; private set; }
    public UnityEvent DayChangedEvent = new UnityEvent();

    private Vignette mVignette;
    private float DayNightT = 0.0f;

    private enum EState { MainGame, Overview };
    private EState mState = EState.MainGame;

    //list of tools picked up
    List<string> tools;

    void Start()
    {
        SwitchToMainGame();
        StartCoroutine(DayNightCycle());

        var pp = MainCamera.GetComponent<PostProcessVolume>();
        pp.sharedProfile.TryGetSettings(out mVignette);
        mVignette.enabled.Override(true);
        mVignette.intensity.Override(0.0f);
        mVignette.smoothness.Override(1.0f);
        mVignette.roundness.Override(1.0f);

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

        Resources.AddToMeter(EMeter.Oxygen, Tracker.IsInside ? OxygenReplenishSpeed : -OxygenDepletionSpeed);

        float vignettePercent = Resources.GetMeter(EMeter.Oxygen) / Resources.GetCap(EMeter.Oxygen);

        if (vignettePercent < VignetteStartPercent)
        {
            mVignette.intensity.Override(Mathf.Lerp(1.0f, 0.0f, vignettePercent / VignetteStartPercent));

            if (vignettePercent <= 0.001f)
            {
                SceneManager.LoadScene("GameOver");
            }
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

            yield return null;
        }
    }

    public void AddTool (string toolName)
    {
        tools.Add(toolName);
    }
}
