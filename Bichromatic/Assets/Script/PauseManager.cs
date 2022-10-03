using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private InGame gameManager;
    [SerializeField] private Cube player;
    public GameObject Pause;
    public Image pausImage;
    public GameObject button;
    public int alpha;
    public AudioClip soundChange;

    void Start()
    {
        gameManager = FindObjectOfType<InGame>();
        player = FindObjectOfType<Cube>();
    }

    void Update()
    {
        Vector3 norm = new Vector3(1,1,1);
        Pause.GetComponent<RectTransform>().localScale = Vector3.Lerp(Pause.GetComponent<RectTransform>().localScale, norm, 0.07f);
        // pausImage.color = Color.Lerp(pausImage.color, new Color(pausImage.color.r, pausImage.color.g, pausImage.color.b, alpha), 0.1f);
    }

    public void SetButtons()
    {
        Pause.GetComponent<RectTransform>().localScale = new Vector3(1.25f,1.25f,1);
        // pausImage.color = new Color(pausImage.color.r, pausImage.color.g, pausImage.color.b, 0);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button);
    }

    public void Reset()
    {
        player.Reset();
    }
    
    public void Continue()
    {
        gameManager.Pausing();
    }

    public void Quit()
    {
        gameManager.Quit();
    }
}
