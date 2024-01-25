using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseManager : GetManagers 
{ 
    public enum GameAction
    {      
        EnemyDieded,
        PlayerDieded,
        OnStartCo, // aka new game or after changing boss level start all co's again
        OnStopCo, // stop all on return or changing level
    }

    public void PerformGameAction(GameAction action)
    {        
        switch (action)
        {
            case GameAction.OnStartCo: NewGameSetUpCoroutine(); break;
            case GameAction.OnStopCo: ChangeBossLevelSetUpStop(); break;
            case GameAction.PlayerDieded: OnPlayerDieded(); break;
            case GameAction.EnemyDieded: OnEnemyDieded(); break;           
        }
    }

    private void NewGameSetUpCoroutine()
    {
        if (!mS.MSIsMeditating()) { mS.StartPlayerAtkFunction(); };
        mS.StartEnemyAtkFunction();
        mS.MSStartHpCo();
        mS.MSStartMpCo();
    }

    private void ChangeBossLevelSetUpStop()
    {
        mS.StopEnemyAtkFunction();
        mS.StopPlayerAtkFunction();
        mS.StopEnemyRespawningFunction();
        mS.MSStopPlayerRespawning();
        mS.MSStopMeditateRoutine();
        mS.MSStopMpCo();
        mS.MSStopHpCo();
    }

    private void OnPlayerDieded()
    {
        mS.StopEnemyAtkFunction();
        mS.StopPlayerAtkFunction();
        mS.MSStopMpCo();
        mS.MSStopHpCo();
        mS.MSStopMeditateRoutine();
    }

    private void OnEnemyDieded()
    {
        mS.StopEnemyAtkFunction();
        mS.StopPlayerAtkFunction();
    }
}
