using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CaseManager;

public class ArrowsForBossFloor : GetManagers
{  
    public Image rightArrow;
    public Image leftArrow;  

    private Color shaded = new(30 / 255f, 30 / 255f, 30 / 255f, 255f);
    private Color unShaded = new(103 / 255f, 103 / 255f, 103 / 255f, 255f);

    public TextMeshProUGUI floorLevelText;
    public TextMeshProUGUI bossNameText;

    public List<Sprite> bossPictures;
    public List<string> bossName;
    public Image bossImageParent;

    public List<SkillMultyperVariables> SkillMultyperVarHit;

    public void ButtonArrowsToChangeBossLevel(int upOrDown)
    {
        mS.PerformGameActionMS(GameAction.OnStopCo);

        eD.currentBossLevel += upOrDown;
        mS.ChangeBossStatsMS();

        if (eD.currentBossLevel - 1 <= bossPictures.Count - 1) 
        {
            bossImageParent.sprite = bossPictures[eD.currentBossLevel - 1];
            bossNameText.text = bossName[eD.currentBossLevel - 1];
        }
        else // for if not more monster pictures left
        {
            int ran = Random.Range(0, bossPictures.Count - 1);
            bossImageParent.sprite = bossPictures[ran];
            bossNameText.text = bossName[ran];
        }




        mS.WhatStatNeedsUpdating("agi"); // to work out the % chance of hitting the Boss and display it on stats

     
        mS.SpawnPlayer();
        mS.SpawnEnemy();

        // maybe need a delay here

        if (mS.MSWhatSkillBuilderType() == "basic") { mS.MSUpdateSkillHitStat(SkillMultyperVarHit[0]); } // not sure yet
      
        EnableOrDisabledArrowButtons();
        mS.PerformGameActionMS(GameAction.OnStartCo);
    }

    public void EnableOrDisabledArrowButtons()
    {
        int bossLevelBeaten = eD.bossLevelBeaten;
        int currentBossLevel = eD.currentBossLevel;

        Button rightButton = rightArrow.transform.parent.GetComponent<Button>();
        Button leftButton = leftArrow.transform.parent.GetComponent<Button>();

        rightArrow.color = bossLevelBeaten >= currentBossLevel ? rightArrow.color = unShaded : rightArrow.color = shaded;
        rightButton.enabled = bossLevelBeaten >= currentBossLevel ? rightButton.enabled = true : rightButton.enabled = false;

        leftArrow.color = currentBossLevel > 1 ? leftArrow.color = unShaded : leftArrow.color = shaded;
        leftButton.enabled = currentBossLevel > 1 ? leftButton.enabled = true : leftButton.enabled = false;
       
        floorLevelText.text = "Floor: " + currentBossLevel;
    }
}
