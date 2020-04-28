using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopInterface : MonoBehaviour {
    [SerializeField]
    public Sprite[] ItemSprites;

    [SerializeField]
    public Sprite[] SpellSprites;

    public Text MyGold;

    public GameObject itemShop;
    public GameObject spellShop;
    private List<int> itemPrices = new List<int> ();
    private List<int> spellPrices = new List<int> ();

    void Start () {
        #region Hardscoding item prices
        itemPrices.Add (10);
        itemPrices.Add (10);
        itemPrices.Add (10);
        #endregion
        #region Hardcoding spell prices
        spellPrices.Add (5);
        spellPrices.Add (5);
        spellPrices.Add (5);
        #endregion
    }

    #region Shopping functionality
    public void BuyItem (int itemNr) {
        if (!BagOfHolding.instance.hasSpace ()) {
            Debug.Log ("no room seniorer");
            return;
        }
        int _current = int.Parse (MyGold.text);
        if (_current >= itemPrices[itemNr]) {
            ClientSend.buyItem (itemNr);

            /*

            */
        } else {
            Debug.Log ("not enough gold!");
            //TODO: Add ingame error message....
        }

    }
    public void addItem (int itemNr, int Slot) {
        int _current = int.Parse (MyGold.text);
        Item _newItem = new Item ();
        _newItem.slot = Slot;
        _newItem.setIcon (ItemSprites[itemNr]);
        BagOfHolding.instance.addItem (_newItem);
        MyGold.text = "" + (_current - itemPrices[itemNr]);
    }


    public void buySpell (int spellNr) {
        int _current = int.Parse (MyGold.text);
        if (_current >= spellPrices[spellNr]) {
            ClientSend.buySpell (spellNr);
            //
        } else {
            Debug.Log ("not enough gold!");
            //TODO: Add ingame error message....
        }
    }

    public void addSpell(int Slot, int ShopIndex){
        spellBook.instance.addSpell(Slot,SpellSprites[ShopIndex]);
    }
    public void setGold(int amount){
        MyGold.text = amount.ToString();
    }

    public void updatePrices(int index,int newprice){
        spellPrices[index]=newprice;
    }
    #endregion

    public void showSpells () {
        itemShop.SetActive (false);
        spellShop.SetActive (true);
    }
    public void showItems () {
        itemShop.SetActive (true);
        spellShop.SetActive (false);
    }

}