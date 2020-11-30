using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{

    [SerializeField]
    float nMinsPerDay;
    float nTotalSeconds;

    float nStart;
    float nEnd;
    float nYOffset;
    float nCurrentX;
    float nTimeCounter;
    bool day;
    [SerializeField]
    Light light;

    float nOriginalIntensity;


    Vector3 nNightRotation;
    [SerializeField]
    float nNightIntensity;

    void Awake()
    {   
        nOriginalIntensity = light.intensity;
        nTimeCounter = 0.0f;
        nStart = 25.0f;
        nEnd = 155.0f;
        nYOffset = -30f; 
        nTotalSeconds = nMinsPerDay * 60.0f;
        nNightRotation = new Vector3(90.0f,-30.0f,0.0f);
        nCurrentX = nStart;
        day = true;

        GameManager.OnDay += StartDay;

    }

    void Update()
    {
        if(day)
        {

            float intropolation = (nEnd - nStart)/nTotalSeconds * Time.deltaTime;
            nCurrentX = nCurrentX + intropolation;  

            if(nTimeCounter >= nTotalSeconds)
            {
                day = false;
                nTimeCounter = 0.0f;
                nCurrentX = nStart;
                light.intensity = nNightIntensity;
                transform.rotation = Quaternion.Euler(nNightRotation);
                GameManager.OnNight();
            }
            else
            {
                
                var currentRotation = new Vector3(nCurrentX, nYOffset, 0.0f);
                transform.rotation = Quaternion.Euler(currentRotation);
                nTimeCounter = nTimeCounter + Time.deltaTime;
            }
          
        }

        else
        {
            
        }






    }


    public void StartDay()
    {
        light.intensity = nOriginalIntensity;
        day = true;
    }


}
