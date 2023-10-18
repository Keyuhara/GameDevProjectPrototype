using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapHandler : MonoBehaviour
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

    public TMP_Text riskText;
    public TMP_Text zombiesText;
    public TMP_Text recruitText;
    public TMP_Text logText;
    
    public static double risk = 0;
    public static double zombies = 20;
    public static double chance = 100;

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

        riskText.text = "Risk: " + risk + "%";
        zombiesText.text = "Zombies: " + zombies;
        recruitText.text = "Chance: " + chance + "%";

        logText.text = "Survive until Day " + ClockTime.endDay + "!\nMake sure you HP don't go below 0!";
    }

    public void Update()
    {
        ClockTime.clock += Time.deltaTime * ClockTime.speed;
        DisplayTime(ClockTime.clock);

        if(JobHandler.explorers > 0 && ClickerHandler.explored < ClickerHandler.notExplored)
        {
            ClickerHandler.explored += UpgradeHandler.exploreRate * JobHandler.explorers * Time.deltaTime * ClockTime.speed;
            ClickerHandler.notSearched += 10 * UpgradeHandler.exploreRate * JobHandler.explorers * Time.deltaTime * ClockTime.speed;

            if(ClickerHandler.explored > ClickerHandler.notExplored)
            {
                ClickerHandler.explored = ClickerHandler.notExplored;
            }
        }

        if(JobHandler.scavengers > 0 && ClickerHandler.searched < ClickerHandler.notSearched)
        {
            ClickerHandler.lootFlag += UpgradeHandler.scavengeRate * JobHandler.scavengers * Time.deltaTime * ClockTime.speed;
            ClickerHandler.searched += UpgradeHandler.scavengeRate * JobHandler.scavengers * Time.deltaTime * ClockTime.speed;

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
            ClickerHandler.food += UpgradeHandler.farmRate * JobHandler.farmers * Time.deltaTime * ClockTime.speed;
            
            foodText.text = "Food: " + ClickerHandler.food.ToString("F0");
        }

        if(JobHandler.pumpers > 0)
        {
            ClickerHandler.water += UpgradeHandler.pumpRate * JobHandler.pumpers * Time.deltaTime * ClockTime.speed;
            
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

    public void ReclaimLand()
    {
        if(ClickerHandler.SP >= 10)
        {
            rand = Random.Range(0, 100);

            switch(rand)
            {
                case float i when i <= risk:
                    ClickerHandler.HP -= 10;
                    healthText.text = "HP: " + ClickerHandler.HP.ToString("F0") + " / " + ClickerHandler.maxHP.ToString("F0");
                    logText.text = "Taken 10 HP damage while reclaiming land!";
                    checkDeath();
                    break;
                case float i when i <= risk + 10:
                    ClickerHandler.scraps += risk;
                    scrapsText.text = "Scraps: " + ClickerHandler.scraps.ToString("F0");
                    logText.text = "Found " + risk + " scraps while reclaiming land!";
                    break;
                case float i when i <= risk + 20:
                    ClickerHandler.food += risk;
                    foodText.text = "Food: " + ClickerHandler.food.ToString("F0");
                    logText.text = "Found " + risk + " food while reclaiming land!";
                    break;
                case float i when i <= risk + 30:
                    ClickerHandler.water += risk;
                    waterText.text = "Water: " + ClickerHandler.water.ToString("F0");
                    logText.text = "Found " + risk + " water while reclaiming land!";
                    break;
                default:
                    logText.text = "Successfully reclaimed land!";
                    break;
            }

            ClickerHandler.SP -= 10;

            if(ClickerHandler.SP < 0)
            {
                ClickerHandler.SP = 0;
            }

            staminaText.text = "SP: " + ClickerHandler.SP.ToString("F0") + " / " + ClickerHandler.maxSP.ToString("F0");

            zombies += 5;
            risk += zombies;
            
            if(risk > 100)
            {
                risk = 100;
            }

            riskText.text = "Risk: " + risk.ToString("F0") + "%";
            zombiesText.text = "Zombies: " + zombies.ToString("F0");

            ClickerHandler.notExplored += 100;
        }
        else
        {
            logText.text = "You are low on stamina (SP)! Drink water!";
        }
    }

    public void Kill()
    {
        if(ClickerHandler.SP >= 10 && zombies >= 1)
        {
            if(ClickerHandler.gun > 0 && ClickerHandler.ammo > 0)
            {
                rand = Random.Range(0, (float) zombies);

                switch(rand)
                {
                    case float i when i >= 1:
                        zombies -= (double) i;
                        risk -= (double) i;
                        chance += (double) i;

                        if(risk < 0)
                        {
                            risk = 0;
                        }

                        if(zombies < 0)
                        {
                            zombies = 0;
                        }

                        if(chance > 100)
                        {
                            zombies = 100;
                        }

                        ClickerHandler.ammo -= (double) i;

                        if(ClickerHandler.ammo < 0)
                        {
                            ClickerHandler.ammo = 0;
                        }

                        ammoText.text = "Ammo: " + ClickerHandler.ammo.ToString("F0") + "%";

                        riskText.text = "Risk: " + risk.ToString("F0") + "%";
                        zombiesText.text = "Zombies: " + zombies.ToString("F0");
                        recruitText.text = "Chance: " + chance.ToString("F0") + "%";
                        logText.text = i.ToString("F0") + " zombies were killed!";
                        break;
                    default:
                        ClickerHandler.HP -= 10;
                        healthText.text = "HP: " + ClickerHandler.HP.ToString("F0") + " / " + ClickerHandler.maxHP.ToString("F0");
                        logText.text = "Failed to kill... Taken 10 HP damage!";
                        checkDeath();
                        break;
                }
            }
            else if (ClickerHandler.SP >= 10 && ClickerHandler.shiv > 0)
            {
                rand = Random.Range(0, 3);

                switch(rand)
                {
                    case float i when i < 1:
                        zombies -= 1;
                        risk -= 1;
                        chance += 1;

                        if(risk < 0)
                        {
                            risk = 0;
                        }

                        if(zombies < 0)
                        {
                            zombies = 0;
                        }

                        if(chance > 100)
                        {
                            zombies = 100;
                        }

                        riskText.text = "Risk: " + risk.ToString("F0") + "%";
                        zombiesText.text = "Zombies: " + zombies.ToString("F0");
                        recruitText.text = "Chance: " + chance.ToString("F0") + "%";
                        logText.text = "Killed 1 zombie with a shiv.";
                        break;
                    case float i when i < 2:
                        zombies -= 1;
                        risk -= 1;
                        chance += 1;

                        if(risk < 0)
                        {
                            risk = 0;
                        }

                        if(zombies < 0)
                        {
                            zombies = 0;
                        }

                        if(chance > 100)
                        {
                            zombies = 100;
                        }
                        
                        riskText.text = "Risk: " + risk.ToString("F0") + "%";
                        zombiesText.text = "Zombies: " + zombies.ToString("F0");
                        recruitText.text = "Chance: " + chance.ToString("F0") + "%";
                        ClickerHandler.shiv -= 1;
                        shivText.text = "Shiv: " + ClickerHandler.shiv.ToString("F0");
                        logText.text = "Killed 1 zombie with a shiv, but the shiv broke afterwards.";
                        break;
                    default:
                        ClickerHandler.HP -= 10;
                        healthText.text = "HP: " + ClickerHandler.HP.ToString("F0") + " / " + ClickerHandler.maxHP.ToString("F0");
                        logText.text = "Failed to kill... Taken 10 HP damage!";
                        checkDeath();
                        break;
                }
            }
            else if (ClickerHandler.SP >= 10)
            {
                rand = Random.Range(0, 2);

                switch(rand)
                {
                    case float i when i < 1:
                        zombies -= 1;
                        risk -= 1;
                        chance += 1;

                        if(risk < 0)
                        {
                            risk = 0;
                        }

                        if(zombies < 0)
                        {
                            zombies = 0;
                        }

                        if(chance > 100)
                        {
                            zombies = 100;
                        }

                        riskText.text = "Risk: " + risk.ToString("F0") + "%";
                        zombiesText.text = "Zombies: " + zombies.ToString("F0");
                        recruitText.text = "Chance: " + chance.ToString("F0") + "%";
                        logText.text = "Killed 1 zombie bare-handed.";
                        break;
                    default:
                        ClickerHandler.HP -= 10;
                        healthText.text = "HP: " + ClickerHandler.HP.ToString("F0") + " / " + ClickerHandler.maxHP.ToString("F0");
                        logText.text = "Failed to kill... Taken 10 HP damage!";
                        checkDeath();
                        break;
                }
            }

            ClickerHandler.SP -= 10;

            if(ClickerHandler.SP < 0)
            {
                ClickerHandler.SP = 0;
            }

            staminaText.text = "SP: " + ClickerHandler.SP.ToString("F0") + " / " + ClickerHandler.maxSP.ToString("F0");
        }
        else if(zombies < 0)
        {
            logText.text = "There are no zombies!";
        }
        else
        {
            logText.text = "You are low on stamina (SP)! Drink water!";
        }
    }

    public void Recruit()
    {
        if(ClickerHandler.SP >= 10)
        {
            rand = Random.Range(0, 100);

            switch(rand)
            {
                case float i when i < chance:
                    JobHandler.idle += 1;
                    JobHandler.survivors += 1;
                    populationText.text = "Pop: " + JobHandler.idle + " / " + JobHandler.survivors;
                    logText.text = "Successfully recruited!";
                    break;
                default:
                    logText.text = "Failed to recruit...";
                    break;
            }

            ClickerHandler.SP -= 10;

            if(ClickerHandler.SP < 0)
            {
                ClickerHandler.SP = 0;
            }

            staminaText.text = "SP: " + ClickerHandler.SP.ToString("F0") + " / " + ClickerHandler.maxSP.ToString("F0");

            zombies += 5;
            chance -= zombies;

            if(chance < 0)
            {
                chance = 0;
            }

            recruitText.text = "Chance: " + chance + "%";
            zombiesText.text = "Zombies: " + zombies;
        }
        else
        {
            logText.text = "You are low on stamina (SP)! Drink water!";
        }
    }

    public void DisplayTime(float time)
    {
        if(time >= 1440)
        {
            zombies += 25;
            zombiesText.text = "Zombies: " + zombies;
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

    public void checkDeath()
    {
        if(ClickerHandler.HP < 0)
        {
            Debug.Log("YOU DIED! GAME OVER!");
            SceneManager.LoadScene("MainMenu");
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
