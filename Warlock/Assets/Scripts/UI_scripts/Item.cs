using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour, IItem {
    public Sprite MyIcon {
        get {
            return icon;
        }
    }
    private Sprite icon;

    public int slot;
    void Start(){
        icon = GetComponent<Image>().sprite;
    }
    public void setIcon(Sprite sprite){
        icon = sprite;
    }
}