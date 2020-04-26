using System.Numerics;

namespace GameServer {
    public class Teleport : SpellObject, SpellTargetable {


        private int distancePrRank = 8;
      
        public Teleport (int _owner) : base (_owner) { 
            spellType = SpellType.Teleport;
        }

        public override void update () {
            Player _player = Server.clients[owner].player;
            Vector3 distance = target - _player.position;
            
            if (distance.Length () > distancePrRank * rank) {
                distance = distance * (distancePrRank * rank / distance.Length ());
            }
            distance.Y = 0;
            _player.position = _player.position + distance;
            Server.cleanUp.Add (this);
        }
    }
}