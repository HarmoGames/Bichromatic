using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class InGame : MonoBehaviour
{
    public GameObject player;
    public Collider2D[] colliderBlack;
    public Collider2D[] colliderWhite;
    public Tilemap[] BlackTile, WhiteTile;
    public SpriteRenderer[] Checkpoints;
    public Color CBlack, CWhite;
    public float[] colorBlack, colorWhite;
    private Color BColor,WColor;
    public bool canInput = true, isBlack = true, canReallyInput = true;
    public GameObject[] Killer;
    public SpriteRenderer bg;
    public float ColorTransition, colorTime,cameraTime;
    public Transform Camera;
    public Animator Cameranim;
    [SerializeField] public CameraOpening cameraOpen;
    private Vector3 CTarget, UITarget;
    private float CTime;
    public bool isFlipped = false;
    public int OpenC;
    public RectTransform texts;
    public GameObject[] Levels;
    public GameObject[] levelText;
    public Vector3[] spawnPoint;
    public float levelIndex = 0;
    public string nextScene;
    public bool Lastlevel, isPaused;
    public GameObject Win, Pause;
    [SerializeField ]public Cube playa;
    private BGSoundScript overaudio;
    public AudioSource SFX;
    public AudioClip soundChange;
    public RoomMoveManager[] roomMoves;


    // Start is called before the first frame update
    void Start()
    {
        soundChange = Pause.GetComponent<PauseManager>().soundChange;
        SFX = Pause.transform.Find("SFX").gameObject.GetComponent<AudioSource>();
        overaudio = FindObjectOfType<BGSoundScript>();
        Pause.SetActive(true);
        cameraOpen = FindObjectOfType<CameraOpening>();
        Pause.transform.Find("Panel").gameObject.SetActive(false);
        //OpenC = Random.Range(-1,2);
        //Camera.GetComponent<Animator>().SetInteger("Open", OpenC);
        colorBlack = new float[] {CBlack.r,CBlack.g,CBlack.b};
        playa.sprite.color = new Color(colorBlack[0],colorBlack[1],colorBlack[2]);

        colorWhite = new float[] {CWhite.r,CWhite.g,CWhite.b};
        foreach(Collider2D col in colliderBlack)
            {
                col.enabled = true;
            }
        foreach(Collider2D col in colliderWhite)
        {
            col.enabled = false;
        }

        BColor = new Color(colorBlack[0],colorBlack[1],colorBlack[2]);
        WColor = new Color(colorWhite[0],colorWhite[1],colorWhite[2]);

        foreach(Tilemap tile in BlackTile)
        {
            tile.color = BColor;
        }
        foreach(Tilemap tile in WhiteTile)
        {
            tile.color = WColor;
        }

        foreach(SpriteRenderer checkpoint in Checkpoints)
        {
            checkpoint.color = BColor;
        }

        Initiate();
    }

    public void Initiate()
    {
        ColorTransition = 0;
        playa.sprite.flipY = false;
        isBlack = true;
        playa.sprite.color = new Color(colorBlack[0],colorBlack[1],colorBlack[2]);
        
        Camera.rotation = Quaternion.Euler(new Vector3(0,0,0));
        if(texts)
            texts.rotation = Quaternion.Euler(new Vector3(0,0,0));
        player.transform.position = spawnPoint[(int)levelIndex];

        foreach(RoomMoveManager roommove in roomMoves)
        {
            roommove.selectedTransition = roommove.firstTransition;
        }
        // oreach(GameObject level in Levels)
        // {
        //     level.SetActive(false);
        // }
        // foreach(GameObject level in levelText)
        // {
        //     level.SetActive(false);
        // }f
        // Levels[(int)levelIndex].SetActive(true);
        // levelText[(int)levelIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {   
        if(isBlack == true)
        {
            bg.color = Color.Lerp(bg.color, BColor, ColorTransition);
            foreach(GameObject killer in Killer){
                killer.GetComponent<Tilemap>().color = Color.Lerp(killer.GetComponent<Tilemap>().color, BColor, ColorTransition);
            }
            ColorTransition = Mathf.Lerp(0f, 1f, colorTime*Time.deltaTime);
        }
        else
        {
            bg.color = Color.Lerp(bg.color, WColor, ColorTransition);
            foreach(GameObject killer in Killer){
                killer.GetComponent<Tilemap>().color = Color.Lerp(killer.GetComponent<Tilemap>().color, WColor, ColorTransition);
            }
            ColorTransition = Mathf.Lerp(0f, 1f, colorTime*Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.X) && playa.canRotate)
        {
            isFlipped = !isFlipped;
            Rotate();
        }

        Camera.rotation = Quaternion.Slerp(Camera.rotation, Quaternion.Euler(CTarget), CTime);
        if(texts)
            texts.rotation = Quaternion.Slerp(texts.rotation, Quaternion.Euler(UITarget), CTime);
        CTime = Mathf.Lerp(0f, 1f, cameraTime*Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pausing();
        }

        if(isPaused)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                SFX.PlayOneShot(soundChange);
            }
        }
    }

    public void Rotate()
    {
        CTarget = new Vector3(0,0,CTarget.z + 180);
        UITarget = new Vector3(0,0,UITarget.z - 180);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void Pausing()
    {
        if(!isPaused)
        {
            Pause.GetComponent<PauseManager>().SetButtons();
            overaudio.gameObject.GetComponent<AudioSource>().volume = 0.3f;
        }
        else
        {
            overaudio.gameObject.GetComponent<AudioSource>().volume = 1f;
        }

        isPaused = !isPaused;
        canInput = !canInput;
        Pause.transform.Find("Panel").gameObject.SetActive(isPaused);

        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        else if(Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
        }
    }

    public void Quit()
    {
        canInput = false;
        canReallyInput = false;
        Time.timeScale = 1f;
        // overaudio.gameObject.GetComponent<AudioSource>().volume = 1f;
    }
}