using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAllScriptsForMS : MonoBehaviour
{
    public OnLoadingScript onLoadingScript;    
    public PlayerActions playerActionScript;
    public PlayerStats playerStatScript;
    public OnReturnToMainMenu onReturnToMainMenuScript;
    public OnButtonPressed onButtonPressedScript;
    public EnemyActions enemyActionsScript;
    public EnemyStats enemyStatsScript;
    public AddToTextLog addToTextLogScript;
    public OnNewGame onNewGameScript;
    public AvailablePoints availablePointsScript;
    public OnPlayerDamagedOrHealed onPlayerDamagedOrHealedScript;
    public OnExpGained onExpGainedScript;
    public OnManaGainedOrUsed onManaGainedOrUsedScript;
    public ArrowsForBossFloor arrowsForBossFloorScript;   
    public SkillBuilder skillBuilderScript;
    public Regen regenScript;
    public CaseManager caseManager;
}
