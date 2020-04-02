using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellType {
    Fireball = 1,
    Ligthning,
    Teleport
}

public class SpellManager : MonoBehaviour {
    // Start is called before the first frame update

    public int id;


    public Vector3 position;

    public SpellType type;

    void FixedUpdate () {
        transform.position = position;
    }
}