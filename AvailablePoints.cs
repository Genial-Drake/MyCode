using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvailablePoints : GetManagers
{
    [SerializeField] private TextMeshProUGUI availablePointsText;
    [SerializeField] private TextMeshProUGUI strStatText;
    [SerializeField] private TextMeshProUGUI agiStatText;
    [SerializeField] private TextMeshProUGUI hpStatText;
    [SerializeField] private TextMeshProUGUI mpStatText;

    [SerializeField] private List<Image> upgradeButtonImages = new();

    public void UIUpdateMenuStatsText() // aka new game
    {     
        strStatText.text = pD.strStat.ToString();
        agiStatText.text = pD.strStat.ToString();
        hpStatText.text = pD.strStat.ToString();
        mpStatText.text = pD.strStat.ToString();       
      
        AvailablePointUpdater(0); // just to set text

        UpdateStatText(strStatText, pD.strStat, pD.strBonusStat);
        UpdateStatText(agiStatText, pD.agiStat, pD.agiBonusStat);
        UpdateStatText(hpStatText, pD.hpStat, pD.hpBonusStat);
        UpdateStatText(mpStatText, pD.mpStat, pD.mpBonusStat);
        
    }   

    public void AvailablePointUpdater(int points)
    {
        pD.availablePoints += points;
        availablePointsText.text = "Available points: " + pD.availablePoints + " / Used points: " + pD.spendPoints;

        Color grimGOld = new(103 / 255f, 95 / 255f, 24 / 255f, 255f);
        Color grimGray = new(30 / 255f, 30 / 255f, 30 / 255f, 255f);
        
        foreach (Image image in upgradeButtonImages)
        {
            image.color = pD.availablePoints > 0 ? grimGOld : grimGray;
            image.transform.GetComponent<Button>().enabled = pD.availablePoints > 0;
        }
    }
   
    public void ButtonToUpgradeStats(string name)
    {
        if (pD.availablePoints > 0)
        {
            pD.spendPoints += 1;
            AvailablePointUpdater(-1);
            mS.WhatStatNeedsUpdating(name);
            if (name == "str") { pD.strStat += 1; UpdateStatText(strStatText, pD.strStat, pD.strBonusStat); }
            else if (name == "agi") { pD.agiStat += 1; UpdateStatText(agiStatText, pD.agiStat, pD.agiBonusStat);}
            else if (name == "hp") { pD.hpStat += 1; UpdateStatText(hpStatText, pD.hpStat, pD.hpBonusStat); }
            else if (name == "mp") { pD.mpStat += 1; UpdateStatText(mpStatText, pD.mpStat, pD.mpBonusStat); }            
        }
    }   

    private void UpdateStatText(TextMeshProUGUI t, int s, int b) => t.text = b < 1 ? s.ToString() : s + " + " + b;       
}
