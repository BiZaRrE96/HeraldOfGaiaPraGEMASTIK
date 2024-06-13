using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour, Building
{
    // Public variables for testing purpose
    public int storage = 0; // Current storage count of items
    public int pstorage = 0; // Processed storage count
    public int MAX_TIME = 2; // Maximum time to process an item
    public GameObject nextBelt; // Reference to the next belt in the conveyor system

    [SerializeField]
    private int produceItem = 7; // Type of item produced
    [SerializeField]
    private int needItem = 5; // Type of item needed

    // Private variables
    private int time = 0; // Time counter for processing
    private Transform[] transforms; // Array of transforms of the GameObject and its children
    private Building building; // Reference to the building script of the next belt
    private string hitm; // Name of the hit object

    void OnEnable()
    {
        // Initialize transforms array with all child transforms
        transforms = gameObject.GetComponentsInChildren<Transform>();
        // Subscribe to the tick event
        TimeTickSystem.TICK += Tick;
        // Find the next belt in the conveyor system
        findNext();
    }

    private void Tick()
    {
        // Process items if the time exceeds MAX_TIME and storage is sufficient and pstorage no more than 10
        if (time >= MAX_TIME && storage > 2 && pstorage < 10)
        {
            pstorage++;
            storage -= 2;
            time = 0;
            Debug.Log(storage);
        }
        else
        {
            time++;
        }

        // Move items to the next belt if there is processed storage and the next belt is active
        if (pstorage != 0 && nextBelt != null && pstorage > 0 && nextBelt.activeInHierarchy)
        {
            if (building is Conveyer)//so that coneyer not fight with each other
            {
                Conveyer temp = (Conveyer)building;
                temp.reservecon = this.name;
                building = temp;
            }
            StartCoroutine(move());
        }
        else
        {
            findNext();
        }
    }

    private IEnumerator move()
    {
        // Wait until the end of the frame
        yield return new WaitForEndOfFrame();

        // If the next building can accept the item, move it
        if (building.checker(this.gameObject, produceItem))
        {
            building.inputer(produceItem);
            pstorage--;
        }
    }

    private void findNext()
    {
        // Perform a raycast to find the next belt
        RaycastHit2D hit = Physics2D.Raycast(transforms[1].position, transforms[1].TransformDirection(Vector2.up), 1, 64);
        if (hit.collider)
        {
            Debug.DrawRay(transforms[1].position, transforms[1].TransformDirection(new Vector2(0, 1)), Color.red, 5);
            if (hit.transform.gameObject.name == "input")
            {
                // Check if the hit object is a new one
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

    public void Stopcorutine()
    {
        // Unsubscribe from the tick event and stop all coroutines
        TimeTickSystem.TICK -= Tick;
        StopAllCoroutines();
    }

    public void enableScript()
    {
        // Enable the script and its colliders
        this.enabled = true;
        BoxCollider2D[] boxCollider = this.GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D box in boxCollider)
        {
            box.enabled = true;
        }
        findNext();
    }

    public void disableScript()
    {
        // Disable the script and its colliders, reset storage
        Stopcorutine();
        this.enabled = false;
        BoxCollider2D[] boxCollider = this.GetComponentsInChildren<BoxCollider2D>();
        pstorage = 0;
        storage = 0;
        foreach (BoxCollider2D box in boxCollider)
        {
            box.enabled = false;
        }
        gameObject.SetActive(false);
    }

    public bool checker(GameObject gameObject, int produce)
    {
        // Check if the building can accept more items  and incoming produce is the needed item
        if (storage < 10 && produce == this.needItem)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void inputer(int Produce)
    {
        // Increase storage count when an item is input
        storage++;
    }
}
