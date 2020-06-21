using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager> ();

    public Dictionary<int, SpellManager> spells = new Dictionary<int, SpellManager> ();

    public bool PlayerSpawned;
    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    public GameObject FireballPrefab;

    public ShopInterface Shop;


    private void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Debug.Log ("Instance already exists, Destroying object");
            Destroy (this);
        }
    }
    public void spawnPlayer (int _id, string _username, float _hp, Vector3 _position, Quaternion _rotation) {
        GameObject _player;
        if (_id == Client.instance.myId) {
            _player = Instantiate (localPlayerPrefab, _position, _rotation);
        } else {
            _player = Instantiate (playerPrefab, _position, _rotation);
        }
        _player.GetComponent<PlayerManager> ().id = _id;
        _player.GetComponent<PlayerManager> ().username = _username;
        _player.GetComponent<PlayerManager>().health=_hp;
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

    public void AnimationTrigger(int playerId,string Animation){
        players[playerId]._animator.SetTrigger(Animation);
        if (Animation == "Death")
        {
            Destroy(players[playerId].gameObject.transform.GetChild(1).gameObject);
        }
    }

    public void removeSpell(int id){
        Destroy(spells[id].gameObject);
        spells.Remove(id);
    }

    public void removePlayer(int id){
        players[id]._animator.SetTrigger("Death");
        Destroy( players[id].gameObject);
        players.Remove(id);
    }

    public void addItem(int Slot,int ItemNr,int playerGold){
        Shop.addItem(ItemNr,Slot);
        Shop.setGold(playerGold);
        // Because price of items is fixed, this doesnt need to change.
    }
    public void addSpell(int slot, int ShopIndex,int playerGold){
        Shop.addSpell(slot,ShopIndex);
        Shop.setGold(playerGold);

        //TODO: Implement shop prices so players can se value of an item....
        Shop.updatePrices(ShopIndex,0);
    }


}