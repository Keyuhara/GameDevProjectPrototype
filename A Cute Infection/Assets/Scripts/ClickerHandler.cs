using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickerHandler : MonoBehaviour
{
    public TMP_Text exploreText;
    public TMP_Text scavengeText;
    public TMP_Text foodText;
    public TMP_Text waterText;
    public TMP_Text scrapsText;
    public TMP_Text healthText;
    public TMP_Text staminaText;
    public TMP_Text gunText;
    public TMP_Text ammoText;
    public TMP_Text shivText;
    public TMP_Text defenseText;

    public TMP_Text farmsText;
    public TMP_Text pumpsText;

    public static double explored;
    public static double notExplored = 100;
    public static double searched;
    public static double notSearched;

    public static double lootFlag;
    
    public static double food;
    public static double water;
    public static double scraps;

    public static double HP = 100;
    public static double maxHP = 100;
    public static double SP = 100;
    public static double maxSP = 100;
    public static double gun;
    public static double ammo;
    public static double shiv;
    public static double defense;

    public static double farms;
    public static double pumps;

    public float rand;

    public void Start()
    {
        foodText.text = "Food: " + food.ToString("F0");
        waterText.text = "Water: " + water.ToString("F0");
        scrapsText.text = "Scraps: " + scraps.ToString("F0");
        healthText.text = "HP: " + HP.ToString("F0") + " / " + maxHP.ToString("F0");
        staminaText.text = "SP: " + SP.ToString("F0") + " / " + maxSP.ToString("F0");
        gunText.text = "Gun: " + gun.ToString("F0");
        ammoText.text = "Ammo: " + ammo.ToString("F0");
        shivText.text = "Shiv: " + shiv.ToString("F0");
        defenseText.text = "DEF: " + defense.ToString("F0");
        
        exploreText.text = explored.ToString("F0") + " / " + notExplored.ToString("F0");
        scavengeText.text = searched.ToString("F0") + " / " + notSearched.ToString("F0");
        farmsText.text = farms.ToString("F0") + " Farms";
        pumpsText.text = pumps.ToString("F0") + " Pumps";
    }

    public void Update()
    {
        if(JobHandler.explorers > 0 && explored < notExplored)
        {
            explored += UpgradeHandler.exploreRate * JobHandler.explorers * Time.deltaTime * ClockTime.speed;
            notSearched += 10 * UpgradeHandler.exploreRate * JobHandler.explorers * Time.deltaTime * ClockTime.speed;

            if(explored > notExplored)
            {
                explored = notExplored;
            }

            exploreText.text = explored.ToString("F0") + " / " + notExplored.ToString("F0");
            scavengeText.text = searched.ToString("F0") + " / " + notSearched.ToString("F0");
        }

        if(JobHandler.scavengers > 0 && searched < notSearched)
        {
            lootFlag += UpgradeHandler.scavengeRate * JobHandler.scavengers * Time.deltaTime * ClockTime.speed;
            searched += UpgradeHandler.scavengeRate * JobHandler.scavengers * Time.deltaTime * ClockTime.speed;

            while(lootFlag > 1)
            {
                Loot();
                lootFlag -= 1;
            }

            if(searched > notSearched)
            {
                searched = notSearched;
            }
            
            scavengeText.text = searched.ToString("F0") + " / " + notSearched.ToString("F0");
        }

        if(JobHandler.farmers > 0)
        {
            food += UpgradeHandler.farmRate * JobHandler.farmers * Time.deltaTime * ClockTime.speed;
            
            foodText.text = "Food: " + food.ToString("F0");
        }

        if(JobHandler.pumpers > 0)
        {
            water += UpgradeHandler.pumpRate * JobHandler.pumpers * Time.deltaTime * ClockTime.speed;
            
            waterText.text = "Water: " + water.ToString("F0");
        }
    }

    public void Explore()
    {
        if(explored < notExplored)
        {
            explored += 1;
            notSearched += 10;
            exploreText.text = explored.ToString("F0") + " / " + notExplored.ToString("F0");
            scavengeText.text = searched.ToString("F0") + " / " + notSearched.ToString("F0");
        }

        if(explored > notExplored)
        {
            explored = notExplored;
        }
    }

    public void Scavenge()
    {
        if(searched < notSearched)
        {
            searched += 1;
            scavengeText.text = searched.ToString("F0") + " / " + notSearched.ToString("F0");
            Loot();
        }
        
        if(searched > notSearched)
        {
            searched = notSearched;
        }
    }

    public void Loot()
    {
        rand = Random.Range(0, 100);
        switch(rand)
        {
            case float i when i > 95:
                food += 1;
                foodText.text = "Food: " + food.ToString("F0");
                break;
            case float i when i > 90:
                water += 1;
                waterText.text = "Water: " + water.ToString("F0");
                break;
            default:
                scraps += 1;
                scrapsText.text = "Scraps: " + scraps.ToString("F0");
                break;
        }
    }

    public void BuildFarm()
    {
        if(food >= 100 && water >= 100)
        {
            food -= 100;
            foodText.text = "Food: " + food.ToString("F0");
            water -= 100;
            waterText.text = "Water: " + water.ToString("F0");

            farms += 1;
            farmsText.text = farms.ToString("F0") + " Farms";
        }
    }

    public void BuildPump()
    {
        if(scraps >= 100)
        {
            scraps -= 100;
            scrapsText.text = "Scraps: " + scraps.ToString("F0");

            pumps += 1;
            pumpsText.text = pumps.ToString("F0") + " Pumps";
        }
    }

    public void EatFood()
    {
        if(food > 0 && HP < maxHP)
        {
            food -= 1;
            foodText.text = "Food: " + food.ToString("F0");

            HP += 10;

            if(HP > maxHP)
            {
                HP = maxHP;
            }

            healthText.text = "HP: " + HP.ToString("F0") + " / " + maxHP.ToString("F0");
        }
    }

    public void DrinkWater()
    {
        if(water > 0 && SP < maxSP)
        {
            water -= 1;
            waterText.text = "Water: " + water.ToString("F0");

            SP += 10;

            if(SP > maxSP)
            {
                SP = maxSP;
            }

            staminaText.text = "SP: " + SP.ToString("F0") + " / " + maxSP.ToString("F0");
        }
    }

    public void CraftGun()
    {
        if(scraps >= 1000)
        {
            scraps -= 1000;
            scrapsText.text = "Scraps: " + scraps.ToString("F0");

            gun += 1;
            gunText.text = "Gun: " + gun.ToString("F0");
        }
    }

    public void CraftAmmo()
    {
        if(scraps >= 5)
        {
            scraps -= 5;
            scrapsText.text = "Scraps: " + scraps.ToString("F0");

            ammo += 1;
            ammoText.text = "Ammo: " + ammo.ToString("F0");
        }
    }

    public void CraftShiv()
    {
        if(scraps >= 100)
        {
            scraps -= 100;
            scrapsText.text = "Scraps: " + scraps.ToString("F0");

            shiv += 1;
            shivText.text = "Shiv: " + shiv.ToString("F0");
        }
    }

    public void BuildDefense()
    {
        if(scraps >= 20)
        {
            scraps -= 20;
            scrapsText.text = "Scraps: " + scraps.ToString("F0");

            defense += 1;
            defenseText.text = "DEF: " + defense.ToString("F0");
        }
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