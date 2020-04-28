namespace GameServer
{
    public class Stats
    {
        public int HP=0;
        public int Wheigth=0;
        public int resistance=0;
        public int magicResistance = 0;
        public Stats(){}
        public Stats(int _hp,int _wheigth, int _resistance, int _magicResistance){
            HP=_hp;
            Wheigth=_wheigth;
            resistance=_resistance;
            magicResistance=_magicResistance;
        }

        public static Stats operator +(Stats a, Stats b)
            => new Stats(a.HP + b.HP, a.Wheigth+b.Wheigth , a.resistance + b.resistance,a.magicResistance+b.magicResistance);

    }
}