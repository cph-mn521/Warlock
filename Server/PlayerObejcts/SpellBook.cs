using System.Collections.Generic;
using GameServer;

namespace Server
{
    public class SpellBook
    {
        private List<Spellpage> pages;
        private List<SpellType> MyIndex;
        public bool CastSpell(int page){
            pages[index].cast();
        }
        public Spell upgradeSpell(int page){
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

    }
}