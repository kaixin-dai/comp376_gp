using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float mSpeed;
    [SerializeField]
    float mTurningSpeed;


    [SerializeField]
    [Range(-5.0f, 5.0f)]
    float mCameraOffset = 0.0f;

    float mCameraHeight;


    bool mRunning;

    Animator mAnimator;
    CharacterController mCharacterController;

    Camera mCamera;
    
    public float mGravity = -9.8f;
    Vector3 mVelocity;

    [SerializeField]
    Transform mGroundCheck;

    public float mGoundDistance = 0.1f;

    [SerializeField]
    LayerMask mGroundMask;

    bool mIsGrounded;

    
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponentInChildren<Animator>();
        mCharacterController = GetComponent<CharacterController>();

        mCamera = Camera.main;

        mCameraHeight = mCamera.transform.position.y - transform.position.y + mCameraOffset;
    }

    // Update is called once per frame
    void Update()


    {

        mIsGrounded = Physics.CheckSphere(mGroundCheck.position, mGoundDistance, mGroundMask);

        if(mIsGrounded && mVelocity.y<0)
        {
            mVelocity.y = -2f;
        }


        Vector2 mousePos = Input.mousePosition;
        

        Vector3 mousePosRelative = mCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mCameraHeight)) - transform.position;
        
        Vector3 lookDirection = Vector3.ClampMagnitude(new Vector3(mousePosRelative.x, 0.0f, mousePosRelative.z), 1.0f);



        transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.LookRotation(lookDirection),Time.deltaTime * mTurningSpeed);
        

        mRunning = false;
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");



        if(horizontal != 0.0f || vertical != 0.0f){
            mRunning = true;

        }

        Vector3 direction = Vector3.ClampMagnitude(new Vector3(horizontal,0.0f,vertical), 1.0f);
        //Vector3 direction = Vector3.ClampMagnitude(new Vector3(horizontal,0.0f,vertical)), 1.0f);

        mCharacterController.Move(direction * Time.deltaTime * mSpeed);
        UpdateAnimation();

        mVelocity.y += mGravity * Time.deltaTime;

        mCharacterController.Move(mVelocity * Time.deltaTime);
        


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



    


}
