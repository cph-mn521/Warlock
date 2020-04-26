using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUsable : Item, IItemUsable {
    // Start is called before the first frame update
    public void use () {
        //TODO: Send UDP to use slot
        Debug.Log (slot);
    }
}