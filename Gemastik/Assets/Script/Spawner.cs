using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static GameObject holditem = null;
    private string state;
    public GameObject test;
    private void Update()
    {
        test = holditem;
    }

    public void Spawnbuilding(GameObject building)
    {
        Debug.Log(holditem);
        if (holditem == null)
        {
            Vector3 vector3 = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            vector3.x = Mathf.RoundToInt(vector3.x);
            vector3.y = Mathf.RoundToInt(vector3.y);
            vector3.z = 0;
            Object_pooling.instance.getinstance(GetBaseName(building.name), vector3);
            holditem.name = GetBaseName(building.name) + "_" + Conveyer.index;
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

    public string getstate()
    {
        return state;
    }

    public string GetBaseName(string objectName)
    {
        int index = objectName.LastIndexOf('_');
        if (index > 0)
        {
            return objectName.Substring(0, index);
        }
        return objectName;
    }
}
