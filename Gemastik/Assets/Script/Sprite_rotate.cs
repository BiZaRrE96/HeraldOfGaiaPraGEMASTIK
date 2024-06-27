using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite_rotate : MonoBehaviour
{
    private Transform parent;

    private void OnEnable()
    {
        parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
