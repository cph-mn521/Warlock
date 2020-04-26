using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Spell : MonoBehaviour, IUseable
{
    public Sprite MyIcon {
        get{
            return icon;    
        }
    }


    private Sprite icon;
    public int slot;

    void Start(){
        icon = GetComponent<Image>().sprite;
    }
    public void Use()
    {
        Debug.Log(slot);
        //TODO send udp cast slot.
    }

   
}
