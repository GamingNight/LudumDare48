using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPitch : MonoBehaviour
{

    private AudioSource audioSource;

    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = (Random.Range(0.9f, 1.1f));
        audioSource.Play();

    }


}
