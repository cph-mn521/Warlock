using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour {

    public static void Welcome (Packet _packet) {
        string _msg = _packet.ReadString ();
        int _myId = _packet.ReadInt ();
        Debug.Log ($"message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived ();

        Client.instance.udp.Connect (((IPEndPoint) Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void spawnObject (Packet _packet) {
        int spawnType = _packet.ReadInt ();
        int _id = _packet.ReadInt ();
        Vector3 _position = _packet.ReadVector3 ();
        Quaternion _rotation = _packet.ReadQuaternion ();
        switch (spawnType) {
            case 1: //Player spawn
                string _username = _packet.ReadString ();
                float _hp = _packet.ReadFloat ();
                GameManager.instance.spawnPlayer (_id, _username, _hp, _position, _rotation);
                break;
            case 2: //Spell Spawn
                int _type = _packet.ReadInt ();
                GameManager.instance.spawnSpell (_id, _position, _rotation, _type);
                break;
            default:
                break;
        }

    }

    public static void updateObject (Packet _packet) {
        int updateType = _packet.ReadInt ();
        int _id = _packet.ReadInt ();
        Vector3 _position = _packet.ReadVector3 ();
        Quaternion _rotation = _packet.ReadQuaternion ();
        switch (updateType) {
            case 1: //Player Update
                float _hp = _packet.ReadFloat ();
                GameManager.instance.players[_id].health = _hp;
                GameManager.instance.players[_id].position = _position;
                if (Client.instance.myId != _id) {
                    GameManager.instance.players[_id].transform.rotation = _rotation;
                }
                break;
            case 2:
                GameManager.instance.spells[_id].position = _position;
                GameManager.instance.spells[_id].transform.rotation = _rotation;
                break;
            default:
                break;
        }
    }

    public static void removeObject (Packet _packet) {
        int removeType = _packet.ReadInt ();
        int _id = _packet.ReadInt ();
        switch (removeType) {
            case 1:
                GameManager.instance.removePlayer (_id);
                break;
            case 2:
                Debug.Log ("removing spell" + _id);
                GameManager.instance.removeSpell (_id);
                break;
            default:
                break;
        }
    }

    public static void itemPurchase (Packet _packet) {
        int shopIndex = _packet.ReadInt ();
        int Slot = _packet.ReadInt();
        int gold = _packet.ReadInt ();
        GameManager.instance.addItem(Slot, shopIndex, gold);
    }

    public static void spellPurchase (Packet _packet) {
        int shopIndex = _packet.ReadInt ();
        int Slot = _packet.ReadInt ();
        int gold = _packet.ReadInt ();
        GameManager.instance.addSpell (Slot, shopIndex, gold);
    }

    public static void playerAnimation(Packet _packet){
        int playerID = _packet.ReadInt();
        string animation = _packet.ReadString();
        Debug.Log($"player animation detected. {playerID} should now play {animation}");
        GameManager.instance.AnimationTrigger(playerID,animation);
    }

}