using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Items thisItem;
    public Inventory playerInventory;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            AddNewItem();
            Destroy(gameObject);
        }
    }

    private void AddNewItem() {
        if (!playerInventory.itemList.Contains(thisItem)) {
            playerInventory.itemList.Add(thisItem);
            thisItem.itemHeld++;
        }
        else {
            thisItem.itemHeld++;
        }

        BagManager.RefreshItem();
    }
}
