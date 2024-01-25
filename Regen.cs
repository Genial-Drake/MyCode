using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regen : GetManagers
{
    [SerializeField] private Transform popUpObjHp; // for shwoing hp has recorved
    [SerializeField] private Transform popUpObjMp; // for shwoing Mp has recorved

    private Coroutine hpRegenCo = null;
    private Coroutine mpRegenCo = null;
    private Coroutine meditateRoutine = null;

    public void StartMeditateRoutine() => meditateRoutine ??= StartCoroutine(StartMeditating());
    public void StopMeditateRoutine() { if (meditateRoutine != null) { StopCoroutine(meditateRoutine); meditateRoutine = null; } }
    public bool IsMeditating() { if (meditateRoutine != null) { return true; } else { return false; } }

    public void StartHpCo() => hpRegenCo ??= StartCoroutine(HpRegen());
    public void StopHpCo() { if (hpRegenCo != null) { StopCoroutine(hpRegenCo); hpRegenCo = null; } }

    public void StartMpCo() => mpRegenCo ??= StartCoroutine(MpRegen());
    public void StopMpCo() { if (mpRegenCo != null) { StopCoroutine(mpRegenCo); mpRegenCo = null; } }

    private IEnumerator HpRegen()
    {
        while (true)
        {
            yield return new WaitForSeconds(6f);
            mS.HealPlayerMS((int)Mathf.Ceil(pD.playerMaxHealth / 10f),"regen");
            popUpObjHp.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            popUpObjHp.gameObject.SetActive(false);
        }
    }

    private IEnumerator MpRegen()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            mS.MSPlayerHealedOrUsedMp((int)Mathf.Ceil(pD.playerMaxMana / 10f));
            popUpObjMp.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            popUpObjMp.gameObject.SetActive(false);
        }
    }

    private IEnumerator StartMeditating()
    {
        mS.MSAddText("Started Meditating", Color.blue);
        while (true)
        {
            mS.MSPlayerHealedOrUsedMp((int)Mathf.Ceil(pD.playerMaxMana / 10));

            if (pD.playerCurrentMp >= pD.playerMaxMana) { break; }

            // maybe give a pop up showing healing

            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
        if (mS.MSEnemyRespawning() == false) { mS.StartPlayerAtkFunction(); }

        yield return new WaitForSeconds(0.1f);
        StopMeditateRoutine();
    }
}
