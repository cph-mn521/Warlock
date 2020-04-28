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
        private Stats TotalStats;

        public void print(){
            Console.WriteLine($"{TotalStats.HP}{TotalStats.Wheigth}{TotalStats.resistance}{TotalStats.magicResistance}");
        }
        public Backpack(Player _player){
            MyPlayer = _player;
            TotalStats = new Stats();
        }
        public int addItem (Item _item) {
            Slots[firstEmpty()]= _item;
            UpdateStats();
            MyPlayer.updateStats(TotalStats);

            currentItems ++;
            return firstEmpty();
        }

        public void useItem (int index) {
            if(Slots[index] is ItemUsable){
                ItemUsable _tmp = Slots[index] as ItemUsable;
                _tmp.use();
            }
        }
        public bool hasSpace(){
            return currentItems < 9;
        }

        private int firstEmpty () {
            for (int i = 0; i < 9; i++)
            {
                if(Slots[i]==null){
                    return i;
                }
            }
            return -1;
        }

        public void UpdateStats(){
            Stats _stats = new Stats();
            for (int i = 0; i < 9; i++)
            {
                if(Slots[i]!= null){
                    _stats = _stats+Slots[i].stats;
                }
            }
            TotalStats = _stats;
        }

    }

}