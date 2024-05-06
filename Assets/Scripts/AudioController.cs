using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource source;
    public AudioClip backgroundMusic;
    public AudioClip blockDropEffect;

    void Start()
    {
        // plays background music on start
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDropEffect(){
        source.PlayOneShot(blockDropEffect);
    }
}