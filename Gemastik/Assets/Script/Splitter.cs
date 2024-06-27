using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : Belt, Building
{
    public Transform[] transforms;
    private GameObject[] nextBelt = new GameObject[3];
    public Building[] building = new Building[3];
    private string[] hitm = new string[3];
    public int currentside = 0;
    private Beltsystem beltsystem;
    // Called when the script is enabled
    void OnEnable()
    {
        transforms = gameObject.GetComponentsInChildren<Transform>();
        TimeTickSystem.TICK += Tick;
        findNext();
    }

    // Checker method to verify if this conveyer can accept the item
    public bool checker(GameObject gameObject, int produce)
    {
        return reservecon == gameObject.name && item == 0;
    }

    // Method to input an item into the conveyer
    public void inputer(int Produce, GameObject resource)
    {
        item = Produce;
        item_object = resource;
        StartCoroutine(moving(resource, this.gameObject));
    }

    private IEnumerator moving(GameObject origin, GameObject target)
    {
        while (origin.transform.position != target.transform.position)
        {
            origin.transform.position = Vector2.MoveTowards(origin.transform.position, target.transform.position, 3 * Time.deltaTime);
            yield return null;
        }
        Debug.Log("moving");

    }

    // Coroutine to move the item to the next building
    private IEnumerator move()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log(building[currentside]);
        Debug.Log(item);
        if (building[currentside].checker(this.gameObject, item))
        {
            building[currentside].inputer(item,item_object);
            item = 0;
            item_object = null;
        }

    }

    // Called on each tick to process items
    public override void Tick()
    {
        if (item != 0)
        {
            if (nextBelt[currentside] != null && nextBelt[currentside].activeInHierarchy)
            {
                if (item_object.transform.position == this.transform.position)
                {

                    if (building[currentside] is Belt)
                    {
                        Belt temp = (Belt)building[currentside];
                        temp.reservecon = this.name;
                        building[currentside] = (Building)temp;
                    }
                    StartCoroutine(move());
                    StartCoroutine(nextside());

                }
            }
            else
            {
                StartCoroutine(nextside());
                findNext();
            }

        }
    }
    private IEnumerator nextside()
    {
        yield return null;
        currentside++;
        if (currentside > 2)
        {
            currentside = 0;
        }
    }

    // Method to find the next belt in the conveyer system
    private void findNext()
    {
        for (int i = 0; i < 3; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transforms[i+1].TransformDirection(Vector2.up), 1, 64);
            if (hit.collider)
            {
                Debug.DrawRay(transforms[i+1].position, transforms[i+1].TransformDirection(Vector2.up), Color.red, 5);
                if (hit.transform.gameObject.name == "input")
                {
                    if (hitm[i] != hit.transform.parent.gameObject.name)
                    {
                        GameObject parentObject = hit.transform.parent.gameObject;
                        building[i] = parentObject.GetComponent<Building>();
                        nextBelt[i] = parentObject;
                        hitm[i] = parentObject.name;
                    }
                }
                else
                {
                    nextBelt[i] = null;
                    hitm[i] = null;
                }
            }
            else
            {
                nextBelt[i] = null;
                hitm[i] = null;
            }
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
        Stopcorutine();
        if (item_object)
        {
            item_object.SetActive(false);
            item_object = null;
        }
        beltsystem = GameObject.FindGameObjectWithTag("Beltsystem").GetComponent<Beltsystem>();
        beltsystem.removeConveyer(this);
        this.enabled = false;
        item = 0;
        currentside = 0;
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        gameObject.SetActive(false);

    }
}
