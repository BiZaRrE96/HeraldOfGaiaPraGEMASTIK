using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using TMPro;
using UnityEngine.Assertions;

public class UIDispatcher : MonoBehaviour, ICancelHandler
{
    public static UIDispatcher Instance;
    [SerializeField] private Stack<AccessPane> stack;
    [SerializeField] private GameObject text_template; //for plain MenuItem
    [SerializeField] private GameObject AP_prefab; //for all AccessPanes
    [SerializeField] private GameObject LayoutgroupPrefab;
    [SerializeField] private GameObject SelectionPanePrefab;
    [SerializeField] private GameObject ButtonPrefab;
    [SerializeField] private GameObject SelectableItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        stack = new Stack<AccessPane>();
    }

    void ICancelHandler.OnCancel(UnityEngine.EventSystems.BaseEventData eventData)
    {
        CancelTopMenu();
    }

    public void CancelTopMenu()
    {
        if (stack.Count > 0)
        {
            stack.Pop().Cancel();
            Debug.Log("Cancel menu");
        }
    }

    //If clicked object has a menu, open it (Might be problematic with buttons?)
    public void OpenMenu(GameObject go, PointerEventData eventData)
    {
        IMenuableObject temp;
        if (go.TryGetComponent<IMenuableObject>(out temp)){
            if (!temp.IsUnityNull()) {
                foreach (var pane in stack)
                {
                    if (pane.ID.Equals(temp.GetMenuID()))
                    {
                        //can be replaced by "move AP"
                        return;
                    };
                }
                DrawMenu(temp, eventData.position);
            }
        }
    }

    //Decorate the AccessPane and connect the updateCoroutine
    public void DrawMenu(IMenuableObject IMO, Vector2 Position)
    {
        //create object
        GameObject apObject = Instantiate(IMO.GetPanelPrefab());
        AccessPane ap = apObject.AddComponent<AccessPane>();
        apObject.name = "AccessPane";
        apObject.transform.SetParent(this.gameObject.transform);
        //Position = Position * this.gameObject.GetComponent<Canvas>().scaleFactor;
        //Asumsi : Pane anchor nya di atas kiri
        //Debug.Log($"Pre {Position.ToString()}");
        Position.y = -(this.gameObject.GetComponent<RectTransform>().sizeDelta.y - Position.y);
        //Debug.Log($"Post {Position.ToString()}");
        apObject.GetComponent<RectTransform>().anchoredPosition = Position;

        //assign update
        IEnumerator UpdateCoroutine() {
            int debugCounter = 0;
            int debugCounterTarget = 500;
            bool setup = true; //prevent somethings from running twice
            while (true)
            {
                var updateresults = IMO.OnUpdate();
                foreach (var menuitem in updateresults)
                {
                    GameObject go = apObject;
                    int i = 0;
                    

                    if (debugCounter == debugCounterTarget)
                    {
                        if (go.IsUnityNull())
                        {
                            Debug.Log($"NULL OBJECT ({menuitem.name})");
                        }
                        else
                        {
                            Debug.Log($"{menuitem.name} : {menuitem.title}");
                        }
                    }

                    //set stuff here
                    //add to child first if not found
                    //go.GetComponent<Text>().text = menuitem.title;
                    DrawGivenMenuItem(IMO, menuitem, go, setup);
                }
                setup = false;
                yield return null;
            }
        }

        ap.UpdateCoroutine = StartCoroutine(UpdateCoroutine());
        ap.ID = IMO.GetMenuID();
        stack.Push(ap);
        Debug.Log("OPEN MENU");
    }

    //receives a MenuItem object, draws it in given gameObject;
    void DrawGivenMenuItem(IMenuableObject IMO, MenuItem item, GameObject host, bool setup)
    {
        string TargetName = item.name;
        Transform targetChild = host.transform.Find(item.name);
        if (!targetChild.IsUnityNull())
        {
            Text Title;
            if (targetChild.gameObject.TryGetComponent<Text>(out Title))
            {
                Title.text = item.title;
            }

            if (item is SingleSelectable)
            {
                //draw item display
                ItemDrawer(host.transform.Find(item.name).gameObject, ((SingleSelectable) item).get_current_value);

                //button
                if (setup)
                {
                    Transform button = host.transform.Find(item.name + "[Button]");
                    if (!button.IsUnityNull()) {
                        void DelegateSpawner()
                        {
                            Debug.Log("SELECT BUTTON TEST");
                            OnSelectablePick(IMO, (SingleSelectable) item);
                        }
                        button.gameObject.GetComponent<Button>().onClick.AddListener(DelegateSpawner);
                    }
                    else
                    {
                        Debug.LogWarning($"\"{item.name + "[Button]"}\" not found!");
                    }
                }

            }
        }
        else if (setup)
        {
            Debug.LogWarning($"No such thing as {item.name}!");
        }
    }
    
    void OnSelectablePick(IMenuableObject IMO, SingleSelectable ss)
    {
        //check if pane exists yet

        GameObject pane = Instantiate(SelectionPanePrefab,this.gameObject.transform);
        AccessPane ap = pane.AddComponent<AccessPane>();
        ap.ID = ss.selectableID;
        Transform catalog = pane.transform.Find("Viewport").Find("Content").Find("Catalog");
        Assert.IsNotNull(ss.get_selectable_items);
        foreach (object item in ss.get_selectable_items) {
            GameObject go = Instantiate(SelectableItemPrefab);
            go.GetComponent<SelectableItem>().item = item;
            ItemDrawer(go, item);
            go.transform.SetParent(catalog, false);
        }

        void DelegateSelector(GameObject pickedItem)
        {
            Debug.Log("Item selected");
            IMO.InvokeChange((ss).selectableID, pickedItem.GetComponent<SelectableItem>().item);
            stack.Pop();
            Destroy(pane);
        }

        catalog.gameObject.GetComponent<LocalSelector>().pickEvent.AddListener(DelegateSelector);

        stack.Push(ap);
    }

    void ItemDrawer(GameObject go, object item) {
        //the idea is, menerapkan peraturan2 khusus tergantung tipe object
        //Item drawer should only be used on SelectableItem prefab (might change)
        //image
        GameObject image = go.transform.Find("Display").Find("Image").gameObject;
        //title
        GameObject title = go.transform.Find("Display").Find("Text").gameObject;
        if (go == null)
        {
            return;
        }

        

        if (item.IsUnityNull())
        {
            image.SetActive(false);
            title.GetComponent<TMPro.TextMeshProUGUI>().text = "None";
            return;
        }
        else if (item is DrawableItem)
        {
            Image sp = ((DrawableItem)item).displayImage();
            if (sp != null) {
                image.SetActive(true);
                image.GetComponent<Image>().sprite = sp.sprite;
            }
            else{
                image.SetActive(false);
            }
            title.GetComponent<TMPro.TextMeshProUGUI>().text = ((DrawableItem)item).displayName();
        }
        else
        {
            image.SetActive(false);
            title.GetComponent<TMPro.TextMeshProUGUI>().text = item.ToString();
        }

        
    }


    public void Test()
    {
        Debug.Log("test");
    }
}
