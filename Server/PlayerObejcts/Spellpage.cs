using System;

namespace GameServer
{
    public class Spellpage
    {
        private SpellObject MySpellObject;

        DateTime lastCast;
        public Spellpage(SpellType type){
            
        }
    
        public SpellObject cast(){
            return MySpellObject.toObject();
        }

        public void upgrade(){
            MySpellObject.rank = MySpellObject.rank+1;
        }

        public void reset(){
            
        }
    }
}