using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreHealth : MonoBehaviour
{
 
    public void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            collider.gameObject.GetComponent<Health>().ResetHealth();
            Destroy(gameObject);
        }

        
    }
}
