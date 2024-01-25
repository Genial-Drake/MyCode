using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnExpGained : GetManagers
{
    [SerializeField] private Slider expSlider;

    [SerializeField] private TextMeshProUGUI levelNumber, levelNumberStatMenu, expBarDisplaytext, totalExpGainedStatMenu; 

    public void ResetExpValues()
    {
        pD.expmax = 100;
        pD.overallExpGained = 0;
        pD.overallExpNeededForLevel = 100;
        pD.ExpCurrent = 0;
        UpdateAllUi();
    }

    public void UpdateExp(float exp)
    {
        mS.MSAddText("Exp Gained: " + exp.ToString(), Color.magenta);
        pD.ExpCurrent += exp;
        pD.overallExpGained += exp;

        while (pD.ExpCurrent >= pD.expmax)
        {
            pD.ExpCurrent -= pD.expmax;
            pD.playerLevel += 1;

            pD.expmax = Mathf.CeilToInt(pD.expmax * (pD.expMult = pD.playerLevel % 100 == 0 ? pD.expMult / 2 + 0.5f : pD.expMult));

            mS.MSAddText("Congratz you Leveled Up!", Color.magenta); // need if statment if OnLoad don't print this

            mS.UpdatePointsFunction(10);
            pD.overallExpNeededForLevel += pD.expmax;
            mS.WhatStatNeedsUpdating("lvl");
        }
        UpdateAllUi();
    }
    private void UpdateAllUi()
    {
        totalExpGainedStatMenu.text = "Exp: " + Mathf.Round(pD.overallExpGained) + " / " + Mathf.Round(pD.overallExpNeededForLevel);
        expBarDisplaytext.text = "Exp: " + Mathf.Round(pD.ExpCurrent) + " / " + Mathf.Round(pD.expmax).ToString();
        expSlider.maxValue = pD.expmax;
        expSlider.value = pD.ExpCurrent;
        levelNumber.text = "LEVEL: " + pD.playerLevel;
        levelNumberStatMenu.text = "LEVEL: " + pD.playerLevel;
    }
}
