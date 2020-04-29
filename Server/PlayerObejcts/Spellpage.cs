using System;

namespace GameServer
{
    public class Spellpage
    {
        private SpellObject MySpellObject;

        public Spellpage(SpellObject spell){
            MySpellObject = spell;
        }
    
        public void prepareCast(){

        }

        public void cast(Status status){
            TimeSpan elapsed = DateTime.Now-MySpellObject.LastCast;
            if(elapsed.TotalMilliseconds >= MySpellObject.Cooldown){
                status.IsCasting = true;
                status.CurrentlyCasting = MySpellObject;
                status.CastingBegan = DateTime.Now;
                ServerSend.Instance.playerAnimation(MySpellObject.owner,MySpellObject.Animation);
            }
        }

        public void upgrade(){
            MySpellObject.rank = MySpellObject.rank+1;
            }

        public void reset(){
            
        }
        public int spellRank(){
            return MySpellObject.rank;
        }
    }
}