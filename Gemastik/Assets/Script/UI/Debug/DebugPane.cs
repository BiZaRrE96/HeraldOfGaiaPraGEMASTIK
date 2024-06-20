using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DebugPane : MonoBehaviour
{
    [SerializeField] public static DebugPane instance;
    [SerializeField] GameObject template;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void ClearChild()
    {
        while(this.gameObject.transform.childCount > 0)
        {
            Destroy(this.gameObject.transform.GetChild(0).gameObject);
        }
    }

    public void List(List<GameObject> list)
    {
        ClearChild();
        foreach (GameObject go in list) {
            GameObject child = Instantiate(template);
            child.GetComponent<Text>().text = go.name;
            child.transform.SetParent(this.gameObject.transform, false);
            child.SetActive(true);
        }
    }
}
