namespace Training.Common.Algorithms
{
    public class FlowEdge
    {
        public int From { get; }
        public int To { get; }
        public long Flow { get; set; }
        public long Capacity { get; set; }

        public bool IsResidual => Capacity == 0;
        public long RemainingCapacity => Capacity - Flow;
        public FlowEdge Residual { get; set; }

        public FlowEdge(int from, int to, long capacity)
        {
            From = from;
            To = to;
            Capacity = capacity;
        }
        public void Augment(long bottleNeck)
        {
            Flow += bottleNeck;
            Residual.Flow -= bottleNeck;
        }

        public string Print(int s, int t)
        {
            var u = (From == s) ? "s" : ((From == t) ? "t" : From.ToString());
            var v = (To == s) ? "s" : ((To == t) ? "t" : To.ToString());
            return $"Edge {u} -> {v} | flow = {Flow} |  capacity = {Capacity} | is residual: {IsResidual}";
        }
    }
}
