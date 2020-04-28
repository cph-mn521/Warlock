﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour {
    private static void sendTCPData (Packet _packet) {
        _packet.WriteLength ();
        Client.instance.tcp.sendData (_packet);

    }

    private static void sendUDPData (Packet _packet) {
        _packet.WriteLength ();
        Client.instance.udp.sendData (_packet);
    }

    #region Packets
    public static void WelcomeReceived () {
        using (Packet _packet = new Packet ((int) ClientPackets.welcomeReceived)) {
            _packet.Write (Client.instance.myId);
            _packet.Write (UIManager.instance.usernameField.text);

            sendTCPData (_packet);
        };
    }

    public static void PlayerMovement (Vector3 _input) {
        using (Packet _packet = new Packet ((int) ClientPackets.playerMovement)) {

            _packet.Write (_input);
            _packet.Write (GameManager.players[Client.instance.myId].transform.rotation);
            sendUDPData (_packet);
        }
    }

    public static void buyItem(int itemNr)
    {
        using (Packet _packet = new Packet ((int) ClientPackets.requestBuyItem)) {
            _packet.Write(itemNr);
            sendTCPData(_packet);
        }
    }

    public static void buySpell(int spellNr)
    {
        using (Packet _packet = new Packet ((int) ClientPackets.requestBuySpell)) {
            _packet.Write(spellNr);
             sendTCPData(_packet);
        }
    }
    

    public static void sendInputPackage(InputPackage input)
    {
        using (Packet _packet = new Packet ((int) ClientPackets.playerMovement)) {
            _packet.Write(input.Movement);
            _packet.Write(input.Rotation);
            sendUDPData (_packet);
        }
    }

    public static void castSpell (int slot, Vector3 target) {
        using (Packet _packet = new Packet ((int) ClientPackets.playerCast)) {
            _packet.Write (slot);
            _packet.Write(target);
            _packet.Write (GameManager.players[Client.instance.myId].transform.rotation);
            sendUDPData (_packet);
            Debug.Log("pew");
        }
    }

    #endregion
}