using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mousehandeler : MonoBehaviour
{
    public static float rotate = 0;
    private Spawner spawner;
    [SerializeField]
    private LayerMask layer;
    private Transform ts;
    private Vector3 vector3;
    [SerializeField]
    private Canvas canvas;

    // Reference to the Event System
    private EventSystem eventSystem;

    // Reference to the Graphic Raycaster
    private GraphicRaycaster graphicRaycaster;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        eventSystem = EventSystem.current;
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Spawner.holditem != null)
        {
            if (ts == null)
            {
                ts = Spawner.holditem.GetComponent<Transform>();
            }
            vector3 = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            vector3.x = Mathf.RoundToInt(vector3.x);
            vector3.y = Mathf.RoundToInt(vector3.y);
            vector3.z = 0;
            ts.position = vector3;
            ts.rotation = Quaternion.Euler(new Vector3(0, 0, rotate));
            if (Input.GetMouseButton(0))
            {
                if (checkPlace(vector3)&&!IsPointerOverUIElement())
                {
                    Spawner.holditem.GetComponent<Building>().enableScript();
                    GameObject temp = Spawner.holditem;
                    Spawner.holditem = null;
                    spawner.Spawnbuilding(temp);
                    ts = Spawner.holditem.GetComponent<Transform>();
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log(Spawner.holditem);
                Debug.Log(Spawner.holditem.name);
                Object_pooling.instance.putinstance(GetBaseName(Spawner.holditem.name), Spawner.holditem);
                ts = null;
                Spawner.holditem = null;
            }
            if (Input.GetKeyDown(KeyCode.R)&&!Input.GetKey(KeyCode.LeftShift))
            {
                Spawner.holditem.transform.Rotate(new Vector3(0, 0, 90));
                rotate = Spawner.holditem.transform.rotation.eulerAngles.z;
            }

        }
        else if (spawner.getstate() == "Delete")
        {
            if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
            {
                Vector3 vector = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                vector.z = 0;
                Collider2D hit = Physics2D.OverlapPoint(vector, layer, -0.5f, 2);
                if (hit != null)
                {
                    Debug.Log(hit.transform.parent.gameObject.name);
                    Object_pooling.instance.putinstance(GetBaseName(hit.transform.parent.gameObject.name), hit.transform.parent.gameObject);

                }
            }
        }
    }
    public bool checkPlace(Vector3 targetvector)
    {
        Collider2D hit;
        if (Spawner.holditem.transform.localScale.x == 1)
        {
            hit = Physics2D.OverlapPoint(targetvector);
        }
        else
        {
            hit = Physics2D.OverlapArea(new Vector2(targetvector.x - 1, targetvector.y - 1), new Vector2(targetvector.x + 1, targetvector.y + 1));
        }

        if (hit == null)
        {
            return true;
        }
        else
        {
            Debug.Log(hit.gameObject.name);
            return false;
        }
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

    private bool IsPointerOverUIElement()
    {
        // Set up a PointerEventData instance
        PointerEventData pointerEventData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };

        // Create a list to hold the raycast results
        List<RaycastResult> results = new List<RaycastResult>();

        // Perform the raycast using the GraphicRaycaster
        graphicRaycaster.Raycast(pointerEventData, results);

        // If the results list contains any objects, the mouse is over a UI element
        return results.Count > 0;
    }
}
