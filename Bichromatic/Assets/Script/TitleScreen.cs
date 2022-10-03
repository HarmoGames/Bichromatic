using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public Animator animator;
    public AudioSource audio, overaudio;
    public AudioClip sound;
    private bool press = true, quited;
    public bool canQuit;
    public float soundTime;
    public GameObject quitObject;
    // Start is called before the first frame update
    void Start()
    {
        press = true;
        overaudio = GameObject.FindGameObjectWithTag("Overaudio").GetComponent<AudioSource>();
        quitObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !quited)
        {
            animator.SetBool("isPressed", true);
            if(press)
            {
                audio.PlayOneShot(sound);
                overaudio.volume = 0.5f;
                press = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape) && press && canQuit)
        {
            quitObject.SetActive(true);
            quited = true;
        }
        overaudio.volume = Mathf.Lerp(overaudio.volume, 1f, soundTime);
    }

    void OnApplicationQuit()
    {
        quitObject.SetActive(true);
        quited = true;
    }
}
