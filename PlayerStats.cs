using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : GetManagers // aka UI update and stat calucations
{    
    [SerializeField] private TextMeshProUGUI onStatsMenuDmgText;    
    [SerializeField] private TextMeshProUGUI onStatsMenuAtkSpeed;
    [SerializeField] private TextMeshProUGUI onStatsMenuHealthText;
    [SerializeField] private TextMeshProUGUI onStatsMenuManaText;   
    [SerializeField] private TextMeshProUGUI onStatsMenuEnduranceText;
    [SerializeField] private TextMeshProUGUI onStatsMenuHitChanceText;

    // needs regen rate
    // needs skill dmg rate
  
    public void WhichStatToUpdate(string whatStat) // might not need this anymore
    {
        if (whatStat == "str") { UpdateAllStrengthStats(); }
        else if (whatStat == "agi") { UpdateAllAgilityStats(); }
        else if (whatStat == "hp") { UpdateAllStanimaStats(); }
        else if (whatStat == "mp") { UpdateAllEnergyyStats(); }
        else if (whatStat == "lvl") { UpdateAllStats(); }      
    }

    private void UpdateAllStats()
    {
        UpdateAllStrengthStats();
        UpdateAllAgilityStats();
        UpdateAllStanimaStats();
        UpdateAllEnergyyStats();
    }

    private void UpdateAllStrengthStats() 
    {
        float str = pD.strStat + pD.strBonusStat;
        pD.minAtk = (int)Mathf.Ceil((str / 5f + pD.minWeaponDmg + pD.playerLevel / 3f) * (1 + (str / 250.0f)));
        pD.maxAtk = (int)Mathf.Ceil((str / 3f + pD.maxWeaponDmg + pD.playerLevel / 5f) * (1 + (str / 250.0f)));

        pD.minAtk = pD.minAtk > pD.maxAtk ? pD.minAtk = pD.maxAtk : pD.minAtk;        
      
        onStatsMenuDmgText.text = "Dmg(rate): " + pD.minAtk + " ~ " + pD.maxAtk.ToString();
        
        pD.endurance = (int)Mathf.Ceil((str / 6.9f + pD.enduranceEquipmentBonus) * (1 + (str / 1000.0f)));

        onStatsMenuEnduranceText.text = "End(rate): " + (int)Mathf.Ceil(str / 1.69f) + " + " 
            + pD.enduranceEquipmentBonus + " (" + pD.endurance + "~)";
    }

    private void UpdateAllAgilityStats()
    {
        float agi = pD.agiStat + pD.agiBonusStat;

        pD.atkSpeed = 5f * pD.equipmentAtkSpeed * (float)Mathf.Pow(0.99f, agi / 12.5f);  

        onStatsMenuAtkSpeed.text = "Atk.spd(rate): " + pD.atkSpeed.ToString("F2") + "s";

        pD.hitChance = (int)Mathf.Ceil(agi * 1.69f * (float)Mathf.Pow(0.99f, agi / 25f));

        float baseHitChance = Calucations.ReturnHitOrMissPercent(pD.hitChance, eD.enemyEvasion);

        onStatsMenuHitChanceText.text = "Hit(rate): " + pD.hitChance + " (" + baseHitChance.ToString("F1") + "~)%";
    }

    private void UpdateAllStanimaStats() 
    {
        // skill power 0.069f per point.
        // 1000stat = 20050hp x 6.9 / 100 = 1383.45f power
        int hp = pD.hpStat + pD.hpBonusStat;
        pD.playerMaxHealth = (int)Mathf.Floor((hp * 10f + pD.playerLevel * 25f) * (1 + (hp / 1000.0f)));
        onStatsMenuHealthText.text = "Max Health: " + pD.playerMaxHealth.ToString();
        mS.HealPlayerMS(0,"none"); // also kills player maybe split it into two one heal 1 kill     
    }

    private void UpdateAllEnergyyStats() // skill 0.025f
    {
        // skill power 0.025f per point.
        // 1000stat = 37593hp x 2.5 / 100 = 939.825f power

        int mp = pD.mpStat + pD.mpBonusStat;
        pD.playerMaxMana = (int)Mathf.Floor((mp * 25f + pD.playerLevel * 62f) * (1 + (mp / 2000.0f)));

        onStatsMenuManaText.text = "Mana: " + pD.playerMaxMana.ToString();
        mS.MSPlayerHealedOrUsedMp(0);
        // + 1% skill mult for making skills 
    }


   
}
