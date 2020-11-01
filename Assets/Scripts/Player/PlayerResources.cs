using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    // Start is called before the first frame update

    public int mNumOfEssences;

    void Start()
    {
        mNumOfEssences = 0;
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
