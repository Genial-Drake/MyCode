using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static CaseManager;

public class OnReturnToMainMenu : GetManagers
{
    public Transform enemyTextDmgParent;
    public Transform playingextDmgParent;

    public void ReturnToMainMenuSettings()
    {            
        mS.opensMainGamePlayScene.SetActive(false);
        mS.StartScreen.SetActive(true);        
        mS.ClearListFunction();

        mS.PerformGameActionMS(GameAction.OnStopCo);

        // This line finds all children of enemyTextDmgParent and destroys them
        enemyTextDmgParent.Cast<Transform>().ToList().ForEach(child => GameObject.Destroy(child.gameObject));
        playingextDmgParent.Cast<Transform>().ToList().ForEach(child => GameObject.Destroy(child.gameObject));
    }
}
