using System;
using System.Threading;
namespace GameServer {
    class Program {
        private static bool _isRunning = false;
        
        static void Main(string[] args){
            Console.Title = "gameServer";
            _isRunning= true;
            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();
            Server.Start(50,26950);
        }
        private static void MainThread(){
            Console.WriteLine($"Main Thread has started. Running at {Constants.TICKS_PR_SEC} Ticks pr second.");
            DateTime _nextLoop = DateTime.Now;
            Spell.InitializeSpells();
            
            while (_isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    GameLogic.Update();

                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PR_TICK);

                    if (_nextLoop > DateTime.Now)
                    {
                        try
                        {
                            Thread.Sleep(_nextLoop - DateTime.Now);//Tjek for non sero...
                        }
                        catch (System.Exception)
                        {                            
                            continue;
                        }
                        
                    }
                }
            }
        }
    }
}