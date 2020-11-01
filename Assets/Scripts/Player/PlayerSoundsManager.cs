using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundsManager : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource source;

    [SerializeField]
    AudioClip shoot;

    [SerializeField]
    AudioClip melee;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    // Update is called once per frame

    public void PlayShootSound()
    {
        source.PlayOneShot(shoot,1);
    }

    public void PlayMeleeSound()
    {
        source.PlayOneShot(melee,1);
    }
}
