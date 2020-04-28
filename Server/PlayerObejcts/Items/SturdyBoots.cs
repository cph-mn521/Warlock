namespace GameServer{
    public class SturdyBoots: Item {
        public int price { get { return 30; } }
        public string name { get { return "Sturdy Vest"; } }
        public Stats stats { get { return new Stats (0, 0, 10, 0); } }

    }
}