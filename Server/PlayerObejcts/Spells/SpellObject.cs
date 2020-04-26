using System;
using System.Numerics;
namespace GameServer {
    public abstract class SpellObject : SpellInterface, updatable {
        private int MyRank;
        public int rank { get { return MyRank; } set { MyRank = value; } }

        private int MyOwner;
        public int owner { get { return MyOwner; } set { MyOwner = value; } }
        private SpellType MySpellType;
        public SpellType spellType { get { return MySpellType; } set { MySpellType = value; } }

        public int type { get { return 2; } }

        private int MyId;
        public int id { get { return MyId; } set { MyId = value; } }

        private Vector3 MyPosition;
        public Vector3 position { get { return MyPosition; } set { MyPosition = value; } }

        private Quaternion MyRotation;
        public Quaternion rotation { get { return MyRotation; } set { MyRotation = value; } }

        public SpellObject (int _owner) {
            owner = _owner;
            rank = 1;
        }
        public SpellObject toObject () {
            SpellObject clone = (SpellObject) this.MemberwiseClone ();
            clone.owner = owner;
            clone.rank = rank;
            clone.spellType = spellType;
            return clone;
        }

        public abstract void update();
    }
}