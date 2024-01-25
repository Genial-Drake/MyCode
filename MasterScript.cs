using UnityEngine;
using static CaseManager;

public class MasterScript : MonoBehaviour
{
    [SerializeField] private GetAllScriptsForMS aS; // All Scripts

    public PlayerDataSO playerDataSO;
    public EnemyDataSO enemyDataSO;


    public GameObject opensMainGamePlayScene, StartScreen, logMenu;
    public bool devBool;

    private void Awake()
    {
        Application.runInBackground = true;
    }


    private void Start()
    {       
        opensMainGamePlayScene.SetActive(false);

        if (devBool == false) { StartOnLoadingFunction(); }
        else { aS.onNewGameScript.NewGame(); }
        UpdatePointsFunction(5000);    
        
    }
    public void StartOnLoadingFunction() => aS.onLoadingScript.StartingCoroutine();

    public void MSAddText(string text, Color color) => aS.addToTextLogScript.AddText(text, color);
    public void ClearListFunction() => aS.addToTextLogScript.ClearLists();    

    public void AttacksEnemy(int atk) => aS.enemyStatsScript.DamageTaken(atk);
    //public void MSPlayerTakesDmgOrHealed(int amount) => aS.onPlayerDamagedOrHealedScript.PlayerTakesDmgOrHealed(amount);   

    public void DamagePlayerMS(int damage, string dmgType) => aS.onPlayerDamagedOrHealedScript.DamagedPlayer(damage, dmgType);
    public void HealPlayerMS(int heal, string healType) => aS.onPlayerDamagedOrHealedScript.HealPlayer(heal, healType);

    public void PreviouslyOpenedMenuReset() => aS.onButtonPressedScript.ButtonPressed(logMenu); 

    public void UpdatePlayerExp(float exp) => aS.onExpGainedScript.UpdateExp(exp);

    public void UpdatePointsFunction(int points) => aS.availablePointsScript.AvailablePointUpdater(points);

    public void ReturnToBaseStats() => aS.availablePointsScript.UIUpdateMenuStatsText();

    public void WhatStatNeedsUpdating(string whatStat) => aS.playerStatScript.WhichStatToUpdate(whatStat);  

    public void MSPlayerHealedOrUsedMp(int amount) => aS.onManaGainedOrUsedScript.PlayerHealedOrUsedMp(amount);

    public void MSEnableOrDisabledArrowButtons() => aS.arrowsForBossFloorScript.EnableOrDisabledArrowButtons();

    public void MSButtonArrowsToChangeBossLevel(int upOrDown) => aS.arrowsForBossFloorScript.ButtonArrowsToChangeBossLevel(upOrDown);

    public void MSResetExpValues() => aS.onExpGainedScript.ResetExpValues();      

    public GameObject MSReturnGameObjectToBeClosed() { return aS.skillBuilderScript.privouslyOpened; }

    public void MSUpdateSkillHitStat(SkillMultyperVariables script) => aS.skillBuilderScript.UpdateSkillHitStat(script);

    public string MSWhatSkillBuilderType() => aS.skillBuilderScript.WhatSkillBuilderType();
    
    public bool MSEnemyRespawning() => aS.enemyStatsScript.EnemyRespawning();
    public bool MSIsMeditating() => aS.regenScript.IsMeditating();

    public void ChangeBossStatsMS() => aS.enemyStatsScript.ChangeBossStats();

    // here for co's
    public void PerformGameActionMS(GameAction action) => aS.caseManager.PerformGameAction(action);

    public void SpawnEnemy() => aS.enemyStatsScript.RespawnEnemy();
    public void SpawnPlayer() => aS.onPlayerDamagedOrHealedScript.RespawnPlayer();


    public void StopEnemyRespawningFunction() => aS.enemyStatsScript.StopEnemyRespawning(); // plus changing Floor / boss
    public void MSStopPlayerRespawning() => aS.onPlayerDamagedOrHealedScript.StopPlayerRespawning(); // plus changing Floor / boss

    public void StartPlayerAtkFunction() => aS.playerActionScript.StartPlayerAtkBarFiller();
    public void StopPlayerAtkFunction() => aS.playerActionScript.StopPlayerAtkBarFiller();

    public void StartEnemyAtkFunction() => aS.enemyActionsScript.StartEnemyAtkBarFiller();
    public void StopEnemyAtkFunction() => aS.enemyActionsScript.StopEnemyAtkBarFiller();


    public void StartMeditateRoutineMS() => aS.regenScript.StartMeditateRoutine();
    public void MSStopMeditateRoutine() => aS.regenScript.StopMeditateRoutine();
    
   
    public void MSStartHpCo() => aS.regenScript.StartHpCo();
    public void MSStartMpCo() => aS.regenScript.StartMpCo();

    public void MSStopMpCo() => aS.regenScript.StopMpCo();
    public void MSStopHpCo() => aS.regenScript.StopHpCo();
}



