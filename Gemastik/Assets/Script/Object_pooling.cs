using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_pooling : MonoBehaviour
{
    public static Dictionary<string, Queue<GameObject>> pooldic = new Dictionary<string, Queue<GameObject>>();
    [SerializeField]
    private List<GameObject> objectprefab;
    [SerializeField]
    private List<string> pools;
    public static Object_pooling instance;
    private void Awake()
    {
        instance = this;
        foreach (GameObject game in objectprefab)
        {
            pooldic.Add(game.name, new Queue<GameObject>());
            pools.Add(game.name);
        }
    }
    public void getinstance(string name, Vector2 posistion)
    {
        GameObject objectinstance = null;
        if (pools.Contains(name))
        {
            if (pooldic[name].Count > 0)
            {
                Debug.Log("jumlah di pool " + pooldic[name].Count);
                GameObject objecttospawn = pooldic[name].Dequeue();
                objecttospawn.transform.position = posistion;
                objecttospawn.SetActive(true);
                Debug.Log(objecttospawn.name);
                objectinstance = objecttospawn;
            }
            else
            {
                objectinstance = Instantiate(objectprefab[pools.IndexOf(name)], posistion, Quaternion.Euler(new Vector3(0, 0, Mousehandeler.rotate)));
            }

        }
        else
        {
            Debug.LogError("name does not exist in pool");
        }
        Spawner.holditem = objectinstance;
    }

    public void putinstance(string name, GameObject hit)
    {
        hit.GetComponent<Building>().disableScript();
        pooldic[name].Enqueue(hit);
    }

}
