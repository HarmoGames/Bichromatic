using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGroove : MonoBehaviour
{
    public Camera camera;
    public float BPM, zoomIntensity;
    public int beatCount;

    void Awake()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        float bpm = (240/BPM)/beatCount;
        Zoom();
        yield return new WaitForSeconds(bpm/Time.deltaTime);
        StartCoroutine(Wait());
    }

    void Zoom()
    {
        camera.orthographicSize = zoomIntensity;

    }
}
