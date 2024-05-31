using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garden : MonoBehaviour
{
    public int storage = 0;
    int time = 0;
    public int MAX_TIME = 2;
    Transform[] transforms;
    public GameObject nextBelt;

    [SerializeField]
    private int produceItem = 5;

    [SerializeField]
    Conveyer conveyer;
    [SerializeField]
    Container container;
    [SerializeField]
    RaycastHit2D hitm;
    void OnEnable()
    {
        transforms = gameObject.GetComponentsInChildren<Transform>();
        TimeTickSystem.TICK += Tick;
        findNext();
    }

    private void OnDisable()
    {
        TimeTickSystem.TICK -= Tick;
    }

    private void Tick()
    {
        if (time >= MAX_TIME)
        {
            storage++;
            time = 0;
            Debug.Log(storage);
        }
        else
        {
            time++;
        }

        if (storage != 0 && nextBelt != null && storage > 0 && nextBelt.activeInHierarchy)
        {
            if (conveyer != null )
            {
                if (conveyer.item == 0)
                {
                    conveyer.reservecon = this.name;
                    StartCoroutine(move());
                }
            }
            else if (container != null )
            {
                StartCoroutine(store());
            }
        }
        else
        {
            findNext();
        }

    }
    private IEnumerator move()
    {
        yield return new WaitForEndOfFrame();

        if (conveyer.reservecon == this.name)
        {
            conveyer.item = produceItem;
            storage--;
        }
    }
    private void findNext()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transforms[1].position, transforms[1].TransformDirection(Vector2.up), 1);
        Debug.Log(hit);
        if (hitm != hit)
        {
            Debug.Log("test 2");
            Debug.DrawRay(transforms[1].position, transforms[1].TransformDirection(new Vector2(0, 1)), Color.red, 5);
            if (hit.collider)
            {
                if (hit.transform.gameObject.name == "input")
                {
                    GameObject parentObject = hit.transform.parent.gameObject;
                    conveyer = null;
                    container = null;
                    conveyer = parentObject.GetComponent<Conveyer>();
                    container = parentObject.GetComponent<Container>();

                    if (conveyer != null && conveyer.enabled)
                    {
                        nextBelt = parentObject;
                    }
                    else if (container != null && container.enabled)
                    {
                        nextBelt = parentObject;
                    }

                }
                else
                {
                    nextBelt = null;
                }
            }
            else
            {
                nextBelt = null;
            }
            hitm = hit;
        }
    }
    private IEnumerator store()
    {
        yield return new WaitForEndOfFrame();
        if (container.item == produceItem || container.item == 0)
        {
            container.input(produceItem);
            storage--;
        }
    }
}
