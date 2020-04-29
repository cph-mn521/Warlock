using System;
using System.Numerics;

namespace GameServer {
    public class Fireball : SpellObject {

        
        private int speed = 1;
        private bool addToCleanup = false;

        private int lifetime = 4000;
        public Fireball (int _owner) : base (_owner) {
            Cooldown= 4000;
            CastTime=1000;
            Animation = "Cast2";
            spellType = SpellType.Fireball;
            OffsetScalar=2.1f;
            try{
                rotation = Server.clients[owner].player.rotation;
                position = Server.clients[owner].player.position + this.forward () * 2.1f;
            }catch{
                Console.WriteLine("could not acces the player");
            }

        }

        public override void update () {
            position = position +this.forward () * speed;
            TimeSpan tmElapsed = DateTime.Now - SpawnTime;
            for (int i = 1; i <= Server.clients.Count; i++) {
                try{
                Vector3 playerPos = Server.clients[i].player.position;
                Player _player = Server.clients[i].player;
                if (playerPos != null) {
                    Vector3 distance = playerPos - position;
                    distance.Y = 0;
                    if (distance.Length () <= 2f ) {
                        Vector3 normDistance = this.normalize (distance);
                        _player.addVelocity (normDistance * (.8f + 0.2f * rank));
                        _player.dmg (5);
                        addToCleanup = true;
                    }
                }
                }catch{}
            }
            
            if (addToCleanup || tmElapsed.TotalMilliseconds >= lifetime) {
                Server.cleanUp.Add (this);
            }else{
                ServerSend.Instance.updateObject(this);
            }
        }

    }

}