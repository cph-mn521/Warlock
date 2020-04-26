using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer
{
    public interface SpellInterface : updatable
    {
        void update();
        int rank{get;} 
        int owner{get;set;}
        SpellType spellType{get;}

        
    }
}