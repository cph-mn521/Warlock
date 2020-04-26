using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spellbutton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private int spellId;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left){
            HandScript.MyInstance.TakeMoveable(spellBook.MyInstance.GetSpell(spellId));
        }
    }
    public void setID(int id){
        spellId = id;
    }
}
