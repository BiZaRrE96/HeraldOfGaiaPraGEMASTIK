using System.Collections;
using UnityEngine;

public class Conveyer : Belt, Building
{
    private Transform[] transforms;
    private SpriteRenderer sprite;
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
        sprite = transforms[1].GetComponent<SpriteRenderer>();
        findNext();
    }

    // Checker method to verify if this conveyer can accept the item
    public bool checker(GameObject gameObject, int produce)
    {
        return reservecon == gameObject.name && spareitem == 0;
    }

    // Method to input an item into the conveyer
    public void inputer(int Produce)
    {
        spareitem = Produce;
        if (item == 0)
        {
            item = spareitem;
            spareitem = 0;
        }
    }

    // Coroutine to move the item to the next building
    private IEnumerator move()
    {
        yield return new WaitForEndOfFrame();

        if (building.checker(this.gameObject, item))
        {
            building.inputer(item);
            item = spareitem;
            spareitem = 0;
        }
    }

    // Called on each tick to process items
    public override void Tick()
    {
        if (item != 0 && nextBelt != null && nextBelt.activeInHierarchy)
        {
            if (building is Belt)
            {
                Belt temp = (Belt)building;
                temp.reservecon = this.name;
                building = (Building)temp;
            }
            StartCoroutine(move());
        }
        else
        {
            findNext();
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

    // Update the sprite color based on item presence
    private void FixedUpdate()
    {
        if(spareitem == 0 && item == 0)
        {
            sprite.color = Color.red;

        }else if (spareitem == 0)
        {
            sprite.color = Color.yellow;
        }
        else
        {
            sprite.color = Color.green;
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
        sprite = transforms[1].GetComponent<SpriteRenderer>();
        sprite.color = Color.red;
        Stopcorutine();
        beltsystem = GameObject.FindGameObjectWithTag("Beltsystem").GetComponent<Beltsystem>();
        beltsystem.removeConveyer(this);
        this.enabled = false;
        item = 0;
        spareitem = 0;
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        gameObject.SetActive(false);
    }
}
