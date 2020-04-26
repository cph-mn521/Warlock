using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer
{
    public interface updatable
    {
        int type{get;}
        int id{get;}
        Vector3 position{get;}
        Quaternion rotation{get;}

    }
}