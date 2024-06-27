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
    [SerializeField]
    private Transform parent;
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
                objectinstance = Instantiate(objectprefab[pools.IndexOf(name)], posistion, Quaternion.Euler(new Vector3(0, 0, Mousehandeler.rotate)),parent);
            }

        }
        else
        {
            Debug.LogError("name does not exist in pool");
        }
        if (objectinstance.transform.localScale.x == 2)
        {
            objectinstance.transform.rotation= Quaternion.Euler(0, 0, 0);
            objectinstance.transform.GetChild(0).rotation= Quaternion.Euler(0, 0, Mousehandeler.rotate);
        }
        else
        {
            objectinstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mousehandeler.rotate));
        }
        Debug.Log(objectinstance.transform.rotation.z);
        Debug.Log(objectinstance.transform.GetChild(0).rotation.z);
        Spawner.holditem = objectinstance;
    }

    public void putinstance(string name, GameObject hit)
    {
        hit.GetComponent<Building>().disableScript();
        pooldic[name].Enqueue(hit);
    }

}
