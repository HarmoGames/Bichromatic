using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Type : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string levelName, sceneName;
    public float typeSpeed, pauseSpeed, waitTime;
    public AudioSource Audio;
    public AudioClip typeSound;

    void Start()
    {
        text.text = "";
        StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        foreach(char character in levelName)
        {
            text.text += character;
            if(character != ' ')
            {
                Audio.PlayOneShot(typeSound);
            }

            if(character == ':')
            {
                yield return new WaitForSeconds(pauseSpeed);
            }
            else
            {
                yield return new WaitForSeconds(typeSpeed);

            }
        }
    }

    void Update()
    {
        if(text.text == levelName)
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneName);
    }
}
