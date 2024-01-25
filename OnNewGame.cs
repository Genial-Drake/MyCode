using static CaseManager;

public class OnNewGame : GetManagers
{
    public PlayerDataSO defaultValuesPlayData;  

    public void NewGame()
    {
        ScriptableObjectCopier.CopyValues(defaultValuesPlayData, pD);

        mS.opensMainGamePlayScene.SetActive(true);
        mS.StartScreen.SetActive(false);

        mS.ReturnToBaseStats(); // new game stats 
        mS.PreviouslyOpenedMenuReset(); // sets to log menu

        mS.WhatStatNeedsUpdating("lvl"); // text update for all stats and calucate all stats        

        mS.MSResetExpValues();
        
        eD.bossLevelBeaten = 0;
        eD.currentBossLevel = 1;
        mS.MSButtonArrowsToChangeBossLevel(0); // also covers respawning enemy and player       
    }
}

