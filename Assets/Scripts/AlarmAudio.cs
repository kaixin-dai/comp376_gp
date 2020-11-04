using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmAudio : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public AudioClip audioClip;
    public AudioSource audioSource;

    void Start()
    {
        audioSource.clip = audioClip;
    }
    // Update is called once per frame
    public void AlarmSound()
    {
        audioSource.Play();
    }
}
