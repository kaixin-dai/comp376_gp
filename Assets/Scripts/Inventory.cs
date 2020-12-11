using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Inventory : MonoBehaviour
{

    public GameObject player;

    public GameObject inventory;
    public GameObject HUD;
    public GameObject essenceHUD;

    public Button buildButton;
    public Button weaponButton;
    public Button mapButton;

    public GameObject buildPanel;
    public GameObject weaponPanel;
    public GameObject mapPanel;

    // Build Panel
    public Button gunner1;
    public int gunner1Cost;
    public Button gunner2;
    public int gunner2Cost;
    public Button gunner3;
    public int gunner3Cost;

    public Button laser1;
    public Button laser2;
    public Button laser3;

    public Button rocket1;
    public Button rocket2;
    public Button rocket3;

    public Button shock1;
    public Button shock2;
    public Button shock3;

    // Weapon Panel 
    public Button melee1;
    public Button melee2;
    public Button melee3;

    public Button gun1;
    public Button gun2;
    public Button gun3;

    public void Melee1() 
    {
        // Attack
        player.GetComponent<Melee>().mDamage = 1;
        // Speed
        player.GetComponent<Melee>().mAttackRate = 0.5f;
        // Range
        player.GetComponent<Melee>().mAttackRadius = 0.5f;
    }

    public void Melee2()
    {
        // Attack
        player.GetComponent<Melee>().mDamage = 2;
        // Speed
        player.GetComponent<Melee>().mAttackRate = 1f;
        // Range
        player.GetComponent<Melee>().mAttackRadius = 0.75f;
    }

    public void Melee3()
    {
        // Attack
        player.GetComponent<Melee>().mDamage = 3;
        // Speed
        player.GetComponent<Melee>().mAttackRate = 1.5f;
        // Range
        player.GetComponent<Melee>().mAttackRadius = 1f;
    }

    public void BuildMenu()
    {
        buildPanel.gameObject.SetActive(true);
        weaponPanel.gameObject.SetActive(false);
        mapPanel.gameObject.SetActive(false);
    }

    public void WeaponMenu()
    {
        buildPanel.gameObject.SetActive(false);
        weaponPanel.gameObject.SetActive(true);
        mapPanel.gameObject.SetActive(false);
    }

    public void MapMenu()
    {
        buildPanel.gameObject.SetActive(false);
        weaponPanel.gameObject.SetActive(false);
        mapPanel.gameObject.SetActive(true);
    }

    // Turret buttons

    public void Gunner1() 
    {
        GameObject.Find("GameFlow").GetComponent<GameFlow>().ResumeGame();
        GameObject.Find("BuildManager").GetComponent<BuildManager>().selectGunner(1);
    }
    public void Gunner2()
    {
        GameObject.Find("GameFlow").GetComponent<GameFlow>().ResumeGame();
        GameObject.Find("BuildManager").GetComponent<BuildManager>().selectGunner(2);
    }
    public void Gunner3()
    {
        GameObject.Find("GameFlow").GetComponent<GameFlow>().ResumeGame();
        GameObject.Find("BuildManager").GetComponent<BuildManager>().selectGunner(3);
    }
    public void Laser1()
    {
        GameObject.Find("GameFlow").GetComponent<GameFlow>().ResumeGame();
        GameObject.Find("BuildManager").GetComponent<BuildManager>().selectLaser(1);
    }
    public void Laser2()
    {
        GameObject.Find("GameFlow").GetComponent<GameFlow>().ResumeGame();
        GameObject.Find("BuildManager").GetComponent<BuildManager>().selectLaser(2);
    }
    public void Laser3()
    {
        GameObject.Find("GameFlow").GetComponent<GameFlow>().ResumeGame();
        GameObject.Find("BuildManager").GetComponent<BuildManager>().selectLaser(3);
    }
    public void Rocket1()
    {
        GameObject.Find("GameFlow").GetComponent<GameFlow>().ResumeGame();
        GameObject.Find("BuildManager").GetComponent<BuildManager>().selectRocket(1);
    }
    public void Rocket2()
    {
        GameObject.Find("GameFlow").GetComponent<GameFlow>().ResumeGame();
        GameObject.Find("BuildManager").GetComponent<BuildManager>().selectRocket(2);
    }
    public void Rocket3()
    {
        GameObject.Find("GameFlow").GetComponent<GameFlow>().ResumeGame();
        GameObject.Find("BuildManager").GetComponent<BuildManager>().selectRocket(3);
    }
    public void Shock1()
    {
        GameObject.Find("GameFlow").GetComponent<GameFlow>().ResumeGame();
        GameObject.Find("BuildManager").GetComponent<BuildManager>().selectShock(1);
    }
    public void Shock2()
    {
        GameObject.Find("GameFlow").GetComponent<GameFlow>().ResumeGame();
        GameObject.Find("BuildManager").GetComponent<BuildManager>().selectShock(2);
    }
    public void Shock3()
    {
        GameObject.Find("GameFlow").GetComponent<GameFlow>().ResumeGame();
        GameObject.Find("BuildManager").GetComponent<BuildManager>().selectShock(3);
    }
    // Start is called before the first frame update
    void Start()
    {
        /*DontDestroyOnLoad(GameObject.Find("Inventory"));*/
        buildPanel.gameObject.SetActive(true);
        weaponPanel.gameObject.SetActive(false);
        mapPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
