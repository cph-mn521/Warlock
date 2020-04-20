using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Animator _animator;

    public int id;
    public string username;

    public float health;
    public HealthBar _healthbar;

    public Text namedisplay;
    public Vector3 position;

    private Vector3 _lastpos;

    void start(){
        _lastpos=transform.position;        
    }

    void FixedUpdate(){
        transform.position=position;
        _healthbar.setHealth(health);
    }

    void LateUpdate(){
        if(_lastpos==position){
            _animator.SetBool("Walking",false);
        }else{
            _animator.SetBool("Walking",true);
            _lastpos=transform.position;
            Debug.Log("walking ACTIVATED!");
        }
        namedisplay.text = username;
    }    

}
