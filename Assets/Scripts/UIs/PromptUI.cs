using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptUI : MonoBehaviour
{


    public Text DayPrompt;
    public Text NightPrompt;
    public Text CrazyMode;
    public Text Responwe;

    public GameObject GameOverPanel;
    public GameObject GameWonPanel;

    Animator EssenceAnimator;
    Animator DamageAnimator;
    Animator DayAnimator;
    Animator NightAnimator;
     
    // Start is called before the first frame update
    void Awake()
    {
        CrazyMode = GameObject.Find("CrazyMode").GetComponent<Text>();
        Responwe = GameObject.Find("PlayerRespawnMessage").GetComponent<Text>();
        GameOverPanel = GameObject.Find("GameOver");
        GameWonPanel = GameObject.Find("GameWon");
        EssenceAnimator = this.transform.Find("Essence Prompt").GetComponent<Animator>();
        DamageAnimator = this.transform.Find("Damage Prompt").GetComponent<Animator>();
        DayAnimator = this.transform.Find("Day Prompt").GetComponent<Animator>();
        NightAnimator = this.transform.Find("Night Prompt").GetComponent<Animator>();
        
        GameManager.OnPickUpEssence += PickUpEssenceMessage;
        GameManager.OnTakeDamage += GetDamageMessage;
        GameManager.OnDayAfter += DayMessage;
        GameManager.OnNight += NightMessage;
        GameManager.OnShipDestoryed += GameOverMessage;
        GameManager.OnGameWon += GameWonMessage;
        GameManager.OnCrazyMode += ActiveCrazyMode;
        GameManager.OnEndCrazyMode += InActiveCrazyMode;
        GameManager.OnPlayerDied += ActiveRespawe;
        GameManager.OnPlayerSpawn += InActiveRespawe;

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
        print(GameManager.dayCounter);
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

    public void ActiveCrazyMode()
    {
        Color zm = CrazyMode.color;
        zm.a = 1.0f;
        CrazyMode.color = zm;
    }

    public void InActiveCrazyMode()
    {
        Color zm = CrazyMode.color;
        zm.a = 0.0f;
        CrazyMode.color = zm;
    }

    public void ActiveRespawe()
    {
        Color zm = Responwe.color;
        zm.a = 1.0f;
        Responwe.color = zm;
    }


    public void InActiveRespawe()
    {
        Color zm = Responwe.color;
        zm.a = 0.0f;
        Responwe.color = zm;
    }








}
