using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public TurretData GreenTurretData;
    public TurretData TorbTurretData;
    public TurretData lavaTurretData;

    private int money = 1000;//small game or we need a datafile
    public Text moneyText;

    public Animator moneyAnimator;
    public AlarmAudio alarmAudio;

    public void UpdateMoney(int changeNum = 0)
    {
        money += changeNum;
        moneyText.text = "$ " + money;
    }
    private TurretData selectedTD;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {   //building turret
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if (selectedTD != null && mapCube.turretBuilding == null)
                    {
                        //empty and ready to build
                        if (money - selectedTD.cost >= 0)
                        {
                            mapCube.BuildTurret(selectedTD.turretPrefab);
                            UpdateMoney(-selectedTD.cost);
                        }
                        else
                        {
                            //TODO notice player that no enough money e.g. make a alarm noice or floating text
                            moneyAnimator.SetTrigger("NotEnoughMoneyFlick");
                            alarmAudio.AlarmSound();
                        }
                    }
                    else if (mapCube.turretBuilding != null)
                    {
                        //TODO upgrade
                    }
                }
            }
        }
    }
    public void onGreenTurretSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTD = GreenTurretData;
        }
    }
    public void onTorbTurretSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTD = TorbTurretData;
        }

    }
    public void onLavaTurretSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTD = lavaTurretData;
        }
    }
}
