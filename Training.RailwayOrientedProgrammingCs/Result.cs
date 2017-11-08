using System;

namespace Training.RailwayOrientedProgrammingCs
{
    public abstract class Result<TSuccess, TFailure>
    {
        public class Success : Result<TSuccess, TFailure>
        {
            public TSuccess Value;

            public Success(TSuccess value)
            {
                Value = value;
            }
        }

        public class Failure : Result<TSuccess, TFailure>
        {
            public TFailure Error;

            public Failure(TFailure error)
            {
                Error = error;
            }
        }

        public static Result<TSuccess, TFailure> NewSuccess(TSuccess item)
        {
            return new Success(item);
        }

        public static Result<TSuccess, TFailure> NewFailure(TFailure item)
        {
            return new Failure(item);
        }

        private static Func<T1, T3> SimpleCompose<T1, T2, T3>(Func<T1, T2> f1, Func<T2, T3> f2)
        {
            return arg =>
            {
                var r1 = f1(arg);
                return f2(r1);
            };
        }

        private static TResult Either<T1, T2, TResult>(Func<T1, TResult> successFunc, Func<T2, TResult> failureFunc,
            Result<T1, T2> r)
        {
            switch (r)
            {
                case Result<T1, T2>.Success s:
                    return successFunc(s.Value);
                case Result<T1, T2>.Failure f:
                    return failureFunc(f.Error);
                default:
                    throw new InvalidOperationException("unknown type");
            }
        }

        public static Result<TB, TC> Bind<TA, TB, TC>(Func<TA, Result<TB, TC>> switchFunction,
            Result<TA, TC> twoTrackInput)
        {
            var result = twoTrackInput;
            if (result is Result<TA, TC>.Failure failure)
            {
                return Result<TB, TC>.NewFailure(failure.Error);
            }
            var r = ((Result<TA, TC>.Success)result).Value;
            return switchFunction(r);
        }

        public static Func<Result<TA, TC>, Result<TB, TC>> Bind<TA, TB, TC>(Func<TA, Result<TB, TC>> f)
        {
            Result<TB, TC> Func(Result<TA, TC> arg)
            {
                var result = arg;
                if (result is Result<TA, TC>.Failure failure)
                {
                    // ISSUE: reference to a compiler-generated field
                    return Result<TB, TC>.NewFailure(failure.Error);
                }
                // ISSUE: reference to a compiler-generated field
                var r = ((Result<TA, TC>.Success)result).Value;
                return f(r);
            }

            return Func;
        }

        public static Result<TC, TB> Pipe<TA, TB, TC>(Result<TA, TB> r, Func<TA, Result<TC, TB>> f)
        {
            return Bind(f, r);
        }
        public static Func<TA, Result<TD, TC>> Compose<TA, TB, TC, TD>(
            Func<TA, Result<TB, TC>> f1,
            Func<TB, Result<TD, TC>> f2)
        {
            return SimpleCompose(f1, Bind(f2));
        }

        public static Result<TD, TC> Compose<TA, TB, TC, TD>(
            Func<TA, Result<TB, TC>> f1,
            Func<TB, Result<TD, TC>> f2,
            TA a)
        {
            var result = f1(a);
            if (result is Result<TB, TC>.Failure failure)
            {
                return Result<TD, TC>.NewFailure(failure.Error);
            }

            var r = ((Result<TB, TC>.Success)result).Value;
            return f2(r);
        }

        public static Func<TA, Result<TB, TC>> Switch<TA, TB, TC>(Func<TA, TB> f)
        {
            return SimpleCompose(f, Result<TB, TC>.NewSuccess);
        }

        public static Func<Result<TA, TC>, Result<TB, TC>> Map<TA, TB, TC>(Func<TA, TB> f)
        {
            return r => Either(Switch<TA, TB, TC>(f), Result<TB, TC>.NewFailure, r);
        }

        public static TA Tee<TA>(Action<TA> f, TA x)
        {
            f(x);
            return x;
        }

        public static Result<TB, TC> TryCatch<TA, TB, TC>(Func<TA, TB> f, Func<Exception, TC> exHandler, TA x)
        {
            try
            {
                var result = f(x);
                return Result<TB, TC>.NewSuccess(result);
            }
            catch (Exception e)
            {
                var result = exHandler(e);
                return Result<TB, TC>.NewFailure(result);
            }
        }

        public static Func<Result<TA, TC>, Result<TB, TD>> DoubleMap<TA, TB, TC, TD>(
            Func<TA, TB> successF,
            Func<TC, TD> failF)
        {
            return r => Either(Switch<TA, TB, TD>(successF), SimpleCompose(failF, Result<TB, TD>.NewFailure), r);
        }

        public static Result<TC, TD> Plus<TA, TB, TC, TD, TE>(
            Func<TA, TB, TC> addSuccess,
            Func<TD, TD, TD> addFailure,
            Func<TE, Result<TA, TD>> f1,
            Func<TE, Result<TB, TD>> f2,
            TE x)
        {
            var result1 = f1(x);
            var result2 = f2(x);

            if (result1 is Result<TA, TD>.Success success1 && result2 is Result<TB, TD>.Success success2)
            {
                return Result<TC, TD>.NewSuccess(addSuccess(success1.Value, success2.Value));
            }
            if (result1 is Result<TA, TD>.Failure failure1 && result2 is Result<TB, TD>.Success)
            {
                return Result<TC, TD>.NewFailure(failure1.Error);
            }
            if (result1 is Result<TA, TD>.Success && result2 is Result<TB, TD>.Failure failure2)
            {
                return Result<TC, TD>.NewFailure(failure2.Error);
            }
            if (result1 is Result<TA, TD>.Failure failure3 && result2 is Result<TB, TD>.Failure failure4)
                return Result<TC, TD>.NewFailure(addFailure(failure3.Error, failure4.Error));

            throw new InvalidOperationException();
        }
    }
}