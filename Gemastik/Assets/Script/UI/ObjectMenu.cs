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
    public string titleName { get { return name + "[Title]"; } } //use if you want to put title in child of object with name
};

/// <summary>
/// I love generic (lies)
/// Note : must put object of name "Display" (refer to prefab)
/// </summary>
public abstract class SingleSelectable : MenuItem
{
    public string selectableID;
    public string buttonName { get { return this.name + "[Button]"; } }
    public abstract object get_current_value { get; }
    public abstract List<object> get_selectable_items { get; }
}

public class SingleSelectable<T> : SingleSelectable
{
    public T current_value;
    public List<T> selectableItems;

    public override object get_current_value
    {
        get { return current_value; }
    }

    public override List<object> get_selectable_items
    {
        get { return selectableItems.ConvertAll(item => (object)item); }
    }
}

public class ProgressBar : MenuItem
{
    public string progressBarName { get { return this.name + "[Progress Bar]";} }
    public float value;
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
