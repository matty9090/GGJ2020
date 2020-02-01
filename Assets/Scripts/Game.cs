using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    void Start()
    {
        SwitchToMainGame();
        StartCoroutine(DayNightCycle());

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

            Resources.SubtractFromMeter(EMeter.Water, 0.1f);

            yield return null;
        }
    }
}
