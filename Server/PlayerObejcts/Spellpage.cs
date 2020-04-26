using System;

namespace GameServer
{
    public class Spellpage
    {
        private SpellObject MySpellObject;

        DateTime lastCast;
        public Spellpage(SpellObject spell){
            MySpellObject = spell;
        }
    
        public SpellObject cast(){
            return MySpellObject.toObject();
        }

        public void upgrade(){
            MySpellObject.rank = MySpellObject.rank+1;
            Console.WriteLine($"Spell of type {MySpellObject.spellType} has been upgraded and is now rank{MySpellObject.rank}");
        }

        public void reset(){
            
        }
        public int spellRank(){
            return MySpellObject.rank;
        }
    }
}