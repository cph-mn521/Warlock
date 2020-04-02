﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager> ();

    public static Dictionary<int, SpellManager> spells = new Dictionary<int, SpellManager> ();

    public static bool PlayerSpawned;
    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    public GameObject FireballPrefab;

    private void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Debug.Log ("Instance already exists, Destroying object");
            Destroy (this);
        }
    }
    public void spawnPlayer (int _id, string _username, Vector3 _position, Quaternion _rotation) {
        GameObject _player;
        if (_id == Client.instance.myId) {
            _player = Instantiate (localPlayerPrefab, _position, _rotation);
        } else {
            _player = Instantiate (playerPrefab, _position, _rotation);
        }
        _player.GetComponent<PlayerManager> ().id = _id;
        _player.GetComponent<PlayerManager> ().username = _username;
        _player.GetComponent<PlayerManager> ().position = _position;
        players.Add (_id, _player.GetComponent<PlayerManager> ());
        Debug.Log ($"added player with id:{_id}");
    }

    public void spawnSpell (int _id, Vector3 _position, Quaternion _rotation, int _type) {
        GameObject _spell;
        switch (_type) {
            case 1:
                Debug.Log(_id);
                _spell = Instantiate (FireballPrefab, _position, _rotation);
                _spell.GetComponent<SpellManager>().id = _id;
                _spell.GetComponent<SpellManager>().position = _position;
                _spell.GetComponent<SpellManager>().type = SpellType.Fireball;
                spells.Add (_id, _spell.GetComponent<SpellManager> ());
                break;
            default:
                break;
        }
        
        

    }

    public void removeSpell(int id){
        Destroy(spells[id].gameObject);
        spells.Remove(id);
    }
}