using System;
using System.Collections.Generic;
using GameServer;

namespace Server {
    public class SpellBook {
        private int owner;
        private List<Spellpage> pages = new List<Spellpage> ();
        private List<SpellType> MyIndex = new List<SpellType> ();
        public SpellObject CastSpell (int index) {
            return pages[index].cast ();
        }

        public int getSlot(SpellType _type){
             for (int i = 0; i < MyIndex.Count; i++) {
                if (MyIndex[i] == _type) {
                    return i;
                }
            }
            return 0;
        }

        public void upgradeSpell (SpellType _type) {
            for (int i = 0; i < MyIndex.Count; i++) {
                if (MyIndex[i] == _type) {
                    pages[i].upgrade();
                    
                }
            }
        }
        public void addSpell (SpellObject spell) {
            spell.owner=owner;
            pages.Add (new Spellpage (spell));
            MyIndex.Add (spell.spellType);
            Console.WriteLine($"Spell of type {spell.spellType} has been learned");
        }
        public bool Has (SpellType type) {
            return MyIndex.Contains (type);
        }

        public int size () {
            return pages.Count;
        }

        public void reset () {

        }

        public SpellBook (int _owner) {
            owner = _owner;
            addSpell (new Fireball (owner));
        }

        public int rankOfSpell (SpellType _type) {

            for (int i = 0; i < MyIndex.Count; i++) {
                if (MyIndex[i] == _type) {
                    return pages[i].spellRank ();
                }
            }
            return 0;
        }

    }
}