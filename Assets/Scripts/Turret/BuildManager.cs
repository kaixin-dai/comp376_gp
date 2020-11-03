using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
                    //
                    if (selectedTD != null )//should check for avaliable 
                    {
                        //empty and ready to build
                        if (money - selectedTD.cost >= 0)
                        {
                            //build turret here
                            UpdateMoney(-selectedTD.cost);
                        }
                        else
                        {
                            //TODO notice player that no enough money e.g. make a alarm noice or floating text
                            moneyAnimator.SetTrigger("NotEnoughMoneyFlick");
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
