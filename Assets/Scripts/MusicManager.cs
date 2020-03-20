/*
Copyright 2020 Micah Schuster

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
this list of conditions and the following disclaimer in the documentation and/or
other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE
OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MusicManager Class.
/// Singleton that plays the background music.
/// Does not destroy on scene switch.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    /// <summary>
    /// Background music clip.
    /// </summary>
    AudioClip backgroundClip;


    //Singleton style class
    static MusicManager _instance=null;

    void Awake(){
        //keeps this gameobject between scene transitions
        //so that the background music will keep playing

        if(_instance==null){
            _instance=this;
        } else {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// Gets the audio source and clip and plays it if it's on not currently
    /// playing.
    /// </summary>
    void Start()
    {
        AudioSource source=GetComponent<AudioSource>();
        backgroundClip=source.clip;

        //play background music here
        if(!source.isPlaying){
            source.Play();
        }
    }
}
