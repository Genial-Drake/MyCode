using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class RandomColorChanger
{
    public static void RandomColorChangerFunction(GameObject obj)
    {
        Color color = new(Random.Range(0, 255) / 255f, Random.Range(0, 255) / 255f, Random.Range(0, 255) / 255f, 255f);

        obj.transform.GetComponent<Image>().color = color;
    }
}
