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
    public static Del OnShipDestoryed;
    public static Del OnPlayerDied;
    public static Del OnPickUpEssence;
    public static Del OnTakeDamage;


    void Awake()
    {   
        GameManager.OnDay += Day;
        GameManager.OnNight += Night;
        GameManager.OnStartGame += StartGame;
        GameManager.OnPlayerDied += PlayerDied;
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

    public void Day(){
        print("day");
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


}
