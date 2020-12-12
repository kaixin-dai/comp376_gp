using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform BaseLocation;
    public Transform WestLocation;
    public Transform SouthLocation;
    public Transform NorthLocation;
    public GameObject Player;
    public CharacterController mCharacterController; 
    public GameObject MainCamera;

    float orinialCameraHeight;
    Vector3 mCameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        MainCamera = GameObject.Find("Main Camera");
        mCharacterController = Player.GetComponent<CharacterController>();
        orinialCameraHeight = MainCamera.transform.position.y;
        mCameraOffset = MainCamera.transform.position - mCharacterController.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            ToBase();
        }

        if(Input.GetKeyDown(KeyCode.H))
        {
            ToWest();
        }

        if(Input.GetKeyDown(KeyCode.G))
        {
            ToSouth();
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            ToNorth();
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            GameManager.OnDay();
            GameManager.OnDayAfter();
        }

        
    }



    public void ToBase()
    {
        ChangeLocation(BaseLocation.position);

    }

    public void ToWest()
    {
        ChangeLocation(WestLocation.position);
    }

    public void ToSouth()
    {
        ChangeLocation(SouthLocation.position);
    }

    public void ToNorth()
    {
        ChangeLocation(NorthLocation.position);
    }



    private void ChangeLocation(Vector3 location)
    {   
       

        mCharacterController.enabled = false;
        mCharacterController.transform.position = new Vector3(location.x,location.y,location.z);
        mCharacterController.enabled = true;

        MainCamera.transform.position = mCharacterController.transform.position + mCameraOffset;


    }
}
