using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Common.Algorithms
{
    public class RowHousesRob
    {
        public static (int, int[]) RobHouses(int[] money)
        {
            var choiceOne = new List<int>();
            var choiceTwo = new List<int>();
            var rob1 = RobHouses(money, 0, choiceOne);
            var rob2 = RobHouses(money, 1, choiceTwo);

            return rob1 > rob2 ? (rob1, choiceOne.ToArray()) : (rob2, choiceTwo.ToArray());

        }

        public static int RobHouses(int[] money, int i, List<int> robbedHouses)
        {
            if (i >= money.Length)
            { return 0; }

            robbedHouses.Add(i);
            return money[i] + RobHouses(money, i + 2, robbedHouses);
        }

        public static (int, int[]) RobHousesMemo(int[] money)
        {
            var cache = new int[money.Length];
            var rob1 = RobHousesMemo(money, 0, cache);
            var rob2 = RobHousesMemo(money, 1, cache);
            var robbedHouses = new List<int>();

            if (rob1 > rob2)
            {
                for (var i = 0; i < money.Length; i += 2)
                {
                    robbedHouses.Add(i);
                }
                return (rob1, robbedHouses.ToArray());
            }
            else
            {
                for (var i = 1; i < money.Length; i += 2)
                {
                    robbedHouses.Add(i);
                }
                return (rob2, robbedHouses.ToArray());
            }

        }

        public static int RobHousesMemo(int[] money, int i, int[] cache)
        {
            if (i >= money.Length)
            { return 0; }

            //cache is never used as there is no overlapped solutions.
            // solution is linear, only one variable and only one pass for every possibility

            if (cache[i] != 0)
            {
                return cache[i];
            }

            return cache[i] = money[i] + RobHousesMemo(money, i + 2, cache);
        }

        public static (int, int[]) RobHousesDp(int[] money)
        {
            var N = money.Length;
            var dp = new int[N];
            for (var i = 0; i < N; i++)
            {
                if (i - 2 >= 0)
                {
                    dp[i] = dp[i - 2] + money[i];
                }
                else
                {
                    dp[i] = money[i];
                }
            }

            var robbedHouses = new List<int>();
            if (dp[N - 1] > dp[N - 2])
            {
                for (var i = N - 1; i >= 0; i -= 2)
                {
                    robbedHouses.Add(i);
                }
                robbedHouses.Reverse();
                return (dp[N - 1], robbedHouses.ToArray());
            }
            else
            {
                for (var i = N - 2; i >= 0; i -= 2)
                {
                    robbedHouses.Add(i);
                }
                robbedHouses.Reverse();
                return (dp[N - 2], robbedHouses.ToArray());
            }
        }

        public static (int, int[]) RobHousesDp2(int[] money)
        {
            var N = money.Length;
            var dp = new int[N + 1];
            dp[0] = 0;
            dp[1] = money[0];
            var robHouse = new bool[N + 1];
            robHouse[0] = true;
            for (var i = 2; i <= N; i++)
            {
                if (dp[i - 2] + money[i - 1] > dp[i - 1])
                {
                    dp[i] = dp[i - 2] + money[i - 1];
                    robHouse[i - 1] = true;
                }
                else
                {
                    dp[i] = dp[i - 1];
                    robHouse[i - 1] = false;
                }
            }

            var robbedHouses = new List<int>();
            for (var i = N - 1; i >= 0;)
            {
                if (robHouse[i])
                {
                    robbedHouses.Add(i);
                    i -= 2;
                }
                else
                {
                    i--;
                }
            }
            robbedHouses.Reverse();
            return (dp[N], robbedHouses.ToArray());
        }
    }
}
