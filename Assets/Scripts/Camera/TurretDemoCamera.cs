using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDemoCamera : MonoBehaviour
{
    public Transform dummy;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = dummy.position;
    }

    // Update is called once per frame
    void Update()
    {            
        if (dummy!=null)
        {

            GetComponent<Camera>().transform.LookAt(dummy.position);
            Vector3 direction = dummy.position - startPos;
            startPos = dummy.position;
            transform.Translate(direction, Space.World);
        }   
    }

}
