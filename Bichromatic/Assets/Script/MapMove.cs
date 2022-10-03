using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 Move;
    public float moveSpeed;
    public float slideness;

    public bool isSelecting;
    public GameObject selectedLevel;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        Move.x = Mathf.Lerp(Move.x, Input.GetAxisRaw("Horizontal"), slideness);
        Move.y = Mathf.Lerp(Move.y, Input.GetAxisRaw("Vertical"), slideness);

        if(isSelecting)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Inception");
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + Move * moveSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "LevelPad")
        {
            selectedLevel = other.gameObject;
            isSelecting = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "LevelPad")
        {
            selectedLevel = null;
            isSelecting = false;
        }
    }
}
