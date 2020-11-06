using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField]
    Transform mPlayer;

    Vector3 mCameraOffset;

    [SerializeField]
    int mSmoothness = 1;
    // Start is called before the first frame update
    void Start()
    {
        mCameraOffset = transform.position - mPlayer.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = mPlayer.position + mCameraOffset;

        transform.position = Vector3.Lerp(transform.position,newPos, mSmoothness * Time.deltaTime);
    }

}
