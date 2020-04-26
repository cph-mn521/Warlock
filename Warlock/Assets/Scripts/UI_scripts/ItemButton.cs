using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour, IPointerClickHandler {
    // Start is called before the first frame update

    public IItem MyItem;

    public Button MyButton { get; private set; }

    public Image MyIcon {
        get {
            return icon;
        }
        set {
            icon = value;
        }
    }

    [SerializeField]
    private Image icon;
    void Start () {
        MyItem = GetComponent<Item> ();
    }
    public void OnPointerClick (PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            if (HandScript.instance.MyMoveable != null && HandScript.instance.MyMoveable is IItem) {
                if (MyItem == null) {
                    setItem (HandScript.instance.MyMoveable as IItem);
                } else {
                    IItem tmp = MyItem;
                    setItem (HandScript.instance.MyMoveable as IItem);
                    HandScript.instance.TakeMoveable (tmp);
                }

            } else {
                HandScript.instance.TakeMoveable (MyItem);
                MyItem = null;
                icon.color = new Color (0, 0, 0, 0);
            }
        }if(eventData.button == PointerEventData.InputButton.Right){
            OnClick();
        }
    }
    public bool isEmpty () {
        return MyItem == null;
    }
    public void setItem (IItem _usable) {
        this.MyItem = _usable;
        updateVisuals ();
    }
    public void updateVisuals () {
        if (HandScript.instance.MyMoveable != null) {
            MyIcon.sprite = HandScript.instance.putMoveable ().MyIcon;
        }else{
            MyIcon.sprite = MyItem.MyIcon;
        }
        MyIcon.color = Color.white;
    }

    public void OnClick () {
        if (MyItem != null && MyItem is ItemUsable) {
            ItemUsable tmp = MyItem as ItemUsable;
            tmp.use ();
        }

    }

}