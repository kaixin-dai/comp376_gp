using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public delegate void Del();
    public static Del OnStartGame;
	public static Del OnDay;
    public static Del OnNight;
    //no used yet
    public static Del OnShipDestoryed;
    public static Del OnPlayerDied;
    // 
    public static Del OnPickUpEssence;
    public static Del OnTakeDamage;

    public static int dayCounter;

    public GameObject GameOverPanel;


    void Awake()
    {  
        GameOverPanel = GameObject.Find("GameOver");
        GameManager.OnStartGame += GameOverPanelOff;
        GameManager.OnDay += IncreaseDayCounter;
        GameManager.OnNight += Night;
        GameManager.OnStartGame += StartGame;
        GameManager.OnPlayerDied += PlayerDied;
        GameManager.OnShipDestoryed += Reset;

    }
    void Start()
    {
        GameManager.OnStartGame();
        GameManager.OnDay();
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void IncreaseDayCounter()
    {
        dayCounter = dayCounter + 1;
    }

    public void Night(){
        print("night");
    }

    public void StartGame(){
        print("startGame");
    }

    public void PlayerDied()
    {
        print("PlayerDied");
    }

    public void Reset()
    {
        dayCounter = 0;
    }

    public void GameOverPanelOff()
    {
        GameOverPanel.SetActive(false);
    }




}
