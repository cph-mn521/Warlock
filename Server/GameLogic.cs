    using System.Collections.Generic;
    using System.Text;
    using System;


namespace GameServer
{
    public class GameLogic
    {

        public static void Update(){

            foreach (Client _client in Server.clients.Values)
            {
                if(_client.player != null){
                    _client.player.Update();
                }
            }
            foreach(Spell _spell in Spell.AllSpells){
                _spell.update();
            }
            #region Cleanup
            foreach(Spell _spell in Spell.spellsToRemove){
                ServerSend.removeSpell(_spell);
                Spell.AllSpells.Remove(_spell);
            }


            Spell.spellsToRemove = new List<Spell> ();

            #endregion

            ThreadManager.UpdateMain();
        }
    }
}