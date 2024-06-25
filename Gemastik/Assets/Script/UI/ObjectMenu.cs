using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEditor;
using System;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ObjectMenu : MonoBehaviour
{

}


public class MenuItem 
{
    public string name;
    public string title;
};

public class SingleSelectable : MenuItem
{
    public string selectableID;
    public object current_value;
    public List<object> selectableItems;
}

public interface DrawableItem
{
    UnityEngine.UI.Image displayImage();
    string displayName();
}
public interface IMenuableObject
{
    /// <summary>
    /// Function to get all variables that will be shown on the UI menu
    /// </summary>
    /// <returns>
    /// List of Tuple, consisting of the "Curent value" and any special property
    /// MenuItem() OR (Maybe) Null : Simple text
    /// SelectableItem : Provide an array of legal choices for change
    /// </returns>
    List<MenuItem> OnUpdate();

    //Especially for SingleSelectable, set or tries to set the field "assigned" with said ID the value
    bool InvokeChange(string MenuItemID, object Thing);

    ///<summary>
    /// Get an ID (used to prevent multiple of the same window
    /// </summary>
    string GetMenuID();

    GameObject GetPanelPrefab();
}
