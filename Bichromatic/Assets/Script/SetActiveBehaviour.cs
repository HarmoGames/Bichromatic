using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveBehaviour : MonoBehaviour
{
    public void SetTrue()
    {
        this.gameObject.SetActive(true);
    }

    public void SetFalse()
    {
        this.gameObject.SetActive(false);
    }

    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }
}
