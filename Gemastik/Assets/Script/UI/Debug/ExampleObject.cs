using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExampleObject : MonoBehaviour, IMenuableObject
{
    [SerializeField] private TestItemType item;
    [SerializeField] private float clocke;
    [SerializeField] public List<TestItemType> legalitems;
    [SerializeField] private GameObject MenuPrefab;
    void Start()
    {
        clocke = 0;
        //StartCoroutine(clockclock());
        for (int i = 0; i < 10; i++)
        {
            legalitems.Add(new TestItemType(i));
        }
    }

    void FixedUpdate()
    {
        clocke += Time.deltaTime;
    }
    IEnumerator clockclock()
    {
        int test = 0;
        while (true)
        {
            clocke += Time.deltaTime;
            if ((int) clocke / 10 >= test)
            {
                test++;
                Debug.Log($"Actual Clocke : {test*5}");
            }
            yield return null;
        }
    }
    List<MenuItem> IMenuableObject.OnUpdate()
    {
        //return value
        List<MenuItem> retval = new List<MenuItem>();

        //have to cast to list of object cus lazy
        var ss = new SingleSelectable<TestItemType>()
        {
            name = "Things",
            title = "SelectableItem",
            selectableID = "ITEM",
            selectableItems = legalitems,
            current_value = item
        };

        var mm = new MenuItem()
        {
            name = "clocke",
            title = string.Format("{0:0.00}",clocke),
        };

        var percenter = new ProgressBar()
        {
            name = "Clocke progress",
            value = (clocke % 100)/100
        };

        retval.Add(ss);
        retval.Add(mm);
        retval.Add(percenter);
        return retval;
    }

    bool IMenuableObject.InvokeChange(string MenuItemID, object Thing)
    {
        switch (MenuItemID)
        {
            case "ITEM":
                item = (TestItemType)Thing;
                return true;
            default:
                return false;
        }
    }

    string IMenuableObject.GetMenuID() {
        return "TESTWINDOWLMAO";
    }

    GameObject IMenuableObject.GetPanelPrefab()
    {
        return MenuPrefab;
    }

}
