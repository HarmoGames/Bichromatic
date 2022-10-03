using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelect : MonoBehaviour
{
    public level[] levels;
    public LevelSave levelSave;
    public int levelIndex;
    public Image[] imagesW;
    public Image[] imagesB;
    public TextMeshProUGUI[] texts;
    public TextMeshProUGUI textName;
    public float[] colorB;
    public float[] colorW;
    public float colorChange;
    public bool canChange, isCredited, canInput = true;
    public GameObject Translevel, Transcancel, texta7;
    public AudioSource audio, overaudio;
    public AudioClip sound, soundChange, soundExit;
    public GameObject creditObject;



    // Start is called before the first frame update
    void Start()
    {
        levelIndex = levelSave.levelID;
        overaudio = FindObjectOfType<BGSoundScript>().GetComponent<AudioSource>();
        overaudio.volume = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) && canChange)
        {
            Left();
        }

        if(Input.GetKeyDown(KeyCode.RightArrow) && canChange)
        {
            Right();
        }

        if(Input.GetKeyDown(KeyCode.Space) && canChange)
        {
            Play();
        }

        if(Input.GetKeyDown(KeyCode.Escape) && canChange)
        {
            Cancel();
        }

        if(levelIndex >= levels.Length)
        {
            levelIndex = 0;
        }

        if(levelIndex < 0)
        {
            levelIndex = levels.Length -1;
        }

        colorB[0] = levels[levelIndex].Black.r;
        colorB[1] = levels[levelIndex].Black.g;
        colorB[2] = levels[levelIndex].Black.b;

        colorW[0] = levels[levelIndex].White.r;
        colorW[1] = levels[levelIndex].White.g;
        colorW[2] = levels[levelIndex].White.b;

        foreach(Image image in imagesB)
        {
            image.color = Color.Lerp(image.color, new Color(colorB[0], colorB[1], colorB[2]), colorChange);
        }

        foreach(Image image in imagesW)
        {
            image.color = Color.Lerp(image.color, new Color(colorW[0], colorW[1], colorW[2]),colorChange);
        }

        foreach(TextMeshProUGUI text in texts)
        {
            text.color = Color.Lerp(text.color, new Color(colorW[0], colorW[1], colorW[2]), colorChange);
        }

        textName.text = levels[levelIndex].name;
        textName.color = Color.Lerp(textName.color, new Color(colorB[0], colorB[1], colorB[2]), colorChange);

        if(isCredited)
        {
            canInput = false;
            creditObject.SetActive(true);
            overaudio.volume = 0.3f;
        }
        else
        {
            creditObject.SetActive(false);
            overaudio.volume = 1;
        }
    }

    public void Cancel()
    {
        if(canInput)
        {
            levelSave.levelID = levelIndex;
            canInput = false;
            canChange = false;
            Transcancel.SetActive(true);
            Transcancel.GetComponent<SceneBehaviour>().Scene = "TitleScreen";
            audio.PlayOneShot(soundExit);
        }

        if(!canInput && isCredited)
        {
            isCredited = false;
            canInput = true;
        }
    }

    public void Left()
    {
        if(canChange && canInput)
        {
            levelIndex--;
            audio.PlayOneShot(soundChange);
        }
    }

    public void Right()
    {
        if(canChange && canInput)
        {
            levelIndex++;
            audio.PlayOneShot(soundChange);
        }
    }

    public void Play()
    {
        if(canInput)
        {
            canChange = false;
            texta7.SetActive(false);
            levelSave.levelID = levelIndex;
            Translevel.SetActive(true);
            Translevel.GetComponent<SceneBehaviour>().Scene = levels[levelIndex].Scene;
            audio.PlayOneShot(sound);
        }
    }

    public void Credits()
    {
        if(canInput)
        {
            isCredited = true;
        }
    }
}
