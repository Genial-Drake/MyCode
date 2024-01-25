using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddToTextLog : MonoBehaviour
{
    [SerializeField] private int textPlacementNum = 1;
    [SerializeField] private Transform textFolder;
    [SerializeField] private Transform textLine;
    [SerializeField] private Transform textDateAndTime;
    [SerializeField] private List<string> addToTextList = new();
    [SerializeField] private List<string> dateList = new();
    [SerializeField] private List<Color> colorList = new();   

    public void ClearLists() // on new game or load
    {
        addToTextList.Clear();
        dateList.Clear();
        textPlacementNum = 1;
        for (int i = 0; i < textFolder.childCount; i++)
        {
            textFolder.GetChild(i).GetComponent<TextMeshProUGUI>().text = "";
            textLine.GetChild(i).GetComponent<TextMeshProUGUI>().text = "";
            textDateAndTime.GetChild(i).GetComponent<TextMeshProUGUI>().text = "";            
        }
    }

    public void AddText(string text, Color color)
    {
        dateList.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        addToTextList.Add(text);   
        colorList.Add(color);
        TextToDisplayUpdate();
    }

    private void TextToDisplayUpdate()
    {
        int count = addToTextList.Count - textPlacementNum;       

        for (int i = 0; i < textFolder.childCount; i++)
        {
            if (count - i >= 0 && count - i < addToTextList.Count)
            {
                textFolder.GetChild(i).GetComponent<TextMeshProUGUI>().text = addToTextList[count - i];
                textLine.GetChild(i).GetComponent<TextMeshProUGUI>().text = (textPlacementNum + i).ToString();
                textDateAndTime.GetChild(i).GetComponent<TextMeshProUGUI>().text = dateList[count - i];
                textFolder.GetChild(i).GetComponent<TextMeshProUGUI>().color = colorList[count - i];
            }
            else 
            { 
                textFolder.GetChild(i).GetComponent<TextMeshProUGUI>().text = "";
                textLine.GetChild(i).GetComponent<TextMeshProUGUI>().text = "";
                textDateAndTime.GetChild(i).GetComponent<TextMeshProUGUI>().text = "";                
            }                       
        }
    }   

    public void AdjustTextPlacementNum(int change) // button
    {
        textPlacementNum += change;
        textPlacementNum = Mathf.Clamp(textPlacementNum, 1, addToTextList.Count);
        TextToDisplayUpdate(); // Update display after adjustment
    }
}
