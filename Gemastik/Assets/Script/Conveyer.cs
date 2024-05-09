using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyer : MonoBehaviour
{
    Transform[] transforms;
    SpriteRenderer sprite;
    public bool isEmpty = false;
    public GameObject nextBelt;
    public Conveyer con;

    // Start is called before the first frame update
    void Start()
    {
        transforms = gameObject.GetComponentsInChildren<Transform>();
        sprite = transforms[1].GetComponent<SpriteRenderer>();
        TimeTickSystem.onTick += Tick;
        RaycastHit2D hit = Physics2D.Raycast(transforms[1].position, transforms[1].TransformDirection(Vector2.up), 10);
        Debug.DrawRay(transforms[1].position, transforms[1].TransformDirection(Vector2.up), Color.red, 10);
        if (hit.collider)
        {
            nextBelt = hit.transform.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator move()
    {
        yield return new WaitForEndOfFrame();

        print(nextBelt.GetComponent<Conveyer>().con + " dd " + this);
        if (nextBelt.GetComponent<Conveyer>().con == this)
        {
        nextBelt.GetComponent<Conveyer>().isEmpty = false;
        isEmpty = true;

        }
    }
    private void Tick(object sender, TimeTickSystem.onTickEvent e)
    {
        if (!isEmpty)
        {
            if (nextBelt.GetComponent<Conveyer>().isEmpty)
            {
                nextBelt.GetComponent<Conveyer>().con = this;
                StartCoroutine(move());
            }
        }
        else
        {
            Debug.Log(this + " " + e.tick);
        }
    }
    private void FixedUpdate()
    {
        if (isEmpty)
        {
            sprite.color = Color.blue;
        }
        else
        {
            sprite.color = Color.green;
        }
    }
}
