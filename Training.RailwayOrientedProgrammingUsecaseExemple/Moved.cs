using System;

namespace Training.RailwayOrientedProgrammingUsecaseExemple
{
    class Moved : Failed
    {
        public Uri MovedTo { get; }

        public Moved(Uri movedTo)
        {
            this.MovedTo = movedTo;
        }
    }
}