using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public static Del OnPlayerSpawn;
    public static Del OnPlayerSpawnLate;
    // 
    public static Del OnPickUpEssence;
    public static Del OnTakeDamage;

    public static Del OnGameWon;

    public static Del OnCrazyMode;
    public static Del OnEndCrazyMode;


    public static int dayCounter = 1;
    
    bool playerDead;
    public float PlayerSpwanTime = 5.0f;
    public float SapwnCounter;




    public GameObject GameOverPanel;
    public GameObject GameWonPanel;

    public GameObject PlayerReference;
    public Text Responwe;
    

    

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
        GameManager.OnPlayerSpawn += InstantiatePlayer;
        

        

    }
    void Start()
    {
        Responwe = GameObject.Find("PlayerRespawnMessage").GetComponent<Text>();
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

        if(playerDead)
        {
            if(SapwnCounter < PlayerSpwanTime)
            {
                UpdateSpwandCounter();
                SapwnCounter += Time.deltaTime;
            }
            else
            {
                SapwnCounter = 0.0f;
                playerDead = false;
                GameManager.OnPlayerSpawn();
                GameManager.OnPlayerSpawnLate();

            }
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

    public void PlayerDied()
    {
        playerDead = true;
    }

    public void InstantiatePlayer()
    {
        print("player instantialed");
        Instantiate(PlayerReference, new Vector3(21.6f,-17.7f,-21.1f), Quaternion.identity);
    }

    public void UpdateSpwandCounter()
    {
        Responwe.text = "You Are Killed\n" + (int)SapwnCounter;
    }






}
