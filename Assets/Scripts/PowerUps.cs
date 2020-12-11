using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    // Start is called before the first frame update



    public void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            collider.gameObject.GetComponent<Gun>().Enpower();
            Destroy(gameObject);
        }

        
    }
}
