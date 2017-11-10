using System.Text;

namespace Training.RailwayOrientedProgrammingUsecaseExemple
{
    public class Failed
    {
    }

    public class Faileds : Failed
    {
        public Failed[] Errors { get; }

        public Faileds(Failed[] errors)
        {
            Errors = errors;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var failed in Errors)
            {
                sb.Append(failed);
            }
            return sb.ToString();
        }
    }
}