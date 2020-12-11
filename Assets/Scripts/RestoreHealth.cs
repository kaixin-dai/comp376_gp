using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreHealth : MonoBehaviour
{

    public GameObject Ship;

    void Start()
    {
        Ship = GameObject.Find("Ship");
    }
 
    public void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            Ship.GetComponent<Health>().ResetHealth();
            Destroy(gameObject);
        }

        
    }
}
