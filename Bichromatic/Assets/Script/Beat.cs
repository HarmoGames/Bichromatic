using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat : MonoBehaviour
{
    private static Beat beatInstance;
    public float TempoBPM;
    public AudioSource audio;
    private float beatTimer, beatInterval;
    public static bool beatFull;
    public static int beatCountFull;
    
    private void Awake()
    {
        if(beatInstance != null && beatInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            beatInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        audio.Play();
    }

    void Update()
    {
        BeatDetection();
    }

    void BeatDetection()
    {
        beatFull = false;
        beatInterval = 60/TempoBPM;
        beatTimer += Time.deltaTime;
        if(beatTimer >= beatInterval)
        {
            beatTimer -= beatInterval;
            beatFull = true;
            beatCountFull++;
            CameraBoom(9.5f);
        }
    }

    public void CameraBoom(float newSize)
    {
        Camera.main.orthographicSize = newSize;
    }
}
