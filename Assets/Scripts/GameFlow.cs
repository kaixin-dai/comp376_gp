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

    public GameObject inventory;
    public GameObject HUD;
    public GameObject essenceHUD;
    public GameObject buildManager;

    private bool paused;

    // Start is called before the first frame update
    void Start()
    {
        ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {

        // Pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
                PauseGame();
            else
                ResumeGame();
        }


        if (Input.GetKeyDown(KeyCode.M))
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

    public void PauseGame()
    {
        paused = true;
        print("Game Paused");

        Time.timeScale = 0;

        inventory.SetActive(true);
        HUD.SetActive(false);
        buildManager.SetActive(false);
/*        essenceHUD.SetActive(true);*/
    }
    public void ResumeGame()
    {
        paused = false;
        print("Game Resumed");

        Time.timeScale = 1;

        inventory.SetActive(false);
        HUD.SetActive(true);
        buildManager.SetActive(true);
        /*        essenceHUD.SetActive(true);*/
    }    
}

