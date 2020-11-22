using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform BaseLocation;
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
        
    }



    public void ToBase()
    {
        ChangeLocation(BaseLocation.position);

    }

    private void ChangeLocation(Vector3 location)
    {   
       

        mCharacterController.enabled = false;
        mCharacterController.transform.position = new Vector3(location.x,location.y,location.z);
        mCharacterController.enabled = true;

        MainCamera.transform.position = mCharacterController.transform.position + mCameraOffset;


    }
}
