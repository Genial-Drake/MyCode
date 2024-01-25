using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CaseManager;

public class OnPlayerDamagedOrHealed : GetManagers
{
    public GameObject prefabDmgText;

    public Transform dmgTextParant;

    public Coroutine PlayerRespawnRoutine = null;

    public Slider playerHpSlider;

    public TextMeshProUGUI playerHpText;
    public TextMeshProUGUI playerCountdownText;  

    public void HealPlayer(int heal, string healType)
    {
        pD.playerCurrentHp = Mathf.Clamp(pD.playerCurrentHp + heal, 0, pD.playerMaxHealth);

        playerHpSlider.value = (float)pD.playerCurrentHp / pD.playerMaxHealth;
        playerHpText.text = "Health: " + pD.playerCurrentHp + "/" + pD.playerMaxHealth;

        if (healType == "Meditating") { mS.MSAddText("Meditating: " + heal, Color.green); DmgTextSpawnPopUp(heal); }
        if (healType == "regen") { mS.MSAddText("Health Regen: " + heal, Color.green); DmgTextSpawnPopUp(heal); }           
    }

    public void DamagedPlayer(int damage, string dmgType)
    {
        int maxHp = pD.playerMaxHealth;

        pD.playerCurrentHp = Mathf.Clamp(pD.playerCurrentHp - damage, 0, maxHp);

        playerHpSlider.value = (float)pD.playerCurrentHp / maxHp;
        playerHpText.text = "Health: " + pD.playerCurrentHp + "/" + maxHp;

        if (dmgType == "enemy") { mS.MSAddText("Enemy Hit Player: " + damage, Color.red); DmgTextSpawnPopUp(-damage); }        
        if (dmgType == "skill") { mS.MSAddText("Life Drained: " + damage, Color.red); DmgTextSpawnPopUp(-damage); }       

        if (pD.playerCurrentHp <= 0) { OnPlayerDied(); }
    }

    public void OnPlayerDied()
    {
        if(eD.currentBossLevel > 1) 
        {
            eD.currentBossLevel = Mathf.Clamp(eD.currentBossLevel - 100, 1, 1000);
            eD.bossLevelBeaten = Mathf.Clamp(eD.bossLevelBeaten - 100, 0, 1000);

            mS.MSButtonArrowsToChangeBossLevel(0);
        }
        eD.actuallyEnemyHp += eD.actuallyEnemyHp / 10;
        playerHpText.text = "Dieded";
        mS.MSAddText("Player Dieded", Color.white);
        mS.PerformGameActionMS(GameAction.PlayerDieded);
        playerHpSlider.value = 0;
        PlayerRespawnRoutine ??= StartCoroutine(RespawningPlayer());        
    }

    public IEnumerator RespawningPlayer()
    {
        int respawnTime = 3;
        while (respawnTime > 0)
        {
            playerCountdownText.text = respawnTime.ToString() + " seconds until respawn";
            yield return new WaitForSeconds(1f);
            respawnTime--;
        }
        mS.MSAddText("Player Revived ", Color.white);
        RespawnPlayer();
    }    
    public void StopPlayerRespawning() { if (PlayerRespawnRoutine != null) StopCoroutine(PlayerRespawnRoutine); PlayerRespawnRoutine = null; }     

    public void RespawnPlayer()
    {        
        StopPlayerRespawning();
        HealPlayer(pD.playerMaxHealth,"None"); // using this to update the UI
        mS.MSPlayerHealedOrUsedMp(pD.playerMaxMana); // using this to update the UI
        playerCountdownText.text = "";
        mS.PerformGameActionMS(GameAction.OnStartCo);        
    }

    public void DmgTextSpawnPopUp(int dmg)    
    {       
        int randX = Random.Range(-120, 120);
        int randY = Random.Range(-120, 120);

        GameObject obj = Instantiate(prefabDmgText,  dmgTextParant);
        
        obj.transform.GetComponent<TextMeshProUGUI>().text = dmg.ToString();

        obj.transform.localPosition = new Vector3(randX, randY, 0);        

        obj.transform.GetComponent<TextMeshProUGUI>().color = dmg > 0 ? Color.green : Color.red;       
    }
}
