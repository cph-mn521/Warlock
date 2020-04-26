using System;
using System.Numerics;

namespace GameServer
{
    public class Fireball : SpellObject
    {
        public Fireball(int _owner) : base(_owner)
        {
        }

        public override void update()
        {
            Console.WriteLine("firebaaal");
        }

    }
}