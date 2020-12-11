using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class BuildManager : MonoBehaviour
{
    public TurretData gunner1TurretData, laser1TurretData, rocket1TurretData, shock1turretData,
        gunner2TurretData, laser2TurretData, rocket2TurretData, shock2turretData,
        gunner3TurretData, laser3TurretData, rocket3TurretData, shock3turretData;
    private TurretData selectedTD;
    private TurretData previewTD;

    /*private bool turretSelected = false;*/
    private enum TURRETS { Gunner, Laser, Rocket, Shock, None }
    private TURRETS turretSelected;
    public EventSystem eventSystem;
    public GameObject gunnerToggle;
    public GameObject rocketToggle;
    public GameObject laserToggle;
    public GameObject shockToggle;

    private GameObject gunnerTier;
    private GameObject rocketTier;
    private GameObject laserTier;
    private GameObject shockTier;

    private GameObject gunnerCost;
    private GameObject rocketCost;
    private GameObject laserCost;
    private GameObject shockCost;

    private int turretTier;

    Camera mainCam;
    Ray ray;
    RaycastHit hit;
    GameObject previewBuild;
    /*    private int money = 1000;//small game or we need a datafile*/
    public int money = PlayerResources.GetEssence();
    
    public Text moneyText;

    public Animator moneyAnimator;
    public AlarmAudio alarmAudio;
    private void Start()
    {

        /*    gunnerToggle = GameObject.Find("GunnerTurret");
            rocketToggle = GameObject.Find("RocketTurret");
            laserToggle = GameObject.Find("LaserTurret");
            shockToggle = GameObject.Find("ShockTurret");*/

        // Hide all turret tier and price labels
        gunnerTier = GameObject.Find("GunnerTurretLevel");
        laserTier = GameObject.Find("LaserTurretLevel");
        rocketTier = GameObject.Find("RocketTurretLevel");
        shockTier = GameObject.Find("ShockTurretLevel");

        gunnerCost = GameObject.Find("GunnerTurretCost");
        laserCost = GameObject.Find("LaserTurretCost");
        rocketCost = GameObject.Find("RocketTurretCost");
        shockCost = GameObject.Find("ShockTurretCost");

        gunnerTier.SetActive(false);
        laserTier.SetActive(false);
        rocketTier.SetActive(false);
        shockTier.SetActive(false);

        gunnerCost.SetActive(false);
        laserCost.SetActive(false);
        rocketCost.SetActive(false);
        shockCost.SetActive(false);




        mainCam =Camera.main;
        ray = mainCam.ScreenPointToRay(Input.mousePosition);

        turretSelected = TURRETS.None;
        /*        gunnerToggle.SetActive(true);
                laserToggle.SetActive(false);
                rocketToggle.SetActive(false);
                shockToggle.SetActive(false);*/
/*        gunnerToggle.GetComponent<Image>().enabled = false;
        laserToggle.GetComponent<Image>().enabled = false;
        rocketToggle.GetComponent<Image>().enabled = false;
        shockToggle.GetComponent<Image>().enabled = false;*/
    }


    public void UpdateMoney(int changeNum = 0)
    {
        money += changeNum;
        PlayerResources.UseEssences(-changeNum);
/*        moneyText.text = "$ " + money;*/
    }
    
    void Update()
    {
        ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {   //building turret
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit2;
                bool isCollider = Physics.Raycast(ray2, out hit2, 100000, LayerMask.GetMask("Ground"));
                if (isCollider)
                {
                    //

                    if (selectedTD != null )//should check for avaliable 
                    {
                        //empty and ready to build
                        if (money - selectedTD.cost >= 0)
                        {
                            //build turret here
                            BuildTurret(selectedTD.turretPrefab);
                            UpdateMoney(-selectedTD.cost);
                        }
                        else
                        {
                            //TODO notice player that no enough money e.g. make a alarm noice or floating text
                            //moneyAnimator.SetTrigger("TextAni");
                            //alarmAudio.AlarmSound();
                        }
                    }
                    //else if (mapCube.turretBuilding != null)   //here we should check if there is already a building
                    //{
                    //    //TODO upgrade
                    //}
                }
            }
        }

        if (selectedTD != null)
        {
            if (previewTD!= selectedTD)
            {
                previewTD = selectedTD;
                if (previewBuild != null)
                {
                    Destroy(previewBuild);
                }

                if (Physics.Raycast(ray, out hit, 100000, LayerMask.GetMask("Ground")))
                {
                        previewBuild = Instantiate(previewTD.previewPrefab, hit.point, Quaternion.identity);
                }
            }
            //if (previewBuild==null)
            //{
            //    if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Ground")))
            //    {
            //        previewBuild = Instantiate(selectedTD.turretPrefab, hit.point, Quaternion.identity);
            //    }
            //}
            else
            {
                if (Physics.Raycast(ray, out hit, 100000, LayerMask.GetMask("Ground")))
                {
                    if (previewBuild!=null)
                    {
                        previewBuild.transform.position = hit.point;
                    }
                    else
                    {
                        previewBuild = Instantiate(previewTD.previewPrefab, hit.point, Quaternion.identity);
                    }
                    
                }
            }
        }
        else
        {
            if (previewBuild!=null)
            {
                Destroy(previewBuild);
            }
            
            previewBuild = null;
        }

        money = PlayerResources.GetEssence();

        // By default pressing the 1234 keys will get you the tier 1 turrets
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectGunner(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectLaser(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectRocket(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectShock(1);
        }
    }
    public void onGunnerTurretSelected(bool isOn)
    {
        if (isOn)
        {
            if (turretTier == 1)
            {
                selectedTD = gunner1TurretData;
                gunnerTier.SetActive(true);
                gunnerTier.GetComponent<TextMeshProUGUI>().text = "1";
                gunnerCost.SetActive(true);
                gunnerCost.GetComponent<TextMeshProUGUI>().text = "£" + gunner1TurretData.cost.ToString();
            }
            if (turretTier == 2)
            {
                selectedTD = gunner2TurretData;
                gunnerTier.SetActive(true);
                gunnerTier.GetComponent<TextMeshProUGUI>().text = "2";
                gunnerCost.SetActive(true);
                gunnerCost.GetComponent<TextMeshProUGUI>().text = "£" + gunner2TurretData.cost.ToString();
            }
            if (turretTier == 3)
            {
                selectedTD = gunner3TurretData;
                gunnerTier.SetActive(true);
                gunnerTier.GetComponent<TextMeshProUGUI>().text = "3";
                gunnerCost.SetActive(true);
                gunnerCost.GetComponent<TextMeshProUGUI>().text = "£" + gunner3TurretData.cost.ToString();
            }
        }
        else
        {
            selectedTD = null;
            gunnerTier.SetActive(false);
            gunnerCost.SetActive(false);
        }
    }
    public void onRocketTurretSelected(bool isOn)
    {
        if (isOn)
        {
            if (turretTier == 1) { 
                selectedTD = rocket1TurretData;
                rocketTier.SetActive(true);
                rocketTier.GetComponent<TextMeshProUGUI>().text = "1";
                rocketCost.SetActive(true);
                rocketCost.GetComponent<TextMeshProUGUI>().text = "£" + rocket1TurretData.cost.ToString();
            }
            if (turretTier == 2) { 
                selectedTD = rocket2TurretData;
                rocketTier.SetActive(true);
                rocketTier.GetComponent<TextMeshProUGUI>().text = "2";
                rocketCost.SetActive(true);
                rocketCost.GetComponent<TextMeshProUGUI>().text = "£" + rocket2TurretData.cost.ToString();
            }
            if (turretTier == 3) { 
                selectedTD = rocket3TurretData;
                rocketTier.SetActive(true);
                rocketTier.GetComponent<TextMeshProUGUI>().text = "3";
                rocketCost.SetActive(true);
                rocketCost.GetComponent<TextMeshProUGUI>().text = "£" + rocket3TurretData.cost.ToString();
            }
        }
        else
        {
            selectedTD = null;
            rocketTier.SetActive(false);
            rocketCost.SetActive(false);
        }

    }
    public void onLaserTurretSelected(bool isOn)
    {
        if (isOn)
        {
            if (turretTier == 1)
            {
                selectedTD = laser1TurretData;
                laserTier.SetActive(true);
                laserTier.GetComponent<TextMeshProUGUI>().text = "1";
                laserCost.SetActive(true);
                laserCost.GetComponent<TextMeshProUGUI>().text = "£" + laser1TurretData.cost.ToString();
            }
            if (turretTier == 2) { 
                selectedTD = laser2TurretData;
                laserTier.SetActive(true);
                laserTier.GetComponent<TextMeshProUGUI>().text = "2";
                laserCost.SetActive(true);
                laserCost.GetComponent<TextMeshProUGUI>().text = "£" + laser2TurretData.cost.ToString();
            }
            if (turretTier == 3) { 
                selectedTD = laser3TurretData;
                laserTier.SetActive(true);
                laserTier.GetComponent<TextMeshProUGUI>().text = "3";
                laserCost.SetActive(true);
                laserCost.GetComponent<TextMeshProUGUI>().text = "£" + laser3TurretData.cost.ToString();
            }
        }
        else
        {
            selectedTD = null;
            laserTier.SetActive(false);
            laserCost.SetActive(false);
        }
    }
    public void onShockTurretSelected(bool isOn)
    {
        if (isOn)
        {
            if (turretTier == 1)
            {
                selectedTD = shock1turretData;
                shockTier.SetActive(true);
                shockTier.GetComponent<TextMeshProUGUI>().text = "1";
                shockCost.SetActive(true);
                shockCost.GetComponent<TextMeshProUGUI>().text = "£" + shock1turretData.cost.ToString();
            }
            if (turretTier == 2) { 
                selectedTD = shock2turretData;
                shockTier.SetActive(true);
                shockTier.GetComponent<TextMeshProUGUI>().text = "2";
                shockCost.SetActive(true);
                shockCost.GetComponent<TextMeshProUGUI>().text = "£" + shock2turretData.cost.ToString();
            }
            if (turretTier == 3) { 
                selectedTD = shock3turretData;
                shockTier.SetActive(true);
                shockTier.GetComponent<TextMeshProUGUI>().text = "3";
                shockCost.SetActive(true);
                shockCost.GetComponent<TextMeshProUGUI>().text = "£" + shock3turretData.cost.ToString();
            }
        }
        else
        {
            selectedTD = null;
            shockTier.SetActive(false);
            shockCost.SetActive(false);
        }
    }
    public void BuildTurret(GameObject turretPrefab)
    {
        
        if (Physics.Raycast(ray, out hit, 100000, LayerMask.GetMask("Ground")))
        {
            Debug.Log("we hit " + hit.collider.name + " " + hit.point);

            Instantiate(turretPrefab, hit.point,Quaternion.identity);
            //if did focus 
        }

    }

    public void selectGunner(int tier) 
    {
        turretTier = tier;
        if (turretSelected == TURRETS.Gunner)
        {
            gunnerToggle.GetComponent<Toggle>().isOn = false;
            turretSelected = TURRETS.None;
        }
        else
        {
            gunnerToggle.GetComponent<Toggle>().isOn = true;
            /*                laserToggle.GetComponent<Toggle>().isOn = false;
                            rocketToggle.GetComponent<Toggle>().isOn = false;
                            shockToggle.GetComponent<Toggle>().isOn = false;*/
            turretSelected = TURRETS.Gunner;
            /*             gunnerToggle.SetActive(true);
                            laserToggle.SetActive(false);
                            rocketToggle.SetActive(false);
                            shockToggle.SetActive(false);*/
        }

    }

    public void selectLaser(int tier) 
    {
        turretTier = tier;
        if (turretSelected == TURRETS.Laser)
        {
            laserToggle.GetComponent<Toggle>().isOn = false;
            turretSelected = TURRETS.None;
        }
        else
        {
            laserToggle.GetComponent<Toggle>().isOn = true;
            turretSelected = TURRETS.Laser;
            /*                laserToggle.SetActive(true);
                            gunnerToggle.SetActive(false);
                            rocketToggle.SetActive(false);
                            shockToggle.SetActive(false);*/
        }
    }

    public void selectRocket(int tier) 
    {
        turretTier = tier;
        if (turretSelected == TURRETS.Rocket)
        {
            rocketToggle.GetComponent<Toggle>().isOn = false;
            turretSelected = TURRETS.None;
        }
        else
        {
            rocketToggle.GetComponent<Toggle>().isOn = true;
            turretSelected = TURRETS.Rocket;
                           /* laserToggle.SetActive(false);
                            gunnerToggle.SetActive(false);
                            rocketToggle.SetActive(true);
                            shockToggle.SetActive(false);*/
        }
    }

    public void selectShock(int tier) 
    {
        turretTier = tier;
        if (turretSelected == TURRETS.Shock)
        {
            shockToggle.GetComponent<Toggle>().isOn = false;
            turretSelected = TURRETS.None;
        }
        else
        {
            shockToggle.GetComponent<Toggle>().isOn = true;
            turretSelected = TURRETS.Shock;
            /*                laserToggle.SetActive(false);
                            gunnerToggle.SetActive(false);
                            rocketToggle.SetActive(false);
                            shockToggle.SetActive(true);*/
        }
    }
}
