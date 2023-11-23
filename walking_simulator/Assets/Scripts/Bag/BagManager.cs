using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagManager : MonoBehaviour
{
    public static BagManager _instance;

    public GameObject bag_UI;
    bool is_open;

    public Inventory bag_Inventory;
    public GameObject slotGrid;
    public Slot slotPrefab;

    private Slot my_item;



    void Awake() {
        if(_instance == null)
            _instance = this;
        else if(_instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable() {
        RefreshItem();
    }

    // Update is called once per frame
    void Update()
    {
        OpenMyBag();
    }

    void OpenMyBag() {
        if (Input.GetKeyDown(KeyCode.Tab)){
            is_open = !is_open;
            bag_UI.SetActive(is_open);
        }
    }

    public static void CreateNewItem(Items item) {
        Slot newItem = Instantiate(_instance.slotPrefab, _instance.slotGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(_instance.slotGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
        newItem.slotNum.text = item.itemHeld.ToString();
    } 

    public static void RefreshItem() {
        
    }
}
