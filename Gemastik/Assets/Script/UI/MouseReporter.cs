using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private GameObject DescriptionHoverBox;
    public Vector3 MousePosition
    {
        get { return _MousePosition; }
    }
    void IPointerMoveHandler.OnPointerMove(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Vector3 target = eventData.position;
        _MousePosition = target;
    }


    /// <summary>
    /// Hides DescriptionHoverBox
    /// </summary>
    private void OnHoverEnd() {
        DescriptionHoverBox.SetActive(false);
    }


    /// <summary>
    /// Displays said DescriptionHoverBox and updates its text
    /// </summary>
    private void OnHoverCompletion(){
        Debug.Log("Hover Complete");
        DescriptionHoverBox.SetActive(true);
    }
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
        HoverableObject ho = eventData.pointerEnter.GetComponent<HoverableObject>();
        if (ho != null) {
            StartCoroutine(StartHovering());
        } 
    }

    void IPointerExitHandler.OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
    {
        StopCoroutine(StartHovering());
        Debug.Log("Exitted thing");
        OnHoverEnd();
    }

}
