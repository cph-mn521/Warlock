using System;
using System.Numerics;

namespace GameServer
{
    public class Status
    {
        public bool IsCasting{get;set;}
        public SpellObject CurrentlyCasting{get;set;}

        public DateTime CastingBegan{get;set;}
        
        public Vector3 Target{get;set;}
    }
}