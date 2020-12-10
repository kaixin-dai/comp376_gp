using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerResources : MonoBehaviour
{
    // Start is called before the first frame update

    public static int mNumOfEssences = 1000;
    public GameObject essenceDisplay;

    void Start()
    {
        mNumOfEssences = 1000;
    }

    public void Update()
    {
        essenceDisplay.GetComponent<TextMeshProUGUI>().text = mNumOfEssences.ToString();
    }

    public void AddEssences(int num)
    {
        print("player + " + num + "essence");

        mNumOfEssences += num;
    }

    public static void UseEssences(int num)
    {
        mNumOfEssences -= num;

    }

    public static int GetEssence() 
    {
        return mNumOfEssences;
    }
}
