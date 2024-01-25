using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PlayerActions : GetManagers
{
    public TextMeshProUGUI timeToAttackEnemyText;

    public GameObject atkSpeedBarObj;
    public GameObject whenAttackOverLay;

    public Transform basicSkillSlot;

    private Coroutine atkfillerRoutine = null;
    private Coroutine atkOverlayRoutine = null;
    

    public bool HaveEnoughMp(Skill script) => pD.playerCurrentMp >= script.overAllMpCost;

    public void StartPlayerAtkBarFiller() => atkfillerRoutine ??= StartCoroutine(PlayerAtkBarFiller());     
    public void StartAtkOverlayPopup() => atkOverlayRoutine ??= StartCoroutine(AtkOverlayPopup());
    public void StopAtkOverlayPopup() { if (atkOverlayRoutine != null) { StopCoroutine(atkOverlayRoutine); atkOverlayRoutine = null; } }

    private IEnumerator PlayerAtkBarFiller()
    {        
        Skill script = basicSkillSlot.GetChild(0).GetComponent<Skill>();
        var slider = atkSpeedBarObj.GetComponent<Slider>();      
        float elapsedTime = 0f;     
        slider.maxValue = pD.atkSpeed;        
        slider.value = 0f;

        while (true)
        {
            if (slider.value == 0f && HaveEnoughMp(script))
            {
                script = basicSkillSlot.GetChild(0).GetComponent<Skill>();
                if (script.overAllHpCost > 0) { mS.DamagePlayerMS(-script.overAllHpCost, "skill"); }
                mS.MSPlayerHealedOrUsedMp(-script.overAllMpCost);                
            }
            else if(slider.value == 0f && !HaveEnoughMp(script)) { mS.StartMeditateRoutineMS(); StopPlayerAtkBarFiller(); }

            elapsedTime += Time.deltaTime;          
            slider.value += Time.deltaTime;
            timeToAttackEnemyText.text = (slider.maxValue - elapsedTime).ToString("F2") + "s";

            if (slider.value >= slider.maxValue) // When filled, ready to attack
            {                
                slider.value = 0f;
                elapsedTime = 0f;
                timeToAttackEnemyText.text = pD.atkSpeed.ToString("F2") + "s";                
                CanIAttackAndWhatDmgDoIDeal(script);
                yield return new WaitForSeconds(0.1f);
                if (mS.MSEnemyRespawning() == true) { break; }
                slider.maxValue = pD.atkSpeed;                 
            }
            else { yield return null; }            
        }
    }

    public void StopPlayerAtkBarFiller()
    {
        if (atkfillerRoutine != null)
        {
            StopCoroutine(atkfillerRoutine);
            atkfillerRoutine = null;
            atkSpeedBarObj.GetComponent<Slider>().value = 0;
            timeToAttackEnemyText.text = pD.atkSpeed.ToString("F2") + "s";
        }
    }

    private IEnumerator AtkOverlayPopup()
    {
        whenAttackOverLay.SetActive(true);
        basicSkillSlot.GetComponent<Image>().color = new(0 / 255f, 46 / 255f, 0 / 255f, 255f);
        yield return new WaitForSeconds(0.1f);
        basicSkillSlot.GetComponent<Image>().color = new(103 / 255f, 103 / 255f, 103 / 255f, 255f);
        whenAttackOverLay.SetActive(false);
        StopAtkOverlayPopup();
    }     

    private void CanIAttackAndWhatDmgDoIDeal(Skill script)
    {
        StartCoroutine(AtkOverlayPopup());
               

        if (Calucations.ReturnHitOrMissPercentBool(pD.hitChance, eD.enemyEvasion))
        {
            int dmg = (int)Mathf.Ceil(pD.Atk() * (0.0069f * script.dmgMultyerHp + 0.0025f * (script.dmgMultyerMp - 100) + 1f));

            mS.AttacksEnemy(Calucations.CalculateDmg(dmg,eD.enemyDefence));
        }
        else { mS.MSAddText("Missed", Color.white); }
    }      
}
