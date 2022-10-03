using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayableEvents : MonoBehaviour
{
    public float TempoBPM;
    private PlayableDirector timelineManager;
    void Start()
    {
        timelineManager = GetComponent<PlayableDirector>();
        timelineManager.playableGraph.GetRootPlayable(0).SetSpeed(TempoBPM/60);
    }

    public void CameraBoom(float newSize)
    {
        Camera.main.orthographicSize = newSize;
    }

    public void CameraChangeStaticSize(float newSizeSet)
    {
        Camera.main.GetComponent<CameraOpening>().size = newSizeSet;
    }

    public void AudioPlay(AudioSource audio)
    {
        if(!audio.isPlaying)
        {
            audio.Play();
        }
    }
}
