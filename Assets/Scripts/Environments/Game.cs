using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // Start is called before the first frame update
    float time = 0.0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;
        if(time > 10){
            GetComponent<Sun>().StartDay();
            time = 0;
            return;
        }
    }
}
