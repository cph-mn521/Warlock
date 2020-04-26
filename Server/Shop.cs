using System.Collections.Generic;
using GameServer;

namespace GameServer
{
    public class Shop
    {


        private int pricePrRank = 10;
        private List<Item> itemsForSale = new List<Item>();

        private List<SpellObject> spellsForSale = new List<SpellObject>();

        public void buyItem(Player _player, int ItemId){

        }

        public Shop(){
            spellsForSale.Add(new Fireball(-1));
            spellsForSale.Add(new Teleport(-1));
        }

        public void BuySpell(Player _player, int SpellId){
            SpellObject chosen = spellsForSale[SpellId];
            if(_player.spellBook.Has(chosen.spellType )){
                int price = _player.spellBook.rankOfSpell(chosen.spellType)*5;
                if(_player.gold >= price){
                    _player.gold -=price;
                    _player.spellBook.upgradeSpell(chosen.spellType);
                    //ServerSend.Instance.sendGold(_player.gold);
                }
            }else{
                int price = pricePrRank;
                if(_player.gold >= price){
                    _player.gold -=price;
                    _player.spellBook.addSpell(chosen);
                    ServerSend.Instance.spellPurchase(_player.id, SpellId, _player.spellBook.getSlot(chosen.spellType),_player.gold);
                }
            }
        }

    }
}