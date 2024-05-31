using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mousehandeler : MonoBehaviour
{
    public static float rotate = 0;
    private Spawner spawner;
    [SerializeField]
    private LayerMask layer;
    private Transform ts;
    private Vector3 vector3;
    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
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
            if (Input.GetAxis("Mouse X") < 0 || Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse Y") < 0 || Input.GetAxis("Mouse Y") > 0)
            {
                vector3 = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                vector3.x = Mathf.RoundToInt(vector3.x);
                vector3.y = Mathf.RoundToInt(vector3.y);
                vector3.z = 0;
                ts.position = vector3;
                ts.rotation = Quaternion.Euler(new Vector3(0, 0, rotate));
            }
            if (Input.GetMouseButton(0))
            {
                if (checkPlace(vector3))
                {
                    Spawner.holditem.GetComponent<Conveyer>().enabled = true;
                    Spawner.holditem.GetComponentInChildren<BoxCollider2D>().enabled = true;
                    Spawner.holditem = null;
                    spawner.Spawnconveyer();
                    ts = Spawner.holditem.GetComponent<Transform>();
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(Spawner.holditem);
                ts = null;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Spawner.holditem.transform.Rotate(new Vector3(0, 0, 90));
                rotate = Spawner.holditem.transform.rotation.eulerAngles.z;
            }

        }
        else if (Spawner.state == "Delete")
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 vector = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                vector.z = 0;
                Collider2D hit = Physics2D.OverlapPoint(vector, layer, -0.5f, 2);
                if (hit != null)
                {
                    Debug.Log(hit.transform.parent.gameObject.name);
                    hit.transform.parent.gameObject.GetComponent<Conveyer>().Stopcorutine();
                    hit.transform.parent.gameObject.GetComponent<Conveyer>().enabled = false;
                    hit.transform.parent.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
                    hit.transform.parent.gameObject.SetActive(false);

                }
            }
        }
    }
    public bool checkPlace(Vector3 targetvector)
    {
        Collider2D hit = Physics2D.OverlapPoint(targetvector, layer, -0.5f, 2);
        if (hit == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
