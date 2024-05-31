using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyer : MonoBehaviour
{
    private Transform[] transforms;
    private SpriteRenderer sprite;
    public GameObject nextBelt;
    public string reservecon;
    public int item = 0;
    public static int index;


    Conveyer conveyer;
    Container container;
    RaycastHit2D hitm;
    // Start is called before the first frame update
    void OnEnable()
    {
        transforms = gameObject.GetComponentsInChildren<Transform>();
        sprite = transforms[1].GetComponent<SpriteRenderer>();
        TimeTickSystem.TICK += Tick;
        findNext();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator move()
    {
        yield return new WaitForEndOfFrame();

        if (conveyer.reservecon == this.name)
        {
            conveyer.item = this.item;
            item = 0;

        }
    }
    private IEnumerator store()
    {
        yield return new WaitForEndOfFrame();
        if (container.item == item || container.item == 0)
        {
            container.input(item);
            item = 0;
        }
    }
    private void Tick()
    {
        if (item != 0 && nextBelt != null && nextBelt.activeInHierarchy) 
        {
            if (conveyer != null)
            {
                if (conveyer.item == 0)
                {
                    conveyer.reservecon = this.name;
                    StartCoroutine(move());
                }
            }
            else if (container != null)
            {
                StartCoroutine(store());
            }
        }
        else
        {
            findNext();
        }
    }

    private void findNext()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transforms[1].TransformDirection(Vector2.up), 1);
        if (hitm != hit)
        {
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
    private void FixedUpdate()
    {
        if (item == 0)
        {
            sprite.color = Color.red;
        }
        else
        {
            sprite.color = Color.green;
        }
    }
    public void Stopcorutine()
    {
        TimeTickSystem.TICK -= Tick;
        StopAllCoroutines();
    }
}
