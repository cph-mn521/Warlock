using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using GameServer;

namespace GameServer {
    public class Backpack {

        private Player MyPlayer;
        private Item[] Slots = new Item[9];

        private int currentItems = 0;

        public int addItem (Item _item) {
            return 0;
        }

        public void useItem (int index) {
            if(Slots[index] is ItemUsable){
                ItemUsable _tmp = Slots[index] as ItemUsable;
                _tmp.use();
            }
        }

        private int firstEmpty () {
            return 0;
        }

        public void UpdatePlayer(){}

        private class statblock
        {            
            public int hp;

            public int weight;

            public int resistance;

            public int value;
        }
    }

}