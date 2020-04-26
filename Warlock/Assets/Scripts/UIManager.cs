using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Button[] actionButtons;
    private KeyCode action1, action2, action3, action4, action5, action6;
    public static UIManager instance;
    public GameObject startMenu;
    public GameObject PlayerUI;
    public GameObject Scoreboard;
    public GameObject Shop;
    public GameObject spellBook;

    public InputField usernameField;

    void Start () {
        action1 = KeyCode.Mouse0;
        action2 = KeyCode.Space;
        action3 = KeyCode.Mouse1;
    }
    private void Awake () {
        Shop.SetActive (false);
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Debug.Log ("Instance already exists, Destroying object");
            Destroy (this);
        }
    }

    private void ActionButtonClick (int buttonIndex) {
        actionButtons[buttonIndex].onClick.Invoke ();
    }

    public void ConnectToServer () {
        startMenu.SetActive (false);
        usernameField.interactable = false;
        Client.instance.ConnectToServer ();
    }

    public void toggleShop () {
        Shop.SetActive (!Shop.active);
        if (!spellBook.active && Shop.active) {
            toggleSpellBook ();
        }
    }
    public void toggleSpellBook () {
        spellBook.SetActive (!spellBook.active);
    }

    void Update () {
        if (Input.GetKeyDown (KeyCode.Escape) && HandScript.instance.MyMoveable == null) {
            if (spellBook.active) {
                toggleSpellBook ();
                if (Shop.active) {
                    toggleShop ();
                }
            }

        }
        if (Input.GetKeyDown (action1)) {
            if(!EventSystem.current.IsPointerOverGameObject()){
                ActionButtonClick (0);
            }
        }
        if (Input.GetKeyDown (action2)) {
            ActionButtonClick (1);
        }
        if (Input.GetKeyDown (action3)) {
            if(!EventSystem.current.IsPointerOverGameObject()){
                ActionButtonClick (2);
            }
        }
    }

}