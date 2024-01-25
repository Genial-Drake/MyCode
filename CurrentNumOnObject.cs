using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CurrentNumOnObject : MonoBehaviour
{
    public int currentAmount;
    

    public void TextAmount(int num, int maxValue)
    {
        currentAmount = Mathf.Clamp(currentAmount + num, 0, maxValue);        
        this.GetComponent<TextMeshProUGUI>().text = currentAmount.ToString();        
    }
}
