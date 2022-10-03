using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMoveManager : MonoBehaviour
{
    public bool single;
    public RoomMove firstTransition;
    public RoomMove secondTransition;
    public RoomMove selectedTransition;
    public bool lateral;

    void Start()
    {
        selectedTransition = firstTransition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && !single)
        {
            Transition(selectedTransition);
            if(!lateral)
            {
                other.transform.position += selectedTransition.playerChange;
            }
        }
        else if(other.gameObject.tag == "Player")
        {
            Transition(firstTransition);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(selectedTransition == firstTransition)
        {
            selectedTransition = secondTransition;
        }
        else if(selectedTransition == secondTransition)
        {
            selectedTransition = firstTransition;
        }
    }

    void Transition(RoomMove check)
    {
        check.cam.minPos = check.newMinpos;
		check.cam.maxPos = check.newMaxpos;
		
    }
}
