using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junction : Buildcost, Building
{

    private Conveyer[] conveyers;

    private void OnEnable()
    {
        Debug.Log("testing");
        conveyers = gameObject.GetComponentsInChildren<Conveyer>(true);
        foreach (Conveyer conveyer in conveyers)
        {
            conveyer.gameObject.SetActive(true);
        }
    }
    // Checker method to verify if this conveyer can accept the item
    public bool checker(GameObject gameObject, int produce)
    {
        return true;
    }

    // Method to input an item into the conveyer
    public void inputer(int Produce,GameObject resource)
    {

    }
    // Enable the script and its colliders
    public void enableScript()
    {
        conveyers= gameObject.GetComponentsInChildren<Conveyer>();
        GetComponentInChildren<BoxCollider2D>().enabled = true;
        foreach (Conveyer conveyer in conveyers)
        {
            conveyer.enableScript();
        }
    }

    // Disable the script and its colliders
    public void disableScript()
    {
        conveyers = gameObject.GetComponentsInChildren<Conveyer>();
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        foreach (Conveyer conveyer in conveyers)
        {
            conveyer.disableScript();
        }
        gameObject.SetActive(false);
    }
}
