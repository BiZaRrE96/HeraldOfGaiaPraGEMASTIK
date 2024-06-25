using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WorldToUIConnector : MonoBehaviour, IPointerClickHandler
{

    public static WorldToUIConnector Instance;
    [SerializeField] public UnityEvent<GameObject> OnWorldobjectClick;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        OnWorldobjectClick = new UnityEvent<GameObject>();
    }
    void IPointerClickHandler.OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        string report = "NULL";
        if (eventData.button.Equals(PointerEventData.InputButton.Right))
        {
            UIDispatcher.Instance.CancelTopMenu();
            report = "RCLICK";
        }
        else if (eventData.button.Equals(PointerEventData.InputButton.Left))
        {
            UIDispatcher.Instance.OpenMenu(eventData.rawPointerPress, eventData);
            report = "LCLICK";
        }

        Debug.Log($"Le {report} clicke {eventData.rawPointerPress.name} at {eventData.pressPosition} ({eventData.position})");

    }
    
}
