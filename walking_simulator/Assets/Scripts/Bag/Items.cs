using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Items : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int itemHeld;

    public void ItemUse() {
        itemHeld--;
        if (itemName == "Baba") {
            Debug.Log("You eat the Baba!");
        }
        if (itemName == "Garbage") {
            Debug.Log("You use the Garbage!");
        }
    }
}
