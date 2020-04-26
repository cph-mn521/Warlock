using System.Collections.Generic;
using GameServer;

namespace Server
{
    public class SpellBook
    {
        private List<Spellpage> pages = new List<Spellpage>();
        private List<SpellType> MyIndex = new List<SpellType>();
        public bool CastSpell(int index){
            pages[index].cast();
            return true;
        }
        public void upgradeSpell(int index){
            pages[index].upgrade();
        }
        public void addSpell(SpellType type){
            pages.Add(new Spellpage(type));
        }
        public bool Has(SpellType type){
            return MyIndex.Contains(type);
        }

        public void reset(){

        }

        public SpellBook(){
            MyIndex.Add(SpellType.Fireball);
            pages.Add(new Spellpage(SpellType.Fireball));
        }

    }
}