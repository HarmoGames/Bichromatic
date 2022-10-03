using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool cameraLocked;
    public Vector2 maxCameraLimits;
    public Vector2 minCameraLimits;
    public CameraMove cameraMove;
    public Vector2 cameraPos;

    public GameObject checkpointUI;

    private bool isCheckpointed;
    public Cube player;
    public InGame game;
    public AudioClip checkpointSound;
    public bool upsideDown;

    void Start()
    {
        cameraMove = FindObjectOfType<CameraMove>();
        player = FindObjectOfType<Cube>();
        game = FindObjectOfType<InGame>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(game.spawnPoint[0] != transform.position)
            {
                GetComponent<Animator>().SetBool("checked", true);
                GameObject checkUI = Instantiate(checkpointUI, Vector3.zero, Quaternion.identity);
                checkUI.transform.SetParent(FindObjectOfType<Canvas>().transform);
                checkUI.transform.localPosition = Vector3.zero;
                checkUI.transform.localScale = new Vector3(1,1,1);
                game.SFX.PlayOneShot(checkpointSound);
            }

            game.spawnPoint[0] = transform.position;
            player.cameraSpawn = cameraPos;

            if(cameraLocked)
            {
                cameraMove.maxipos = maxCameraLimits;
                cameraMove.minipos = maxCameraLimits;
            }
        }
    }

    void Update()
    {
        if(game.spawnPoint[0] != transform.position)
        {
            GetComponent<Animator>().SetBool("checked", false);
        }
    }
}