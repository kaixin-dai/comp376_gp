using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCamera : MonoBehaviour
{
    GameObject MainCamera;
    GameObject BaseCamera1;
    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.Find("Main Camera");
        BaseCamera1 = GameObject.Find("BaseCamera");
        BaseCamera1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            print("O");
            if(MainCamera.active)
            {
                MainCamera.SetActive(false);
                BaseCamera1.SetActive(true);
            }
            else
            {
                MainCamera.SetActive(true);
                BaseCamera1.SetActive(false);
            }

        }
    }
}
