using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSensor : MonoBehaviour
{
    private Cube playa;

    void Start()
    {
        playa = transform.parent.GetComponent<Cube>();
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Killer")
        {
            playa.gameSettings.Cameranim.SetInteger("ded", Random.Range(1,3));
            playa.Die();
        }
    }
}
