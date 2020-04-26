using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HandScript : MonoBehaviour {
    public static HandScript instance;
    public static HandScript MyInstance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<HandScript>();
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

    public IMoveable MyMoveable { get; set; }

    private Image icon;

    // Start is called before the first frame update
    void Start () {
        icon = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update () {
        icon.transform.position = Input.mousePosition;
    }

    void LateUpdate(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            drop();
        }
    }
    public void TakeMoveable(IMoveable moveable){
        this.MyMoveable = moveable;
        icon.sprite = moveable.MyIcon;
        icon.color = Color.white;
    }
     public IMoveable putMoveable(){
         IMoveable tmp = MyMoveable;
         MyMoveable = null;
         icon.color = new Color(0,0,0,0);

         return tmp;

     }

     private void drop(){
         MyMoveable = null;
         icon.color = new Color(0,0,0,0);
     }
}