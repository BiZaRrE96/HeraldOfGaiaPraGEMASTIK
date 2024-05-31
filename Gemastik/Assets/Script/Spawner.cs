using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static GameObject holditem = null;
    public GameObject conveyer;
    public static string state;
    public void Spawnconveyer()
    {
        Debug.Log(holditem);
        if (holditem == null)
        {
            Vector3 vector3 = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            vector3.x = Mathf.RoundToInt(vector3.x);
            vector3.y = Mathf.RoundToInt(vector3.y);
            vector3.z = 0;
            holditem = Instantiate(conveyer, vector3, Quaternion.Euler(new Vector3(0, 0, Mousehandeler.rotate)));
            holditem.name = "Conveyer " + Conveyer.index;
            Conveyer.index++;

        }
    }
    public void Deletestate()
    {
        if (state == "Delete")
        {
            state = null;
            Debug.Log(state);
        }
        else
        {
            state = "Delete";
            Debug.Log(state);
        }
    }
}
