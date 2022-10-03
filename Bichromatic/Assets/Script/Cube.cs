using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Cube : MonoBehaviour
{
    [SerializeField] public InGame gameSettings;
    private Rigidbody2D cube;
    public SpriteRenderer sprite;
    public Animator animator;
    public float JumpForce;
    public bool canJump = false, canSuperJump, canSwap = true, canYouJump = true, canRotate, blocked;
    public float moveX, moveY;
    public float moveSpeed, endSpeed;
    public Vector3 EndLevel, A,B;
    public bool isEnded = false;
    private GameObject camerade;
    private CameraMove cameraMove;
    public Vector2 cameraSpawn;
    public AudioClip[] deathSounds;
    public GameObject colliderDown, colliderUp;
    // Start is called before the first frame update
    void Start()
    {
        cube = gameObject.GetComponent<Rigidbody2D>();
        camerade = FindObjectOfType<CameraMove>().gameObject;
        cameraMove = FindObjectOfType<CameraMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {   if(canJump && gameSettings.canInput && canYouJump)
            {
                if(canSuperJump)
                {
                    cube.velocity = Vector2.up * JumpForce *1.5f;
                }
                else
                {
                    cube.velocity = Vector2.up * JumpForce;
                }
            }

            
        }

        if(Input.GetKeyDown(KeyCode.DownArrow) && gameSettings.canInput)
        {
            if(canJump && !canSuperJump && canSwap && !blocked)
            {
                if(gameSettings.colliderBlack[0].enabled == true)
                {
                    gameSettings.ColorTransition = 0;
                    sprite.flipY = true;
                    gameSettings.isBlack = false;
                    gameSettings.canInput = false;
                    animator.SetInteger("switch", 1);
                    sprite.color = new Color(gameSettings.colorWhite[0],gameSettings.colorWhite[1],gameSettings.colorWhite[2]);
                }
                else
                {
                    gameSettings.ColorTransition = 0;
                    sprite.flipY = false;
                    gameSettings.isBlack = true;
                    gameSettings.canInput = false;
                    animator.SetInteger("switch", -1);
                    sprite.color = new Color(gameSettings.colorBlack[0],gameSettings.colorBlack[1],gameSettings.colorBlack[2]);
                }
                cube.velocity = new Vector2(0, cube.velocity.y);
            }
        }

        if(gameSettings.colliderBlack[0].enabled == true)
        {
            colliderDown.SetActive(true);
            colliderUp.SetActive(false);
        }
        else
        {
            colliderUp.SetActive(true);
            colliderDown.SetActive(false);
        }

        if(isEnded == true)
        {
            gameSettings.canInput = false;
            // A = transform.position;
            cube.gravityScale = 0;
            gameObject.GetComponent<Collider2D>().enabled = false;
            // transform.position = Vector3.Lerp(transform.position, new Vector3(EndLevel.x + 2, EndLevel.y, EndLevel.z), endSpeed*Time.deltaTime);
            // B = transform.position;
            // if(A == B)
            // {
            gameSettings.NextLevel();
            // }
        }
    }

    void FixedUpdate()
    {
        if(gameSettings.canInput)
        {
            if(!gameSettings.isFlipped){
                moveX =  Input.GetAxisRaw("Horizontal");
            }
            else
            {
                moveX =  -Input.GetAxisRaw("Horizontal");
            }

            cube.velocity = new Vector2(moveX* moveSpeed, cube.velocity.y);
            if(moveX > 0)
            {
                sprite.flipX = false;
            }
            else if(moveX < 0)
            {
                sprite.flipX = true;
            }
        }
    }

    public void Switch()
    {
        animator.SetInteger("switch", 0);
        cube.gravityScale *= -1;
        JumpForce = -JumpForce;

        if(gameSettings.colliderBlack[0].enabled == true)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y -1, 0);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y +1, 0);
            }

        foreach(Collider2D col in gameSettings.colliderBlack)
        {
            col.enabled = !col.enabled;
        }
        foreach(Collider2D col in gameSettings.colliderWhite)
        {
            col.enabled = !col.enabled;
        }
        gameSettings.canInput = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            canJump = true;
        }

        if(other.gameObject.tag == "Block")
        {
            canJump = true;
            blocked = true;
        }

        if(other.gameObject.tag == "Jumppad")
        {
            canSuperJump = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "EndDoor")
        {
            if(gameSettings.Lastlevel)
            {
                gameSettings.Win.SetActive(true);
                gameSettings.canInput = false;
                gameSettings.canReallyInput = false;
            }
            else
            {
                isEnded = true;
            }
        }

        if(other.gameObject.tag == "Ground" || other.gameObject.tag == "Block")
        {
            animator.SetTrigger("Land");
        }
    }

    public void Reset()
    {
        gameSettings.Initiate();
            if(gameSettings.colliderBlack[0].enabled == false){
                Switch();
            }
            if(gameSettings.isFlipped)
            {
                gameSettings.Rotate();
            }
            gameSettings.isFlipped = false;
            gameSettings.cameraOpen.camara.orthographicSize = 5;
            camerade.transform.position = cameraSpawn;
            cameraMove.maxPos = cameraMove.maxipos;
            cameraMove.minPos = cameraMove.minipos;
    }

    public void Die()
    {
        gameSettings.Initiate();
        int sound = Random.Range(0,2);
        gameSettings.SFX.PlayOneShot(deathSounds[sound]);
            if(gameSettings.colliderBlack[0].enabled == false){
                Switch();
            }
            if(gameSettings.isFlipped)
            {
                gameSettings.Rotate();
            }
            gameSettings.isFlipped = false;
            gameSettings.cameraOpen.camara.orthographicSize = 5;
            camerade.transform.position = cameraSpawn;
            cameraMove.maxPos = cameraMove.maxipos;
            cameraMove.minPos = cameraMove.minipos;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            canJump = false;
        }

        if(other.gameObject.tag == "Block")
        {
            blocked = false;
            canJump = false;
        }

         if(other.gameObject.tag == "Jumppad")
        {
            canSuperJump = false;
        }
    }
}
