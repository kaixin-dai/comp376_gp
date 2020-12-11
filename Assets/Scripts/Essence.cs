using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Essence : MonoBehaviour
{
    // Start is called before the first frame update
    
    public int mAmount = 0;

    [SerializeField]
    GameObject mPlayer;

    GameObject EssencePrompt;
    Text EssnecePromptText;

    void Start()
    {   
        mPlayer = GameObject.Find("Player");
        EssencePrompt = GameObject.Find("Essence Prompt");
        EssnecePromptText = EssencePrompt.GetComponent<Text>();

    }


    public void Interact()
    {
        mPlayer.GetComponent<PlayerResources>().AddEssences(mAmount);
        EssnecePromptText.text = " + " + mAmount +" Essence";
        GameManager.OnPickUpEssence();
        Destroy(this.gameObject);
    }

    public void SetAmount(int amount)
    {
        mAmount = amount;
    }

}
