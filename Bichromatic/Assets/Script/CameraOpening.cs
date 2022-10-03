using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOpening : MonoBehaviour
{
    private Animator animator;
    public Camera camara;
    public float size = 10;

    void Start()
    {
        animator = GetComponent<Animator>();
        camara = gameObject.GetComponent<Camera>();
        camara.orthographicSize = 5;
    }

    void Update()
    {
        if(gameObject.GetComponent<Camera>().orthographicSize != size)
        {
            gameObject.GetComponent<Camera>().orthographicSize = (Mathf.Lerp(gameObject.GetComponent<Camera>().orthographicSize, 10, 5*Time.deltaTime));
        }
    }

    public void StopIt()
    {
        animator.enabled = false;
    }
}
