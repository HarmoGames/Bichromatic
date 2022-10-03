using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    private InGame game;
    public GameObject pauseButton;

    void Start()
    {
        game = FindObjectOfType<InGame>();
    }

    public void Pausing()
    {
        game.Pausing();
    }

    void Update()
    {
        if(game.isPaused)
        {
            pauseButton.SetActive(false);
        }
        else
        {
            pauseButton.SetActive(true);
        }
    }
}
