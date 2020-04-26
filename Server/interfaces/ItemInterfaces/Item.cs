namespace GameServer
{
    public enum ItemCollection{

    }

    public interface Item
    {   
        ItemCollection MyItem{get;}
         int hp{get;}
         int weight{get;}
         int resistance{get;}
         int value{get;}
    }
}