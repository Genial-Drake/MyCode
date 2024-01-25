
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public class OnButtonPressed : MonoBehaviour
{
    public GameObject previouslyOpenedMenu; // log menu attached before game start or bug!

    private MasterScript mS; private void Awake() => mS = FindObjectOfType<MasterScript>();

    public GameObject createSkill;
    public GameObject oneOrTenOrObj;

    //Opens anything attached as a GameObject to the button,on button pressed.
    public void ButtonPressed(GameObject obj)
    {
        if (previouslyOpenedMenu.name == "SkillsMenu")
        {
            mS.MSReturnGameObjectToBeClosed().SetActive(false);
            createSkill.SetActive(true);
            oneOrTenOrObj.SetActive(false);
        }
        previouslyOpenedMenu.SetActive(false);
        obj.SetActive(true);
        previouslyOpenedMenu = obj;
    }
}
