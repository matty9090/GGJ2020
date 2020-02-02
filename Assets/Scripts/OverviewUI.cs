using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverviewUI : MonoBehaviour
{
    [SerializeField] private Game Game = null;
    [SerializeField] private TMPro.TextMeshProUGUI TxtDay = null;
    [SerializeField] private TMPro.TextMeshProUGUI TxtTools = null;
    [SerializeField] private List<TMPro.TextMeshProUGUI> TxtGoals = null;

    void Start()
    {
        Game.DayChangedEvent.AddListener(OnDayChanged);
        Game.ToolAquiredEvent.AddListener(OnToolAquired);
        Game.DomeFinishedEvent.AddListener(OnDomeFinished);
    }

    void OnDayChanged()
    {
        TxtDay.text = "Day: " + Game.DayNumber;
    }

    void OnToolAquired()
    {
        TxtTools.text = "";

        foreach (var tool in Game.Tools)
        {
            TxtTools.text += tool + "\n";
        }
    }

    void OnDomeFinished()
    {
        TxtGoals[Game.DomesFinished - 1].color = Color.green;
    }
}
