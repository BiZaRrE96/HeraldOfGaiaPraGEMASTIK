using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyer : MonoBehaviour, Building
{
    private Transform[] transforms;
    private SpriteRenderer sprite;
    public GameObject nextBelt;
    public string reservecon;
    public int item = 0;
    public static int index;
    Building building;
    string hitm;

    // Start is called before the first frame update
    void OnEnable()
    {
        Debug.Log("test");
        transforms = gameObject.GetComponentsInChildren<Transform>();
        sprite = transforms[1].GetComponent<SpriteRenderer>();
        TimeTickSystem.TICK += Tick;
        findNext();
    }


    public bool checker(GameObject gameObject, int produce)
    {
        if (reservecon == gameObject.name && item == 0)
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
        item = Produce;
    }
    private IEnumerator move()
    {
        yield return new WaitForEndOfFrame();

        if (building.checker(this.gameObject, item))
        {

            building.inputer(item);
            item = 0;
        }

    }
    private void Tick()
    {
        if (item != 0 && nextBelt != null && nextBelt.activeInHierarchy)
        {

            if (building is Conveyer)
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

    private void findNext()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transforms[1].TransformDirection(Vector2.up), 1, 64);
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
        item = 0;
        this.GetComponentInChildren<BoxCollider2D>().enabled = false;
        gameObject.SetActive(false);
    }


}
