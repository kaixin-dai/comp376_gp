using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPrize : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject EssenceReference;
    public GameObject Player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropPrize(Vector3 location)
    {
        GameObject essence = Instantiate(EssenceReference, transform.position, Quaternion.identity);
        essence.GetComponent<Essence>().SetAmount(300);
    }
}
