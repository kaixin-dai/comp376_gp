using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    public GameObject gunnerToggle;
    public GameObject rocketToggle;
    public GameObject laserToggle;
    public GameObject shockToggle;

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


    mainCam=Camera.main;
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
                bool isCollider = Physics.Raycast(ray2, out hit2, 1000, LayerMask.GetMask("Ground"));
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
                            moneyAnimator.SetTrigger("TextAni");
                            alarmAudio.AlarmSound();
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

                if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Ground")))
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
                if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Ground")))
                {
                    if (previewBuild!=null)
                    {
                        previewBuild.transform.position = hit.point;
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
                selectedTD = gunner1TurretData;
            if (turretTier == 2)
                selectedTD = gunner2TurretData;
            if (turretTier == 3)
                selectedTD = gunner3TurretData;
        }
        else
        {
            selectedTD = null;
        }
    }
    public void onRocketTurretSelected(bool isOn)
    {
        if (isOn)
        {
            if (turretTier == 1)
                selectedTD = rocket1TurretData;
            if (turretTier == 2)
                selectedTD = rocket3TurretData;
            if (turretTier == 3)
                selectedTD = rocket3TurretData;
        }
        else
        {
            selectedTD = null;
        }

    }
    public void onLaserTurretSelected(bool isOn)
    {
        if (isOn)
        {
            if (turretTier == 1)
                selectedTD = laser1TurretData;
            if (turretTier == 2)
                selectedTD = laser2TurretData;
            if (turretTier == 3)
                selectedTD = laser3TurretData;
        }
        else
        {
            selectedTD = null;
        }
    }
    public void onShockTurretSelected(bool isOn)
    {
        if (isOn)
        {
            if (turretTier == 1)
                selectedTD = shock1turretData;
            if (turretTier == 2)
                selectedTD = shock2turretData;
            if (turretTier == 3)
                selectedTD = shock3turretData;
        }
        else
        {
            selectedTD = null;
        }
    }
    public void BuildTurret(GameObject turretPrefab)
    {
        
        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Ground")))
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
            /*                turretSelected = TURRETS.Rocket;
                            laserToggle.SetActive(false);
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
