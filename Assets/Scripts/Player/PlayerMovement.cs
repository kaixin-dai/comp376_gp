using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float mSpeed;
    [SerializeField]
    float mTurningSpeed;


    bool mRunning;

    Animator mAnimator;
    CharacterController mCharacterController;

    
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponentInChildren<Animator>();
        mCharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        mRunning = false;
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");



        if(horizontal != 0.0f || vertical != 0.0f){
            mRunning = true;

        }
        Vector3 direction = Vector3.ClampMagnitude(new Vector3(horizontal,0.0f,vertical), 1.0f);

        mCharacterController.SimpleMove(direction * Time.deltaTime * mSpeed);
        UpdateAnimation();
        if(direction.magnitude > 0){
            transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.LookRotation(direction),Time.deltaTime * mTurningSpeed);
        }
        


    }


    private void UpdateAnimation()
    {
        mAnimator.SetBool ("IsRunning", mRunning);
    }

    public void ChangeMovementSpeed(int speed)
    {
        mSpeed -= speed;
        if(mSpeed < 0 )
        {
            mSpeed = 0;
        }
    }

    // void OnTriggerEnter()
    // {
    //     print("trigger");
    // }

    // void OnCollisionEnter()
    // {
    //     print("collision");
    // }

    


}
