using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagOfHolding : MonoBehaviour {
    [SerializeField]
    private Item[] items = new Item[9]; // måske overflødig.

    [SerializeField]
    public ItemButton[] BackpackSlots;

    [SerializeField]
    public Sprite[] ItemSprites;

    private int itemsInBag = 0;
    private int bagCapacity = 9;
    private ItemButton[] itemLocations= new ItemButton[9];
    public static BagOfHolding instance;
    public static BagOfHolding MyInstance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<BagOfHolding> ();
            }
            return instance;
        }
    }
    private void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Debug.Log ("Instance already exists, Destroying object");
            Destroy (this);
        }
    }

    public Item getItem (int id) {
        return items[id];
    }

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame

    public void indexItem(ItemButton _slot, Item _item){
        itemLocations[_item.slot] = _slot;
    }
    public bool hasSpace(){
        return itemsInBag<bagCapacity;
    }

    public void addItem(Item item){
        items[item.slot] = item;
        try{
            int slot = firstEmpty();
            BackpackSlots[slot].setItem(item);
            itemLocations[item.slot] = BackpackSlots[slot];
            itemsInBag++;
        }catch{
            Debug.Log("reee");
        }
        //TODO add similar code from add spell in spellBook
        
    }

    public int firstEmpty(){
        for (int i = 0; i < BackpackSlots.Length; i++)
        {
            if(BackpackSlots[i].isEmpty()){
                return i;
            }
        }
        return 10;
    }

}