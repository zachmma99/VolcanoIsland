using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{


    void Awake(){
        //keeps this gameobject between scene transitions
        //so that the background music will keep playing
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //play background music here
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
