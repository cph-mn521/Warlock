namespace GameServer
{
    public class MagicAmulet: Item
    {
        public int price { get { return 30; } }
        public string name { get { return "Magic Amulet"; } }
        public Stats stats { get { return new Stats (0, 0, 10, 0); } }
    }
}