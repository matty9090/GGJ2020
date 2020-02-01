using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverviewUI : MonoBehaviour
{
    [SerializeField] private Game Game = null;
    [SerializeField] private TMPro.TextMeshProUGUI TxtDay = null;

    void Start()
    {
        Game.DayChangedEvent.AddListener(OnDayChanged);
    }

    void OnDayChanged()
    {
        TxtDay.text = "Day: " + Game.DayNumber;
    }
}
