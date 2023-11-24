using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public Items slotItem;
    public Image slotImage;
    public TextMeshProUGUI slotNum;

    public void DropItem() {
        if (slotItem.itemHeld > 1) {
            slotItem.itemHeld--;
            slotNum.text = slotItem.itemHeld.ToString();
        }
        else {
            slotItem.itemHeld--;
            foreach(Transform child in transform) {
                GameObject.Destroy(child.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void ItemOnClicked() {
        if (slotItem.itemHeld > 1) {
            slotItem.itemHeld--;
            slotNum.text = slotItem.itemHeld.ToString();
        }
            
        else {
            slotItem.itemHeld--;
            foreach(Transform child in transform) {             
                GameObject.Destroy(child.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
