using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class Constants
    {
        public const float turnPlayerDelay = 2.0f;
        public const float clearMethodDelay = 1.5f;
        public const int totalrounds = 2;
        public const int totalmatch = 2;
        public const float startnextround = 0.5f;
        public const float maxTimer = 10f;

        public static Dictionary<int, string> ranksToText = new Dictionary<int, string>
        {
            {1," (1st)" },
            {2," (2nd)" },
            {3," (3rd)" },
            {4," (4th)" },
            {5," (5th)" },
            {6," (6th)" },
        };
    }
}
