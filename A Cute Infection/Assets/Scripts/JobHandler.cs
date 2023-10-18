using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JobHandler : MonoBehaviour
{
    public TMP_Text populationText;
    public TMP_Text explorerText;
    public TMP_Text scavengerText;
    public TMP_Text farmerText;
    public TMP_Text pumperText;

    public static double idle = 10;
    public static double survivors = 10;

    public static double explorers;
    public static double scavengers;
    public static double farmers;
    public static double pumpers;

    public void Start()
    {
        populationText.text = "Pop: " + idle + " / " + survivors;
        
        explorerText.text = explorers + " exploring";
        scavengerText.text = scavengers + " scavenging";
        farmerText.text = farmers + " farming";
        pumperText.text = pumpers + " pumping";
    }

    public void AddExplorer()
    {
        if(idle > 0)
        {
            idle -= 1;
            explorers += 1;

            populationText.text = "Pop: " + idle + " / " + survivors;
            explorerText.text = explorers + " exploring";
        }
    }

    public void SubtractExplorer()
    {
        if(explorers > 0)
        {
            idle += 1;
            explorers -= 1;

            populationText.text = "Pop: " + idle + " / " + survivors;
            explorerText.text = explorers + " exploring";
        }
    }

    public void AddScavenger()
    {
        if(idle > 0)
        {
            idle -= 1;
            scavengers += 1;

            populationText.text = "Pop: " + idle + " / " + survivors;
            scavengerText.text = scavengers + " scavenging";
        }
    }

    public void SubtractScavenger()
    {
        if(scavengers > 0)
        {
            idle += 1;
            scavengers -= 1;
            
            populationText.text = "Pop: " + idle + " / " + survivors;
            scavengerText.text = scavengers + " scavenging";
        }
    }

    public void AddFarmer()
    {
        if(idle > 0 && farmers < ClickerHandler.farms)
        {
            idle -= 1;
            farmers += 1;

            populationText.text = "Pop: " + idle + " / " + survivors;
            farmerText.text = farmers + " farming";
        }
    }

    public void SubtractFarmer()
    {
        if(farmers > 0)
        {
            idle += 1;
            farmers -= 1;
            
            populationText.text = "Pop: " + idle + " / " + survivors;
            farmerText.text = farmers + " farming";
        }
    }

    public void AddPumper()
    {
        if(idle > 0 && pumpers < ClickerHandler.pumps)
        {
            idle -= 1;
            pumpers += 1;

            populationText.text = "Pop: " + idle + " / " + survivors;
            pumperText.text = pumpers + " pumping";
        }
    }

    public void SubtractPumper()
    {
        if(pumpers > 0)
        {
            idle += 1;
            pumpers -= 1;
            
            populationText.text = "Pop: " + idle + " / " + survivors;
            pumperText.text = pumpers + " pumping";
        }
    }
}