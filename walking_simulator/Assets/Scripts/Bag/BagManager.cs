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


    //我知道这个方法很狗屎，这个方法也确实非常狗屎，但我没更多时间改进或是自己写一个新的改变数字的方法了QAQ
    public static void RefreshItem() {
        for (int i = 0; i < _instance.slotGrid.transform.childCount; ++i) {
            if (_instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(_instance.slotGrid.transform.GetChild(i).gameObject);
        }

        for(int i = 0; i < _instance.bag_Inventory.itemList.Count; ++i) {
            if (_instance.bag_Inventory.itemList[i].itemHeld == 0)
                continue;
            else
                CreateNewItem(_instance.bag_Inventory.itemList[i]);
        }
    }

}
