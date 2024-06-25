using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TestItemType : DrawableItem
{
    public int TestValue;
    public TestItemType(int testValue)
    {
        TestValue = testValue; 
    }

    UnityEngine.UI.Image DrawableItem.displayImage() { return null; }
    string DrawableItem.displayName() { return $"TestItem {TestValue}"; }

}
