namespace GameServer {
    public class HeavyChest:Item {
        public int price { get { return 25; } }
        public string name { get { return "Heavy Chest"; } }
        public Stats stats { get { return new Stats (0, 10, 0, 0); } }

    }
}