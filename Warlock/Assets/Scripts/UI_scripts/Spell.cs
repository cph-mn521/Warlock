﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Spell : MonoBehaviour, IUseable {
    public Sprite MyIcon {
        get {
            return icon;
        }
    }

    private Sprite icon;
    public int slot;

    void Start () {
        icon = GetComponent<Image> ().sprite;
    }
    public void Use () {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast (ray, out hit)) {
            Debug.Log(hit.point);
            ClientSend.castSpell (slot, hit.point);
        }
        Debug.Log (slot);
        //TODO send udp cast slot.
    }

}