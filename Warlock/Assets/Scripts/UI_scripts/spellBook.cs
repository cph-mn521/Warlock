using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spellBook : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField]
    public List<Spell> spells;

    [SerializeField]
    public Sprite[] icons;

    public GameObject spellPrefab;


    public static spellBook instance;
    public static spellBook MyInstance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<spellBook>();
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


    public Spell GetSpell(int id){
        return spells[id];
    }

    public void addSpell(int slot, Sprite _sprite){
        GameObject _spell = Instantiate(spellPrefab);
        _spell.GetComponent<Spell>().slot=spells.Count;
        _spell.GetComponent<Spellbutton>().setID(spells.Count);
        spells.Add(_spell.GetComponent<Spell>());
        _spell.transform.SetParent(transform);
        _spell.transform.localScale = (new Vector3(1,1,1));
        _spell.GetComponent<Image>().sprite = _sprite;
        
    }
}
