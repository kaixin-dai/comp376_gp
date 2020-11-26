using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{
    [SerializeField]
    public TurretData gunnerTurretData, rocketTurretData, laserTurretData, shockturretData;
    [SerializeField]
    private TurretData selectedTD;
    private TurretData previewTD;
    Camera mainCam;
    Ray ray;
    RaycastHit hit;
    GameObject previewBuild;
    private int money = 1000;//small game or we need a datafile
    public Text moneyText;

    public Animator moneyAnimator;
    public AlarmAudio alarmAudio;
    private void Start()
    {
        mainCam=Camera.main;
        ray = mainCam.ScreenPointToRay(Input.mousePosition);
    }
   
    

    public void UpdateMoney(int changeNum = 0)
    {
        money += changeNum;
        moneyText.text = "$ " + money;
    }
    
    void Update()
    {
        ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {   //building turret
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray2, out hit, 1000, LayerMask.GetMask("Ground"));
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
                Destroy(previewBuild);
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
                    previewBuild.transform.position = hit.point;
                }
            }
        }
        else
        {
            Destroy(previewBuild);
            previewBuild = null;
        }
    }
    public void onGunnerTurretSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTD = gunnerTurretData;
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
            selectedTD = rocketTurretData;
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
            selectedTD = laserTurretData;
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
            selectedTD = shockturretData;
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
}
