using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class HoverDisplay : MonoBehaviour
{
    [SerializeField] private GameObject Title;
    [SerializeField] private GameObject Body;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    void SetTitle(string title)
    {
        Title.GetComponent<TextMeshProUGUI>().text = title;
        if (title.Equals("") || title.IsUnityNull())
        {
            Title.SetActive(false);
        }
        else
        {
            Title.SetActive(true);
        }
    }

    void SetBody(string body)
    {
        Body.GetComponent<TextMeshProUGUI>().text = body;
        if (Body.Equals("") || Body.IsUnityNull())
        {
            Body.SetActive(false);
        }
        else
        {
            Body.SetActive(true);
        }
    }

    public void Display(string title, string body)
    {
        SetTitle(title);
        SetBody(body);
    }

    public void Display(HoverableObject hoverableObject) {
        Display(hoverableObject.title, hoverableObject.body);
    }
}
