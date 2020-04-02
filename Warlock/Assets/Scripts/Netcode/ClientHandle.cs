using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour {
    
    public static void Welcome (Packet _packet) {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();
        Debug.Log($"message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }
    public static void SpawnPlayer(Packet _packet){
        Debug.Log("spawning a player");
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Debug.Log(_position);
        Quaternion _rotation = _packet.ReadQuaternion();
        GameManager.instance.spawnPlayer(_id,_username,_position,_rotation);
    }
    public static void playerPosition(Packet _packet){
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        //Vector3 _velocity = _packet.ReadVector3();    
        try
        {
            GameManager.players[_id].position = _position;
            
        }
        catch (System.Exception ex)
        {
            
            Debug.Log("No player found yet:" + ex.Message);
        }    
        
        //GameManager.players[_id].rigidbody.velocity = _velocity;
    }
    public static void playerRotation(Packet _packet){
        int _id = _packet.ReadInt();        
        Quaternion _rotation = _packet.ReadQuaternion();
        try
        {
            GameManager.players[_id].transform.rotation = _rotation;
        }
        catch (System.Exception ex)
        {
            
            Debug.Log("No player found yet:" + ex.Message);
        }
        
    }

    public static void SpellUpdate(Packet _packet){
        int _id = _packet.ReadInt();        
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();
        try
        {
            Debug.Log($"Spell {_id} moved to {_position}");
            GameManager.spells[_id].position = _position;
            GameManager.spells[_id].transform.rotation = _rotation;
        }
        catch (System.Exception ex)
        {
            
            Debug.Log("No spell found yet:" + ex.Message);
        }

    }

    public static void spawnSpell(Packet _packet){        
        int _id = _packet.ReadInt();
        int _type = _packet.ReadInt();
        Debug.Log($"spawning a spell, type {_type}");
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();
        GameManager.instance.spawnSpell(_id,  _position,  _rotation,_type);
    }

    public static void removeSpell(Packet _packet){        
        int _id = _packet.ReadInt();
        Debug.Log($"Time to kille projectile {_id}");
        GameManager.instance.removeSpell(_id);
    }

}