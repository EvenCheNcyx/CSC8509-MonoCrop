using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeManager : MonoBehaviour
{
    private int gameSecond, gameMinute, gameHour, gameDay, gameMonth, gameYear;
    private Season gameSeason = Season.Spring;

    private int monthInSeason = 2;
    public bool gameClockPause;
    private float tikTime;

    private void Awake()
    {
        NewGameTime();
    }

    private void Start()
    {
        Time.timeScale = 0;
        EventHandler.CallGameDateEvent(gameHour,gameDay,gameMonth,gameYear,gameSeason);
        EventHandler.CallGameMinuteEvent(gameMinute,gameHour);
    }

    private void Update()
    {
        
        if (!gameClockPause)
        {
            tikTime += Time.deltaTime;
            if (tikTime >= Settings.secondThershold)
            {
                tikTime -= Settings.secondThershold;
                UpdateGameTime();
            }
        }
        
        //Push T to accelerate 1month
        if(Input.GetKeyDown(KeyCode.T))
        {
            JumpToNextMonth();
        }
    }

    private void JumpToNextMonth()
    {
        gameSecond = 0;
        gameMinute = 0;
        gameHour = 8;
        gameDay++;
        gameHour = 8;
        if(gameDay>Settings.dayHold)
        {
            gameDay = 1;
            gameMonth++;

            if (gameMonth > 12)
            {
                gameMonth = 1;
                gameYear++;
            }
                            
            //monthInSeason--;

            if (monthInSeason == 0)
            {
                monthInSeason = 3;

                int seasonNumber = (int)gameSeason;
                seasonNumber++;

                if (seasonNumber > Settings.seasonHold)
                {
                    seasonNumber = 0;
                    //gameYear++;
                }

                gameSeason = (Season)seasonNumber;

                if (gameYear > 9999)
                {
                    gameYear = 2022;
                }
            }
            monthInSeason--;
                        
        }
                    
        EventHandler.CallGameDateEvent(gameHour,gameDay,gameMonth,gameYear,gameSeason);
        
    }
    
    private void NewGameTime()
    {
        gameSecond = 0;
        gameMinute = 0;
        gameHour = 8;
        gameDay = 1;
        gameMonth = 3;
        gameYear = 2022;
        gameSeason = Season.Spring;
    }
    
    
    private void UpdateGameTime()
    {
        gameSecond++;
        
        if (gameSecond > Settings.secondHold)
        {
            gameMinute++;
            gameSecond = 0;
            
            if (gameMinute > Settings.minuteHold)
            {
                gameHour++;
                gameMinute = 0;

                if (gameHour > Settings.hourHold)
                {
                    gameDay++;
                    gameHour = 8;
                    
                    if(gameDay>Settings.dayHold)
                    {
                        gameDay = 1;
                        gameMonth++;

                        if (gameMonth > 12)
                        {
                            gameMonth = 1;
                            gameYear++;
                        }
                            
                        //monthInSeason--;

                        if (monthInSeason == 0)
                        {
                            monthInSeason = 3;

                            int seasonNumber = (int)gameSeason;
                            seasonNumber++;

                            if (seasonNumber > Settings.seasonHold)
                            {
                                seasonNumber = 0;
                                //gameYear++;
                            }

                            gameSeason = (Season)seasonNumber;

                            if (gameYear > 9999)
                            {
                                gameYear = 2022;
                            }
                        }
                        monthInSeason--;
                        
                    }
                    
                    EventHandler.CallGameDateEvent(gameHour,gameDay,gameMonth,gameYear,gameSeason);
                }
            }
            
            EventHandler.CallGameMinuteEvent(gameMinute,gameHour);
        }

        //Debug.Log("Second:"+gameSecond+"Minute:"+gameMinute+"Hour:"+gameHour+"Day:"+gameDay);
    }
    
}
