using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager _instance = null;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public static MusicManager instance()
    {
        return _instance;
    }

    AudioClip background;

    void Start()
    {
        AudioSource source = GetComponent<AudioSource>();
        background = source.clip;

        if (!source.isPlaying)
        {
            source.Play();
        }
    }
}
