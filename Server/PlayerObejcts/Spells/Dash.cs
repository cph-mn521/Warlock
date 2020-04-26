using System.Numerics;

namespace GameServer {
    public class Dash : SpellObject {
        private bool dashed = false;
        private bool addToCleanup= false;
        public Dash (int _owner) : base (_owner) {
            spellType = SpellType.Dash;
        }

        public override void update () {
            Player _player = Server.clients[owner].player;
            if (!dashed) {
                _player.addVelocity (forward () * (0.2f*(rank-1)) * .5f);
                dashed = true;
            }
            for (int i = 1; i <= Server.clients.Count; i++) {
                try {
                    Vector3 playerPos = Server.clients[i].player.position;
                    Player _playerOthers = Server.clients[i].player;
                    if (playerPos != null) {
                        Vector3 distance = playerPos - _player.position;
                        distance.Y = 0;
                        if (distance.Length () <= 2f && owner != Server.clients[i].player.id) {
                            Vector3 normDistance = this.normalize (distance);
                            _playerOthers.addVelocity (normDistance * (0.2f*(rank-1)) * .5f );
                            _playerOthers.dmg (10*rank);
                            _player.setVelocity(new Vector3(0,0,0));
                            addToCleanup = true;
                        }
                    }
                } catch { }
            }

            if (addToCleanup || _player.getVelocity().Length()<0.2)

                Server.cleanUp.Add (this);
            }
        }
    }