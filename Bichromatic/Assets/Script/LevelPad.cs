using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPad : MonoBehaviour
{
    public SpriteRenderer lvlDisplay;
    private bool amSelected;
    public float easing;
    private float colorTime;

    void Start()
    {
        colorTime = 0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            amSelected = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            amSelected = false;
        }
    }

    void Update()
    {
        if(amSelected)
        {
            lvlDisplay.color = Color.Lerp(lvlDisplay.color, Color.white, colorTime);
            colorTime = Mathf.Lerp(0f, 1f, easing*Time.deltaTime);
        }
        else
        {
            lvlDisplay.color = Color.Lerp(lvlDisplay.color, new Color(255, 255, 255, 0), colorTime);
            colorTime = Mathf.Lerp(0f, 1f, easing*Time.deltaTime);
        }
    }
}
