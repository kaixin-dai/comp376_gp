using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResources : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public int mNumOfEssences;

    void Start()
    {
        mNumOfEssences = 0;
    }

    public void Update()
    {
        GameObject.Find("EssenceAmount").GetComponent<Text>().text = mNumOfEssences.ToString();
    }

    public void AddEssences(int num)
    {
        print("player + " + num + "essence");

        mNumOfEssences += num;
    }

    public void UseEssences(int num)
    {
        mNumOfEssences -= num;

    }
}
