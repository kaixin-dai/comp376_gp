using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptUI : MonoBehaviour
{

    public Text EssencePrompt;
    public Text DamagePrompt;

    Animator EssenceAnimator;
    Animator DamageAnimator;
    // Start is called before the first frame update
    void Awake()
    {
        EssenceAnimator = this.transform.Find("Essence Prompt").GetComponent<Animator>();
        DamageAnimator = this.transform.Find("Damage Prompt").GetComponent<Animator>();

    }
    void Start()
    {
        GameManager.OnPickUpEssence += PickUpEssenceMessage;
        GameManager.OnTakeDamage += GetDamageMessage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUpEssenceMessage()
    {
        
        EssenceAnimator.Play("popMessage");
    }

    public void GetDamageMessage()
    {

        DamageAnimator.Play("popMessage");
    }
}
