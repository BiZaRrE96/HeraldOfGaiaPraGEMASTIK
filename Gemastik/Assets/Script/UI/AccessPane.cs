using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AccessPane : MonoBehaviour
{
    public Coroutine UpdateCoroutine;
    public string ID;
    void Start()
    {

    }

    public void Cancel()
    {
        Debug.Log("Nvm bro");
        if (UpdateCoroutine != null)
        {
            StopCoroutine(UpdateCoroutine);
        }
        Destroy(this.gameObject);
    }
}
