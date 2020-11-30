using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptUI : MonoBehaviour
{


    public Text DayPrompt;
    public Text NightPrompt;

    public GameObject GameOverPanel;
    public GameObject GameWonPanel;

    Animator EssenceAnimator;
    Animator DamageAnimator;
    Animator DayAnimator;
    Animator NightAnimator;
     
    // Start is called before the first frame update
    void Awake()
    {
        GameOverPanel = GameObject.Find("GameOver");
        GameWonPanel = GameObject.Find("GameWon");
        EssenceAnimator = this.transform.Find("Essence Prompt").GetComponent<Animator>();
        DamageAnimator = this.transform.Find("Damage Prompt").GetComponent<Animator>();
        DayAnimator = this.transform.Find("Day Prompt").GetComponent<Animator>();
        NightAnimator = this.transform.Find("Night Prompt").GetComponent<Animator>();
        
        GameManager.OnPickUpEssence += PickUpEssenceMessage;
        GameManager.OnTakeDamage += GetDamageMessage;
        GameManager.OnDay += DayMessage;
        GameManager.OnNight += NightMessage;
        GameManager.OnShipDestoryed += GameOverMessage;
        GameManager.OnGameWon += GameWonMessage;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()

    {

        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // RaycastHit hit;
        // if(Physics.Raycast(ray,out hit))
        // {   

        //     print(hit.collider.name);

        // }
    }

    public void PickUpEssenceMessage()
    {
        
        EssenceAnimator.Play("popMessage");
    }

    public void GetDamageMessage()
    {

        DamageAnimator.Play("popMessage");
    }

    public void DayMessage()
    {
        DayPrompt.text = "DAY " + GameManager.dayCounter;
        DayAnimator.Play("popMessage");
    }

    public void NightMessage()
    {
        NightAnimator.Play("popMessage");
    }

    public void GameOverMessage()
    {
        GameOverPanel.SetActive(true);
    }

    public void GameWonMessage()
    {
        print("gamewon panel on");
        GameWonPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        print("to Main Menu");
    }



}
