using System;

namespace GameServer
{
    public class Frostlance : SpellObject
    {
        public Frostlance(int _owner) : base(_owner)
        {
        }

        public override void update()
        {
            Console.WriteLine("frooostLAAANCE!");
        }
    }
}