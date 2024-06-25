using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class LocalSelector : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public UnityEvent<GameObject> pickEvent = new UnityEvent<GameObject>();
    [SerializeField] private GameObject m_Parent;
    void IPointerClickHandler.OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        GameObject go = eventData.pointerPressRaycast.gameObject;
        while (go.transform.parent != this.gameObject.transform) {
            go = go.transform.parent.gameObject;
        }
        pickEvent.Invoke(go);
        Debug.Log(go.name);
    }
}