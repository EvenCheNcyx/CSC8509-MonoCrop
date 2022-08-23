using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI DateText;
    public TextMeshProUGUI SeasonText;
    public TextMeshProUGUI TimeText;
    
    //private List<GameObject> 

    private void OnEnable()
    {
        EventHandler.GameMinuteEvent += OnGameMinuteEvent;
        EventHandler.GameDateEvent += OnGameDateEvent;
    }

    private void OnDisable()
    {
        EventHandler.GameMinuteEvent -= OnGameMinuteEvent;
        EventHandler.GameDateEvent -= OnGameDateEvent;
    }
    
    private void OnGameMinuteEvent(int minute, int hour)
    {
        TimeText.text = hour.ToString("00") + ":" + minute.ToString("00");
    }
    
    private void OnGameDateEvent(int hour, int day, int month, int year, Season season)
    {
        DateText.text = year + "." + month.ToString("00");
        SeasonText.text = season.ToString();
        //Debug.Log(DateText.text + "M"+ SeasonText.text);
    }

   
    
}
