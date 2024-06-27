using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_pooling_resource : MonoBehaviour
{
    public Dictionary<string, Queue<GameObject>> pooldic;
    [SerializeField]
    private Transform parent;
    [System.Serializable]
    public class pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<pool> pools;

    public static Object_pooling_resource instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        pooldic = new Dictionary<string, Queue<GameObject>>();

        foreach (pool pool in pools)
        {
            Queue<GameObject> objectpool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab,parent);
                obj.SetActive(false);
                objectpool.Enqueue(obj);
            }
            pooldic.Add(pool.tag, objectpool);
        }
    }

    public GameObject spawnfrompool(string tag, Vector2 position, Quaternion rotation)
    {
        if (!pooldic.ContainsKey(tag))
        {
            Debug.LogWarning("No tag: " + tag);
            return null;
        }

        GameObject objecttospawn = null;

        foreach (GameObject obj in pooldic[tag])
        {
            if (!obj.activeInHierarchy)
            {
                objecttospawn = obj;
                break;
            }
        }

        if (objecttospawn == null)
        {
            // All objects are active, create a new one
            pool poolConfig = pools.Find(p => p.tag == tag);
            if (poolConfig != null)
            {
                objecttospawn = Instantiate(poolConfig.prefab, parent);
                pooldic[tag].Enqueue(objecttospawn);
            }
            else
            {
                Debug.LogWarning("No pool configuration found for tag: " + tag);
                return null;
            }
        }

        // Dequeue the object, set it up and re-enqueue it
        pooldic[tag].Dequeue(); // Remove it from the queue temporarily
        objecttospawn.transform.position = position;
        objecttospawn.transform.rotation = rotation;
        objecttospawn.SetActive(true);
        pooldic[tag].Enqueue(objecttospawn);

        return objecttospawn;
    }
}
