using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Sound")]
    [SerializeField] private AudioClip footstepClip; 
    [SerializeField] private AudioClip chargeClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Function to play footstep sound
    public void PlayFootstep()
    {  
        // play a footstep sound        
        audioSource.PlayOneShot(footstepClip);        
    }

    public void PlayCharge()
    {
        // play a footstep sound        
        audioSource.PlayOneShot(chargeClip);
    }
}
