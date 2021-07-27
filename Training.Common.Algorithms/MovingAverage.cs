namespace Training.Common.Algorithms
{
    public static class MovingAverage
    {
        public static double[] GetMovingAverage(double[] arr, int k)
        {
            var movingAverage = new double[arr.Length - k + 1];
            
            var j = 0;
            var sum = 0d;
            for (var i = 0; i < k; i++)
            {
                sum += arr[i];
            }
            var lastV = arr[k-1];
            foreach (var v in arr)
            {
                var nextIndex = j + k - 1;
                if (nextIndex >= arr.Length)
                {
                    break;
                }

                var nextV = arr[nextIndex];
                
                sum -= lastV;
                sum += nextV;
                movingAverage[j++] = sum / k;
                
                lastV = v;
            }

            return movingAverage;
        }
    }
}