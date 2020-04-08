using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;

    public float health;
    public HealthBar _healthbar;
    public Vector3 position;

    void FixedUpdate(){
        transform.position=position;
        _healthbar.setHealth(health);
    }
}
