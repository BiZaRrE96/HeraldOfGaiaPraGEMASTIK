using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class Conveyer : Belt, Building,IPointerClickHandler
{
    private Transform[] transforms;
    public GameObject nextBelt;
    public static int index;
    private Building building;
    private string hitm;
    private Beltsystem beltsystem;
    // Called when the script is enabled
    void OnEnable()
    {
        Debug.Log("Conveyer enabled");
        transforms = gameObject.GetComponentsInChildren<Transform>();
        findNext();
    }

    // Checker method to verify if this conveyer can accept the item
    public bool checker(GameObject gameObject, int produce)
    {
        return reservecon == gameObject.name && item == 0;
    }

    // Method to input an item into the conveyer
    public void inputer(int Produce,GameObject resource)
    {
        item = Produce;
        item_object = resource;
        StartCoroutine(moving(resource, this.gameObject));
    }

    // Coroutine to move the item to the next building
    private IEnumerator move()
    {
        yield return new WaitForEndOfFrame();

        if (building.checker(this.gameObject, item))
        {
            building.inputer(item,item_object);
            item = 0;
            item_object = null;
        }
    }

    private IEnumerator moving(GameObject origin,GameObject target)
    {
        while (origin.transform.position!=target.transform.position)
        {
            origin.transform.position = Vector2.MoveTowards(origin.transform.position, target.transform.position, 3 * Time.deltaTime);
            yield return null;
        }
        Debug.Log("moving");

    }

    // Called on each tick to process items
    public override void Tick()
    {
        if (item_object)
        {
            if (item != 0 && nextBelt != null && nextBelt.activeInHierarchy)
            {
                if(item_object.transform.position==this.transform.position)
                {
                    if (building is Belt)
                    {
                        Belt temp = (Belt)building;
                        temp.reservecon = this.name;
                        building = (Building)temp;
                    }
                    StartCoroutine(move());

                }
            }
            else
            {
                findNext();
            }

        }
    }

    // Method to find the next belt in the conveyer system
    private void findNext()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transforms[1].TransformDirection(Vector2.up), 1, 64);
        if (hit.collider)
        {
            Debug.DrawRay(transforms[1].position, transforms[1].TransformDirection(Vector2.up), Color.red, 5);
            if (hit.transform.gameObject.name == "input")
            {
                if (hitm != hit.transform.parent.gameObject.name)
                {
                    GameObject parentObject = hit.transform.parent.gameObject;
                    building = parentObject.GetComponent<Building>();
                    nextBelt = parentObject;
                    hitm = parentObject.name;
                }
            }
            else
            {
                nextBelt = null;
                hitm = null;
            }
        }
        else
        {
            nextBelt = null;
            hitm = null;
        }
    }

    // Method to stop all coroutines and unsubscribe from the tick event
    public void Stopcorutine()
    {
        TimeTickSystem.TICK -= Tick;
        StopAllCoroutines();
    }

    // Enable the script and its colliders
    public void enableScript()
    {
        this.enabled = true;
        GetComponentInChildren<BoxCollider2D>().enabled = true;
        findNext();
        beltsystem = GameObject.FindGameObjectWithTag("Beltsystem").GetComponent<Beltsystem>();
        beltsystem.addConveyer(this);
    }

    // Disable the script and its colliders
    public void disableScript()
    {
        transforms = gameObject.GetComponentsInChildren<Transform>();
        if (item_object)
        {
            item_object.SetActive(false);
            item_object = null;
        }
        Stopcorutine();
        beltsystem = GameObject.FindGameObjectWithTag("Beltsystem").GetComponent<Beltsystem>();
        beltsystem.removeConveyer(this);
        this.enabled = false;
        item = 0;
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        gameObject.SetActive(false);
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        Debug.Log(name + " Game Object Clicked!");
    }
}
