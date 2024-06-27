using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour,Building
{
    public List<int> storage = new List<int>(new int[10]);
    public bool checker(GameObject gameObject,int produce)
    {
        if (produce >= storage.Capacity)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void inputer(int Produce,GameObject resource)
    {
        storage[Produce]++;
        StartCoroutine(moving(resource, this.gameObject));
    }

    private IEnumerator moving(GameObject origin, GameObject target)
    {
        while (origin.transform.position != target.transform.position)
        {
            origin.transform.position = Vector2.MoveTowards(origin.transform.position, target.transform.position, 3 * Time.deltaTime);
            yield return null;
        }
        origin.SetActive(false);
        Debug.Log("moving");

    }
    public bool checkResource(Vector2[] needs)
    {
        bool enough = true;
        foreach(Vector2 vector in needs)
        {
            if(storage[Mathf.RoundToInt(vector.x)]>= Mathf.RoundToInt(vector.y))
            {

            }
            else
            {
                enough = false;
                break;
            }
        }
        if (enough)
        {
            foreach (Vector2 vector in needs)
            {
                storage[Mathf.RoundToInt(vector.x)] -= Mathf.RoundToInt(vector.y);
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    public void refundResource(Vector2[] needs)
    {
        foreach (Vector2 vector in needs)
        {
            storage[Mathf.RoundToInt(vector.x)] += Mathf.RoundToInt(vector.y);
        }
    }
    public void disableScript()
    {
    }

    public void enableScript()
    {
    }

}
