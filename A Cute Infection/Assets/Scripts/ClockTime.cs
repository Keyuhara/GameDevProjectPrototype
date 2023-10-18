using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClockTime : MonoBehaviour
{
    public TMP_Text overheadText;
    
    public TMP_Text clockText;
    public TMP_Text dayText;

    public static float clock;
    public static int day = 1;
    public static int endDay = 10;
    public static float speed = 1f;

    public void Start()
    {
        // DisplayTime(0);
        dayText.text = "DAY " + day + " / " + endDay;
    }

    public void Update()
    {
        clock += Time.deltaTime * speed;
        DisplayTime(clock);
    }

    public void DisplayTime(float time)
    {
        if(time >= 1440)
        {
            MapHandler.zombies += 25;
            UpdateDay();
            CheckVictory();
        }
        float hours = Mathf.FloorToInt(time / 60);
        float minutes = Mathf.FloorToInt(time % 60);
        clockText.text = string.Format("{0:00} : {1:00}", hours, minutes);
    }

    public void UpdateDay()
    {
        clock = 0;
        day += 1;
        dayText.text = "DAY " + day + " / " + endDay;
    }

    public void CheckVictory()
    {
        if(day > endDay)
        {
            overheadText.text = "VICTORY!";
        }
    }

    public void UpdateSpeed1x()
    {
        speed = 1;
    }

    public void UpdateSpeed5x()
    {
        speed = 5;
    }

    public void UpdateSpeed10x()
    {
        speed = 10;
    }

    public void UpdateSpeed50x()
    {
        speed = 50;
    }
}
