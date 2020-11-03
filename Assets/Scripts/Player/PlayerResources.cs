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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEssences(int num)
    {
        mNumOfEssences += num;
    }

    public void UseEssences(int num)
    {
        mNumOfEssences -= num;

    }
}
