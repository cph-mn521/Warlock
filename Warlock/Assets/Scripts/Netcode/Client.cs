using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
public class Client : MonoBehaviour {
    public static Client instance;
    public static int dataSizeBuffer = 4096;
    public string ip = "127.0.0.1";
    public int port = 26950;

    public int myId = 0;

    private delegate void PacketHandler (Packet _packet);
    private static Dictionary<int, PacketHandler> packetHandlers;
    public TCP tcp;
    public UDP udp;


    private void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Debug.Log ("Instance already exists, Destroying object");
            Destroy (this);
        }
    }
    private void Start () {
        tcp = new TCP ();
        udp = new UDP ();
    }

    public void ConnectToServer () {
        InitializeClientData();
        tcp.Connect ();
    }

    public class TCP {
        public TcpClient socket;
        private NetworkStream stream;
        private Packet receivedData;
        private byte[] receiveBuffer;
        public void Connect () {
            socket = new TcpClient {
                ReceiveBufferSize = dataSizeBuffer,
                SendBufferSize = dataSizeBuffer
            };
            receiveBuffer = new byte[dataSizeBuffer];
            socket.BeginConnect (instance.ip, instance.port, ConnectCallback, socket);

        }
        private void ConnectCallback (IAsyncResult _result) {
            socket.EndConnect (_result);
            if (!socket.Connected) {
                return;
            }
            receivedData = new Packet ();
            stream = socket.GetStream ();
            stream.BeginRead (receiveBuffer, 0, dataSizeBuffer, ReceiveCallback, null);

        }
        public void sendData(Packet _packet){
            try
            {
                if(socket != null){
                    stream.BeginWrite(_packet.ToArray(),0,_packet.Length(),null,null);
                }
            }
            catch (System.Exception ex)
            {
                Debug.Log($"Error sending data via TCP:{ex}");
            }
        }
        private void ReceiveCallback (IAsyncResult _result) {
            try {
                int _bytelength = stream.EndRead (_result);
                if (_bytelength <= 0) {
                    //TODO: Disconnect.
                    return;
                }
                byte[] _data = new byte[_bytelength];
                Array.Copy (receiveBuffer, _data, _bytelength);
                receivedData.Reset (HandleData (_data));
                stream.BeginRead (receiveBuffer, 0, dataSizeBuffer, ReceiveCallback, null);

            } catch (Exception _ex) {

                //TODO: Disconnect.
            }
        }
        private bool HandleData (byte[] _data) {
            int _packetLength = 0;
            receivedData.SetBytes (_data);
            if (receivedData.UnreadLength () >= 4) {
                _packetLength = receivedData.ReadInt ();
                if (_packetLength <= 0) {
                    return true;
                }
            }
            while (_packetLength > 0 && _packetLength <= receivedData.UnreadLength ()) {
                byte[] _packetBytes = receivedData.ReadBytes (_packetLength);
                ThreadManager.ExecuteOnMainThread (() => {
                    using (Packet _packet = new Packet (_packetBytes)) {
                        int _packetId = _packet.ReadInt ();
                        packetHandlers[_packetId] (_packet);
                    }
                });
                _packetLength = 0;
                if (receivedData.UnreadLength () >= 4) {
                    _packetLength = receivedData.ReadInt ();
                    if (_packetLength <= 0) {
                        return true;
                    }
                }
            }
            if (_packetLength<=1 )
            {
                return true;
            }
            return false;
        }
    }

    public class UDP{
        public UdpClient socket;
        public IPEndPoint endPoint;

        public UDP(){
            endPoint = new IPEndPoint(IPAddress.Parse(instance.ip),instance.port);
        }

        public void Connect(int _localPort){
            socket = new UdpClient(_localPort);
            socket.Connect(endPoint);
            socket.BeginReceive(ReceiveCallback,null);

            using(Packet _packet = new Packet()){
                sendData(_packet);
            }

        }

        public void sendData(Packet _packet){
            try
            {
                _packet.InsertInt(instance.myId);
                if(socket!= null){
                    socket.BeginSend(_packet.ToArray(),_packet.Length(),null,null);
                    Debug.Log("Sending UDP");
                }
            }
            catch (System.Exception ex)
            {
                
                Debug.Log($"Error in sending UDP data: {ex}");
            }
        }

        private void ReceiveCallback(IAsyncResult _result){
            try
            {
                byte[] _data = socket.EndReceive(_result, ref endPoint);
                socket.BeginReceive(ReceiveCallback,null);

                if (_data.Length<4)
                {
                    //TODO: Disconnect. Måske ikke alligevel
                    return;
                }

                HandleData(_data);
            }
            catch (System.Exception)
            {
                
                //TODO: Disconnect. Måske ikke alligevel
            }
        }
        private void HandleData(byte[] _data){
            using(Packet _packet = new Packet(_data)){
                int _packetLength = _packet.ReadInt();
                _data = _packet.ReadBytes(_packetLength);

            }
            ThreadManager.ExecuteOnMainThread( () =>{
                using(Packet _packet=new Packet(_data)){
                    int _packetId = _packet.ReadInt();
                    packetHandlers[_packetId](_packet);
                }
            });
        }
    }
    private void InitializeClientData () {
        packetHandlers = new Dictionary<int, PacketHandler> () { 
               {(int) ServerPackets.welcome, ClientHandle.Welcome},
               {(int)ServerPackets.spawnPlayer, ClientHandle.SpawnPlayer},
               {(int)ServerPackets.playerPosition, ClientHandle.playerPosition},
               {(int)ServerPackets.playerRotation, ClientHandle.playerRotation},
               {(int)ServerPackets.spawnSpell, ClientHandle.spawnSpell},
               {(int)ServerPackets.SpellUpdate, ClientHandle.SpellUpdate},
               {(int)ServerPackets.removeSpell, ClientHandle.removeSpell}


        };
        Debug.Log("Initialized packets");
    }

}