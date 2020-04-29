using System.Numerics;
using System;

namespace GameServer {
    public class Teleport : SpellObject {


        private int distancePrRank = 8;
      
        public Teleport (int _owner) : base (_owner) { 
            spellType = SpellType.Teleport;
            Animation = "Cast1";
            Cooldown = 6000;
            CastTime =200;
        }

        public override void update () {
            Player _player = Server.clients[owner].player;
            Vector3 distance = target - _player.position;
            distance.Y = 0;
            if (distance.Length () > distancePrRank * rank) {
                distance = normalize(distance)*distancePrRank*rank;
                _player.position = _player.position + distance;
            }else{
                 _player.position=target;
            }
        
            Server.cleanUp.Add (this);
        }
    }
}