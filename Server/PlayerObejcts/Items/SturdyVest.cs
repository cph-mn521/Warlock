namespace GameServer
{
    public class SturdyVest : Item
    {
        public int price{get{return 25;}}
        public string name{get{return"Sturdy Vest"; }} 
        public Stats stats {get{return new Stats(25,0,0,0);} }

    }
}