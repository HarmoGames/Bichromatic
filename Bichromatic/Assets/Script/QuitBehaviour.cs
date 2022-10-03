using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuitBehaviour : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string[] messages;
    // Start is called before the first frame update
    void Start()
    {
        text.text = messages[Random.Range(0,messages.Length -1)];
    }

    public void Quit()
    {
        Application.Quit();
    }
}
