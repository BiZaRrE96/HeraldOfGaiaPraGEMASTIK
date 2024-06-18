using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    public GameObject master;
    public Vector3 offset;
    private MouseReporter mouseReporter;

    private IEnumerator MoveThis()
    {
        while (true)
        {
            this.gameObject.transform.position = mouseReporter.MousePosition + offset;
            yield return null;
        }
    }

    void OnEnable()
    {
        mouseReporter = master.GetComponent<MouseReporter>();
        if (mouseReporter != null)
        {
            StartCoroutine(MoveThis());
        }
    }

    void OnDisable()
    {
        StopCoroutine(MoveThis());
    }

}
