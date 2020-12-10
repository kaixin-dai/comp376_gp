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

    public static Del OnGameWon;

    public static Del OnCrazyMode;
    public static Del OnEndCrazyMode;

    public static int dayCounter = 1;



    public GameObject GameOverPanel;
    public GameObject GameWonPanel;

    public int WinningDay = 7;


    void Awake()
    {  

        GameOverPanel = GameObject.Find("GameOver");
        GameWonPanel = GameObject.Find("GameWon");
        GameManager.OnStartGame += GameOverPanelOff;
        GameManager.OnStartGame += GameWonPanelOff;
        GameManager.OnDay += IncreaseDayCounter;
        GameManager.OnNight += Night;
        GameManager.OnStartGame += StartGame;
        GameManager.OnPlayerDied += PlayerDied;
        GameManager.OnShipDestoryed += Reset;
        GameManager.OnGameWon += Reset;
        GameManager.OnShipDestoryed += PauseGame;
        GameManager.OnGameWon += PauseGame;
        

        

    }
    void Start()
    {
        GameManager.OnStartGame();
        GameManager.OnDay();
    }


    void Update()
    {
        
        if(GameManager.dayCounter == WinningDay)

        {
            print("call game won");
            GameManager.OnGameWon();
            // Time.timeScale = 0;
        }
    }

    public void IncreaseDayCounter()
    {
        print("day");
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

    public void GameWonPanelOff()
    {
        print("gamewon panel off");
        GameWonPanel.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }





}
