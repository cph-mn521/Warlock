using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    private Stack<Item> items = new Stack<Item>();

    public bool addItem(Item _item){
        items.Push(_item);
        icon.sprite = _item.MyIcon;
        icon.color = Color.white;
        return true;
    }
}
