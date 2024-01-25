using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnManaGainedOrUsed : GetManagers
{
    
  
    public int playerCurrentMana;    

    public Slider playerMpSlider;

    public TextMeshProUGUI playerMpText;

    public void PlayerHealedOrUsedMp(int amount) // also used to respawnPlayer 
    {
        int maxMp = pD.playerMaxMana;

        pD.playerCurrentMp = Mathf.Clamp(pD.playerCurrentMp + amount, 0, maxMp);

        playerMpSlider.value = (float)pD.playerCurrentMp / maxMp;
        playerMpText.text = "Mana: " + pD.playerCurrentMp + "/" + maxMp;

        //if (amount == maxMp) { MasterScript.MSAddText("Player Revived ", Color.white); }
        if (amount > 0 && pD.playerCurrentMp < maxMp) { mS.MSAddText("Mana Healed: " + amount, Color.blue); }
        else if (amount < 0) { mS.MSAddText("Skill used: " + amount + " Mana Used", Color.blue); }

      

        

        // logic here for if mana is less than what amount is used to stop
        // pause attking till you have enough mana to use the skill
    }    
}
