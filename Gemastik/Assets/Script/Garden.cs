using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garden : MonoBehaviour, Building
{
    public int storage = 0;
    int time = 0;
    public int MAX_TIME = 2;
    Transform[] transforms;
    public GameObject nextBelt;

    [SerializeField]
    private int produceItem = 5;

    Building building;
    string hitm;
    void OnEnable()
    {
        transforms = gameObject.GetComponentsInChildren<Transform>();
        TimeTickSystem.TICK += Tick;
        findNext();
    }


    private void Tick()
    {
        if (time >= MAX_TIME && storage < 20)
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
    private IEnumerator move()
    {
        yield return new WaitForEndOfFrame();

        if (building.checker(this.gameObject, produceItem))
        {
            building.inputer(produceItem);
            storage--;
        }
    }
    private void findNext()
    {

        RaycastHit2D hit = Physics2D.Raycast(transforms[1].position, transforms[1].TransformDirection(Vector2.up), 1, 64);
        if (hit.collider)
        {
            Debug.DrawRay(transforms[1].position, transforms[1].TransformDirection(new Vector2(0, 1)), Color.red, 5);
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
    public void Stopcorutine()
    {
        TimeTickSystem.TICK -= Tick;
        StopAllCoroutines();
    }
    public void enableScript()
    {
        this.enabled = true;
        this.GetComponentInChildren<BoxCollider2D>().enabled = true;
        findNext();
    }
    public void disableScript()
    {
        Stopcorutine();
        this.enabled = false;
        this.GetComponentInChildren<BoxCollider2D>().enabled = false;
        storage = 0;
        gameObject.SetActive(false);
    }

    public bool checker(GameObject gameObject, int produce)
    {
        return false;
    }

    public void inputer(int Produce)
    {
    }
}
