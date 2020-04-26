using System;

namespace GameServer
{
    public class Spellpage
    {
        private SpellObject MySpellObject;

        DateTime lastCast;
        public Spellpage(SpellType type){
            
        }
    
        public SpellObject CastSpell(){
            return MySpellObject.toObject();
        }


        public void reset(){
            
        }
    }
}