using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Essence : MonoBehaviour
{
    // Start is called before the first frame update
    
    public int mAmount = 0;

    [SerializeField]
    GameObject mPlayer;


    public void Interact()
    {
        mPlayer.GetComponent<PlayerResources>().AddEssences(mAmount);
        Destroy(this.gameObject);
    }

}
