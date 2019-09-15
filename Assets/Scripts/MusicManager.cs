using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{

    AudioClip backgroundClip;
    static MusicManager _instance=null;

    void Awake(){
        //keeps this gameobject between scene transitions
        //so that the background music will keep playing
        
        //ensures that there is only ever one of these
        //objects in the scene.
        if(_instance==null){
            _instance=this;
        } else {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioSource source=GetComponent<AudioSource>();
        backgroundClip=source.clip;

        //play background music here
        if(!source.isPlaying){
            source.Play();
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
