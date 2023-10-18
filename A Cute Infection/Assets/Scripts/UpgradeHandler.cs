using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeHandler : MonoBehaviour
{
    public TMP_Text overheadText;

    public TMP_Text clockText;
    public TMP_Text dayText;

    public TMP_Text foodText;
    public TMP_Text waterText;
    public TMP_Text scrapsText;
    public TMP_Text populationText;
    public TMP_Text healthText;
    public TMP_Text staminaText;
    public TMP_Text gunText;
    public TMP_Text ammoText;
    public TMP_Text shivText;
    public TMP_Text defenseText;

    public TMP_Text exploreLevelText;
    public TMP_Text scavengeLevelText;
    public TMP_Text farmLevelText;
    public TMP_Text pumpLevelText;

    public static double exploreRate = 1;
    public static double scavengeRate = 1;
    public static double farmRate = 1;
    public static double pumpRate = 1;

    public float rand;

    public void Start()
    {
        dayText.text = "DAY " + ClockTime.day + " / " + ClockTime.endDay;

        foodText.text = "Food: " + ClickerHandler.food.ToString("F0");
        waterText.text = "Water: " + ClickerHandler.water.ToString("F0");
        scrapsText.text = "Scraps: " + ClickerHandler.scraps.ToString("F0");

        populationText.text = "Pop: " + JobHandler.idle + " / " + JobHandler.survivors;
        
        healthText.text = "HP: " + ClickerHandler.HP.ToString("F0") + " / " + ClickerHandler.maxHP.ToString("F0");
        staminaText.text = "SP: " + ClickerHandler.SP.ToString("F0") + " / " + ClickerHandler.maxSP.ToString("F0");
        gunText.text = "Gun: " + ClickerHandler.gun.ToString("F0");
        ammoText.text = "Ammo: " + ClickerHandler.ammo.ToString("F0");
        shivText.text = "Shiv: " + ClickerHandler.shiv.ToString("F0");
        defenseText.text = "DEF: " + ClickerHandler.defense.ToString("F0");
        
        exploreLevelText.text = "Level " + exploreRate;
        scavengeLevelText.text = "Level " + scavengeRate;
        farmLevelText.text = "Level " + farmRate;
        pumpLevelText.text = "Level " + pumpRate;
    }

    public void Update()
    {
        ClockTime.clock += Time.deltaTime * ClockTime.speed;
        DisplayTime(ClockTime.clock);

        if(JobHandler.explorers > 0 && ClickerHandler.explored < ClickerHandler.notExplored)
        {
            ClickerHandler.explored += exploreRate * JobHandler.explorers * Time.deltaTime * ClockTime.speed;
            ClickerHandler.notSearched += 10 * exploreRate * JobHandler.explorers * Time.deltaTime * ClockTime.speed;

            if(ClickerHandler.explored > ClickerHandler.notExplored)
            {
                ClickerHandler.explored = ClickerHandler.notExplored;
            }
        }

        if(JobHandler.scavengers > 0 && ClickerHandler.searched < ClickerHandler.notSearched)
        {
            ClickerHandler.lootFlag += scavengeRate * JobHandler.scavengers * Time.deltaTime * ClockTime.speed;
            ClickerHandler.searched += scavengeRate * JobHandler.scavengers * Time.deltaTime * ClockTime.speed;

            while(ClickerHandler.lootFlag > 1)
            {
                Loot();
                ClickerHandler.lootFlag -= 1;
            }

            if(ClickerHandler.searched > ClickerHandler.notSearched)
            {
                ClickerHandler.searched = ClickerHandler.notSearched;
            }
        }

        if(JobHandler.farmers > 0)
        {
            ClickerHandler.food += farmRate * JobHandler.farmers * Time.deltaTime * ClockTime.speed;
            
            foodText.text = "Food: " + ClickerHandler.food.ToString("F0");
        }

        if(JobHandler.pumpers > 0)
        {
            ClickerHandler.water += pumpRate * JobHandler.pumpers * Time.deltaTime * ClockTime.speed;
            
            waterText.text = "Water: " + ClickerHandler.water.ToString("F0");
        }
    }

    public void Loot()
    {
        rand = Random.Range(0, 100);
        switch(rand)
        {
            case float i when i > 95:
                ClickerHandler.food += 1;
                foodText.text = "Food: " + ClickerHandler.food.ToString("F0");
                break;
            case float i when i > 90:
                ClickerHandler.water += 1;
                waterText.text = "Water: " + ClickerHandler.water.ToString("F0");
                break;
            default:
                ClickerHandler.scraps += 1;
                scrapsText.text = "Scraps: " + ClickerHandler.scraps.ToString("F0");
                break;
        }
    }

    public void BoostExploration()
    {
        if(ClickerHandler.food >= 10 && ClickerHandler.water >= 10)
        {
            ClickerHandler.food -= 10;
            foodText.text = "Food: " + ClickerHandler.food.ToString("F0");
            ClickerHandler.water -= 10;
            waterText.text = "Water: " + ClickerHandler.water.ToString("F0");

            exploreRate += 1;
            exploreLevelText.text = "Level " + exploreRate;
        }
    }

    public void BoostScavenging()
    {
        if(ClickerHandler.scraps >= 10)
        {
            ClickerHandler.scraps -= 10;
            scrapsText.text = "Scraps: " + ClickerHandler.scraps.ToString("F0");

            scavengeRate += 1;
            scavengeLevelText.text = "Level " + scavengeRate;
        }
    }

    public void BoostFarming()
    {
        if(ClickerHandler.food >= 10 && ClickerHandler.water >= 10)
        {
            ClickerHandler.food -= 10;
            foodText.text = "Food: " + ClickerHandler.food.ToString("F0");
            ClickerHandler.water -= 10;
            waterText.text = "Water: " + ClickerHandler.water.ToString("F0");

            farmRate += 1;
            farmLevelText.text = "Level " + farmRate;
        }
    }

    public void BoostPumping()
    {
        if(ClickerHandler.scraps >= 10)
        {
            ClickerHandler.scraps -= 10;
            scrapsText.text = "Scraps: " + ClickerHandler.scraps.ToString("F0");

            pumpRate += 1;
            pumpLevelText.text = "Level " + pumpRate;
        }
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
        ClockTime.clock = 0;
        ClockTime.day += 1;
        dayText.text = "DAY " + ClockTime.day + " / " + ClockTime.endDay;
    }

    public void CheckVictory()
    {
        if(ClockTime.day > ClockTime.endDay)
        {
            overheadText.text = "VICTORY!";
        }
    }

    public void UpdateSpeed1x()
    {
        ClockTime.speed = 1;
    }

    public void UpdateSpeed5x()
    {
        ClockTime.speed = 5;
    }

    public void UpdateSpeed10x()
    {
        ClockTime.speed = 10;
    }

    public void UpdateSpeed50x()
    {
        ClockTime.speed = 50;
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenMap()
    {
        SceneManager.LoadScene("Map");
    }

    public void OpenTask()
    {
        SceneManager.LoadScene("Clicker");
    }

    public void OpenTech()
    {
        SceneManager.LoadScene("Upgrade");
    }
}
