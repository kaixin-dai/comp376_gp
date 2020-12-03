using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{

    [SerializeField]
    Transform mSpwanPosition;
    [SerializeField]
    int mSpwanNumber = 5;
    [SerializeField]
    GameObject mBugReference;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            SceneManager.LoadScene("Inventory");
        }


        if(Input.GetKeyDown(KeyCode.M))
        {
            for(int i = - 2 ; i < mSpwanNumber - 2 ; i++)
            {
              Instantiate(mBugReference,mSpwanPosition.position + (i * 5 * Vector3.right),Quaternion.identity);
            }
        }

        if(Input.GetKeyDown(KeyCode.N))
        {
            GameObject bigBug = Instantiate(mBugReference,mSpwanPosition.position,Quaternion.identity);
            bigBug.transform.localScale = new Vector3(3.0f,3.0f,3.0f);
            bigBug.GetComponent<Health>().SetMaxHealth(100);
        }

    }
}

