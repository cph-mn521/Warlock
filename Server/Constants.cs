    using System.Collections.Generic;
    using System.Text;
    using System;

    namespace GameServer {
        public class Constants {
            public const int TICKS_PR_SEC = 30; // DETTE SKAL MATCHE Unity's refresh rate, sættes i edit-project Settings - time.
            public const int MS_PR_TICK = 1000/TICKS_PR_SEC;
        }
    }