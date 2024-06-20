using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class LocalSelector : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] UnityEvent<GameObject> m_MyEvent = new UnityEvent<GameObject>();
    void IPointerClickHandler.OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        GameObject go = eventData.pointerPressRaycast.gameObject;
        while (go.transform.parent != this.gameObject.transform) {
            go = go.transform.parent.gameObject;
            m_MyEvent.Invoke(go);
        }
        Debug.Log(go.name);
    }
}