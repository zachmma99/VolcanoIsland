using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{

    AudioClip backgroundClip;

    void Awake(){
        //keeps this gameobject between scene transitions
        //so that the background music will keep playing
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioSource source=GetComponent<AudioSource>();
        backgroundClip=source.clip;

        //play background music here
        source.Play();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
