﻿using System.Collections;
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
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();
        GameManager.instance.spawnPlayer(_id,_username,_position,_rotation);
    }
    public static void playerPosition(Packet _packet){
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        GameManager.players[_id].transform.position = _position;
    }
    public static void playerRotation(Packet _packet){
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();
        GameManager.players[_id].transform.rotation = _rotation;
    }
}