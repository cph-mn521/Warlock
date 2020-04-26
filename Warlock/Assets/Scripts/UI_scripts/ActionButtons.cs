using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ActionButtons : MonoBehaviour, IPointerClickHandler {

    public IUseable MyUseable { get; set; }
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

    // Start is called before the first frame update
    void Start () {
        MyButton = GetComponent<Button> ();
        MyButton.onClick.AddListener (OnClick);
    }

    // Update is called once per frame
    void Update () {

    }

    public void OnClick () {
        if (MyUseable !=null)
        {
            MyUseable.Use();
        }
        
    }

    public void OnPointerClick (PointerEventData eventData) {
        if(HandScript.instance.MyMoveable!=null && HandScript.instance.MyMoveable is IUseable){
            SetUsable(HandScript.instance.MyMoveable as IUseable);
        }
    }
    public void SetUsable (IUseable _usable) {
        this.MyUseable = _usable;
        updateVisuals();
    }
    public void updateVisuals(){
        MyIcon.sprite = HandScript.instance.putMoveable().MyIcon;
        MyIcon.color = Color.white;
    }
}