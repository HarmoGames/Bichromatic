using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntelligentAudio : MonoBehaviour
{
    public float defaultVolume, changeSmoothing;
    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        audio.volume = Mathf.Lerp(audio.volume, defaultVolume, changeSmoothing);
    }
}
