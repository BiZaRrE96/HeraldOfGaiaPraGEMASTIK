using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Class to check where, when, and what the mouse is pointing at
/// </summary>
public class MouseReporter : MonoBehaviour, IPointerMoveHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 _MousePosition;
    private float generalHoverTime = 1.0f;
    private int evoCounter = 0;
    private int evoCounter2 = 0;
    [SerializeField] private GameObject descriptionHoverBox;
    [SerializeField] private HoverableObject hoveredObject;
    [SerializeField] private bool hovering;
    [SerializeField] private Coroutine lastHoverCoroutine;
    public Vector3 MousePosition
    {
        get { return _MousePosition; }
    }
    void IPointerMoveHandler.OnPointerMove(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (hovering)
        {
            Vector3 target = eventData.position;
            _MousePosition = target;
            HoverRoutine(eventData);
        }
    }


    /// <summary>
    /// Hides DescriptionHoverBox
    /// </summary>
    private void OnHoverEnd() {
        descriptionHoverBox.SetActive(false);
        hovering = false;
    }


    /// <summary>
    /// Thing to do when hoveing
    /// </summary>
    private void HoverRoutine(UnityEngine.EventSystems.PointerEventData eventData)
    {
        var objects = eventData.hovered;
        HoverableObject tempHO = null;
        foreach (GameObject go in objects)
        {
            if (go.TryGetComponent<HoverableObject>(out tempHO))
            {
                if (hoveredObject.IsUnityNull())
                {
                    ////
                }
                else if (tempHO != hoveredObject)
                {
                    Debug.Log($"{hoveredObject.name} -> {tempHO}");
                    StopCoroutine(lastHoverCoroutine);
                    lastHoverCoroutine = null;
                }
                else // tempHo == hoveredObject
                {
                    return;
                }
                hoveredObject = tempHO;
                lastHoverCoroutine = StartCoroutine(StartHovering());
                return;
            }
        }

        if (tempHO.IsUnityNull())
        {
            if (lastHoverCoroutine != null)
            {
                StopCoroutine(lastHoverCoroutine);
            }
            hoveredObject = null;
            descriptionHoverBox.SetActive(false);
        }

    }

    /// <summary>
    /// Displays said DescriptionHoverBox and updates its text
    /// </summary>
    private void OnHoverCompletion(){
        HoverDisplay hd = descriptionHoverBox.GetComponent<HoverDisplay>();
        hd.gameObject.SetActive(true);
        hd.Display(hoveredObject);
        //descriptionHoverBox.SetActive(true);
    }
    /// <summary>
    /// Hover timer countdown
    /// </summary>
    private IEnumerator StartHovering()
    {
        float totalHoverTime = 0.0f;
        while (totalHoverTime<generalHoverTime)
        {
            totalHoverTime += Time.deltaTime;
            yield return null;
        }
        OnHoverCompletion();
    }

    void IPointerEnterHandler.OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        hovering = true;
    }

    void IPointerExitHandler.OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
    {
        StopCoroutine(StartHovering());
        OnHoverEnd();
    }

}
