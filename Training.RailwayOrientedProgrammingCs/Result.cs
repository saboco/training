// Related to : http://fsharpforfunandprofit.com/rop/

using System;

namespace Training.RailwayOrientedProgrammingCs
{
    public abstract class Result<TSuccess, TFailure> : IEquatable<Result<TSuccess, TFailure>>
    {
        public bool Equals(Result<TSuccess, TFailure> other)
        {
            switch (this)
            {
                case Success s1 when other is Success s2:
                    return s1.Value.Equals(s2.Value);
                case Failure f1 when other is Failure f2:
                    return f1.Error.Equals(f2.Error);
                default: return false;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType()
                   && Equals((Result<TSuccess, TFailure>) obj);
        }

        public override int GetHashCode()
        {
            switch (this)
            {
                case Success s:
                    return s.Value.GetHashCode();
                case Failure f:
                    return f.Error.GetHashCode();
                default: return 0;
            }
        }

        public static bool operator ==(Result<TSuccess, TFailure> left, Result<TSuccess, TFailure> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Result<TSuccess, TFailure> left, Result<TSuccess, TFailure> right)
        {
            return !Equals(left, right);
        }

        public class Success : Result<TSuccess, TFailure>
        {
            public readonly TSuccess Value;

            public Success(TSuccess value)
            {
                Value = value;
            }
        }

        public class Failure : Result<TSuccess, TFailure>
        {
            public readonly TFailure Error;

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

        public override string ToString()
        {
            switch (this)
            {
                case Success s:
                    return s.Value.ToString();
                case Failure f:
                    return f.Error.ToString();
                default: return "";
            }
        }
    }

    public static class FuncExtensions
    {
        public static Func<T1, T3> Compose<T1, T2, T3>(this Func<T1, T2> f1, Func<T2, T3> f2)
        {
            return arg =>
            {
                var r1 = f1(arg);
                return f2(r1);
            };
        }

        public static Func<TA, TA> Tee<TA>(this Action<TA> f)
        {
            return arg =>
            {
                f(arg);
                return arg;
            };
        }
    }

    public static class ResultExtentions
    {
        public static TResult EitherR<T1, T2, TResult>(
            this Result<T1, T2> r,
            Func<T1, TResult> successFunc,
            Func<T2, TResult> failureFunc)
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

        public static Result<TB, TC> BindR<TA, TB, TC>(this Func<TA, Result<TB, TC>> f, Result<TA, TC> r)
        {
            var result = r;
            if (result is Result<TA, TC>.Failure failure)
            {
                return Result<TB, TC>.NewFailure(failure.Error);
            }
            var rSuccess = ((Result<TA, TC>.Success) result).Value;
            return f(rSuccess);
        }

        public static Func<Result<TA, TC>, Result<TB, TC>> BindR<TA, TB, TC>(this Func<TA, Result<TB, TC>> f)
        {
            Result<TB, TC> Func(Result<TA, TC> arg)
            {
                var result = arg;
                if (result is Result<TA, TC>.Failure failure)
                {
                    return Result<TB, TC>.NewFailure(failure.Error);
                }
                
                var r = ((Result<TA, TC>.Success) result).Value;
                return f(r);
            }

            return Func;
        }

        public static Result<TC, TB> PipeR<TA, TB, TC>(this Result<TA, TB> r, Func<TA, Result<TC, TB>> f)
        {
            return f.BindR(r);
        }

        public static Func<TA, Result<TD, TC>> ComposeR<TA, TB, TC, TD>(
            this Func<TA, Result<TB, TC>> f1,
            Func<TB, Result<TD, TC>> f2)
        {
            return f1.Compose(BindR(f2));
        }

        public static Func<TA, Result<TB, TC>> SwitchR<TA, TB, TC>(this Func<TA, TB> f)
        {
            return f.Compose(Result<TB, TC>.NewSuccess);
        }

        public static Func<Result<TA, TC>, Result<TB, TC>> MapR<TA, TB, TC>(this Func<TA, TB> f)
        {
            return r => r.EitherR(SwitchR<TA, TB, TC>(f), Result<TB, TC>.NewFailure);
        }

        public static Func<TA, Result<TB, TC>> TryCatchR<TA, TB, TC>(this Func<TA, TB> f, Func<Exception, TC> exHandler)
        {
            return arg =>
            {
                try
                {
                    var result = f(arg);
                    return Result<TB, TC>.NewSuccess(result);
                }
                catch (Exception e)
                {
                    var result = exHandler(e);
                    return Result<TB, TC>.NewFailure(result);
                }
            };
        }

        public static Func<Result<TA, TC>, Result<TB, TD>> DoubleMapR<TA, TB, TC, TD>(
            Func<TA, TB> successF,
            Func<TC, TD> failF)
        {
            return r => r.EitherR(SwitchR<TA, TB, TD>(successF), failF.Compose(Result<TB, TD>.NewFailure));
        }

        public static Func<TE, Result<TC, TD>> PlusR<TA, TB, TC, TD, TE>(
            Func<TA, TB, TC> addSuccess,
            Func<TD, TD, TD> addFailure,
            Func<TE, Result<TA, TD>> f1,
            Func<TE, Result<TB, TD>> f2)
        {
            return x =>
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
                {
                    return Result<TC, TD>.NewFailure(addFailure(failure3.Error, failure4.Error));
                }

                throw new InvalidOperationException();
            };
        }
    }
}