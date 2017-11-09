using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Training.RailwayOrientedProgrammingCs.Tests
{
    [TestFixture]
    public class ResultTests
    {
        [Test]
        public void Compose_ComposeTwoFuncions()
        {
            Func<int, int> f1 = i => i + 1;
            Func<int, int> f2 = i => i + 2;
            var composedF = f1.Compose(f2);

            const int expected = 4;
            var actual = composedF(1);
            Assert.AreEqual(expected, actual);

            //----------------------------------------------------------------------
            Func<int, int> f3 = i => i * 3;
            var composedF2 = composedF.Compose(f3);

            const int expected2 = 12;
            var actual2 = composedF2(1);
            Assert.AreEqual(expected2, actual2);
        }

        [Test]
        public void Bind_BindsAValueToAFunction()
        {
            Func<int, Result<int, string>> f1 = i =>
                i < 5
                    ? Result<int, string>.NewSuccess(i + 5)
                    : Result<int, string>.NewFailure("value was greater than 5");

            var rS = Result<int, string>.NewSuccess(4);
            var actualS = f1.BindR(rS);

            var expectedS = Result<int, string>.NewSuccess(9);
            Assert.AreEqual(expectedS, actualS);

            //----------------------------------------------------------------------
            var rF = Result<int, string>.NewSuccess(6);
            var actualF = f1.BindR(rF);

            var expectedF = Result<int, string>.NewFailure("value was greater than 5");
            Assert.AreEqual(expectedF, actualF);
        }

        [Test]
        public void Either_return_a_modified_success_when_a_success_is_provided()
        {
            var expected = Result<int, string>.NewSuccess(10);
            var input = Result<int, string>.NewSuccess(5);
            var actual = input.EitherR(
                i => Result<int, string>.NewSuccess(i + 5),
                f => Result<int, string>.NewFailure($"Failure {f}"));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Either_return_a_modified_failure_when_a_failure_is_provided()
        {
            var expected = Result<int, string>.NewFailure("Failure i'm a failure");
            var input = Result<int, string>.NewFailure("i'm a failure");
            var actual = input.EitherR(
                i => Result<int, string>.NewSuccess(i + 5),
                f => Result<int, string>.NewFailure($"Failure {f}"));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Pipe_pipe_un_result_into_a_funtion()
        {
            Func<int, Result<int, string>> f = i =>
                i <= 10
                    ? Result<int, string>.Success.NewSuccess(i + 1)
                    : Result<int, string>.NewFailure($"i was greater than 10. It was {i}");

            var expectedS = Result<int, string>.NewSuccess(11);
            var inputS = Result<int, string>.NewSuccess(10);
            var actualS = inputS.PipeR(f);

            Assert.AreEqual(expectedS, actualS);

            //-----------------------------------------------------------------------
            var expectedF = Result<int, string>.NewFailure("i was greater than 10. It was 15");
            var inputF = Result<int, string>.NewSuccess(15);
            var actualF = inputF.PipeR(f);

            Assert.AreEqual(expectedF, actualF);
        }

        [Test]
        public void ComposeR_compose_two_functions()
        {
            Func<int, Result<int, string>> f1 = i =>
                i >= 5
                    ? Result<int, string>.NewSuccess(i + 5)
                    : Result<int, string>.NewFailure($"value was less than than 5. It was {i}");

            Func<int, Result<int, string>> f2 = i =>
                i <= 10
                    ? Result<int, string>.NewSuccess(i + 1)
                    : Result<int, string>.NewFailure($"i was greater than 10. It was {i}");

            var composedF = f1.ComposeR(f2);

            const int inputS = 5;

            var expectedS = Result<int, string>.NewSuccess(11);
            var actualS = composedF(inputS);

            Assert.AreEqual(expectedS, actualS);

            //---------------------------------------------------------------------------------------
            const int inputF = 2;

            var expectedF = Result<int, string>.NewFailure("value was less than than 5. It was 2");
            var actualF = composedF(inputF);

            Assert.AreEqual(expectedF, actualF);
        }

        [Test]
        public void ComposeR_can_compose_two_or_more_functions()
        {
            Func<int, Result<int, string>> f1 = i =>
                i >= 5
                    ? Result<int, string>.NewSuccess(i + 5)
                    : Result<int, string>.NewFailure($"value was less than than 5. It was {i}");

            Func<int, Result<int, string>> f2 = i =>
                i <= 10
                    ? Result<int, string>.NewSuccess(i + 1)
                    : Result<int, string>.NewFailure($"i was greater than 10. It was {i}");

            Func<int, Result<int, string>> f3 = i =>
                i >= 3
                    ? Result<int, string>.NewSuccess(i + 1)
                    : Result<int, string>.NewFailure($"i was less than 3. It was {i}");

            var composedF = f1
                .ComposeR(f2)
                .ComposeR(f3);

            const int inputS = 5;

            var expectedS = Result<int, string>.NewSuccess(12);
            var actualS = composedF(inputS);
            Assert.AreEqual(expectedS, actualS);

            //---------------------------------------------------------------------------------------
            const int inputF = 2;

            var expectedF = Result<int, string>.NewFailure("value was less than than 5. It was 2");
            var actualF = composedF(inputF);
            Assert.AreEqual(expectedF, actualF);
        }

        [Test]
        public void SwitchR_can_convert_any_function_in_a_rFunction()
        {
            Func<int, int> f = i => i + 5;
            var fR = f.SwitchR<int, int, string>();

            const int input = 1;
            var expected = Result<int, string>.NewSuccess(6);
            var actual = fR(input);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MapR_can_map_a_normal_function_into_a_result()
        {
            Func<int, int> f = i => i + 6;
            var mappedF = f.MapR<int, int, string>();
            
            var inputS = Result<int, string>.NewSuccess(2);
            var expectedS = Result<int, string>.NewSuccess(8);
            var actualS = mappedF(inputS);
            
            Assert.AreEqual(expectedS, actualS);
        }

        [Test]
        public void TryCatchR_should_catch_exceptions()
        {
            Action<int> f1 = i =>
            {
                if (i <= 5) throw new Exception("i'm an exception");
            };

            Func<int, Result<int, string>> f2 = i =>
                i <= 10
                    ? Result<int, string>.NewSuccess(i + 1)
                    : Result<int, string>.NewFailure($"i was greater than 10. It was {i}");

            var composedF = f2.ComposeR(f1.Tee().TryCatchR(exception => exception.Message));

            const int inputS = 6;
            var expectedS = Result<int, string>.NewSuccess(7);
            var actualS = composedF(inputS);

            Assert.AreEqual(expectedS, actualS);

            //----------------------------------------------------------------------------------
            const int inputF = 4;
            var expectedF = Result<int, string>.NewFailure("i'm an exception");
            var actualF = composedF(inputF);

            Assert.AreEqual(expectedF, actualF);
        }

        [Test]
        public void DoubleMapR_should_map_success_and_failure()
        {
            Func<int, string> fSuccess = i => $"value was {i}";
            Func<string, string> fError = e => $"Error was {e}";

            var doubleMappedF = ResultExtentions.DoubleMapR(fSuccess, fError);

            var inputS = Result<int, string>.NewSuccess(2);
            var expectedS = Result<string, string>.NewSuccess("value was 2");
            var actualS = doubleMappedF(inputS);
            Assert.AreEqual(expectedS, actualS);

            //-------------------------------------------------------------------------
            var inputF = Result<int, string>.NewFailure("some error");
            var expectedF = Result<string, string>.NewFailure("Error was some error");
            var actualF = doubleMappedF(inputF);
            Assert.AreEqual(expectedF, actualF);
        }

        [Test]
        public void PlusR_should_add_success_and_failures()
        {
            Func<int, int, int> addSuccess = (a, b) => a;
            Func<string, string, string> addFailure = (s1, s2) => string.Concat(s1, ", ", s2);

            Func<int, Result<int, string>> f1 = i =>
                i >= 5
                    ? Result<int, string>.NewSuccess(i)
                    : Result<int, string>.NewFailure($"value was less than than 5. It was {i}");

            Func<int, Result<int, string>> f2 = i =>
                i <= 10
                    ? Result<int, string>.NewSuccess(i)
                    : Result<int, string>.NewFailure($"value was greater than 10. It was {i}");

            Func<int, Result<int, string>> f3 = i =>
                i >= 3
                    ? Result<int, string>.NewSuccess(i)
                    : Result<int, string>.NewFailure($"value was less than 3. It was {i}");

            var plusF1F2 = ResultExtentions.PlusR(addSuccess, addFailure, f1, f2);
            var plusF1F2F3 = ResultExtentions.PlusR(addSuccess, addFailure, plusF1F2, f3);

            var inputS = 5;
            var expectedS = Result<int, string>.NewSuccess(5);
            var actualS = plusF1F2F3(inputS);
            Assert.AreEqual(expectedS, actualS);

            //-----------------------------------------------------------------------------------------
            var inputF = 2;
            var expectedF =
                Result<int, string>.NewFailure(
                    $"value was less than than 5. It was 2, value was less than 3. It was 2");
            var actualF = plusF1F2F3(inputF);
            Assert.AreEqual(expectedF, actualF);
        }

        [Test]
        public void Usecase()
        {
            Func<Request, Result<Request, string>> validate1 = r =>
                r.Name == ""
                    ? Result<Request, string>.NewFailure("Name must not be blank")
                    : Result<Request, string>.NewSuccess(r);


            Func<Request, Result<Request, string>> validate2 = r =>
                r.Name.Length > 50
                    ? Result<Request, string>.NewFailure("Name must not be longer than 50 chars")
                    : Result<Request, string>.NewSuccess(r);

            Func<Request, Result<Request, string>> validate3 = r =>
                r.Email == ""
                    ? Result<Request, string>.NewFailure("Email must not be blank")
                    : Result<Request, string>.NewSuccess(r);

            Func<Result<Request, string>, Result<Request, string>> log = twoTrackInput =>
            {
                Func<Request, Request> success = x =>
                {
                    Debug.WriteLine($"DEBUG. Success so far: {x}");
                    return x;
                };

                Func<string, string> failure = x =>
                {
                    Debug.WriteLine($"ERROR. {x}");
                    return x;
                };

                return ResultExtentions.DoubleMapR(success, failure)(twoTrackInput);
            };

            Func<Request, Result<Request, string>> combinedValidation = r =>
            {
                Func<Request, Request, Request> addSuccess = (r1, _) => r1;
                Func<string, string, string> addFailure = (s1, s2) => s1 + "; " + s2;

                var f1 = ResultExtentions.PlusR(addSuccess, addFailure, validate1, validate2);
                var resultF = ResultExtentions.PlusR(addSuccess, addFailure, f1, validate3);

                return resultF(r);
            };

            Func<Request, Request> canonicalizeEmail = r => new Request(r.Name, r.Email.Trim().ToLower());

            Action<Request> updateDatabase = r => { };
            Func<Exception, string> exHandler = e => e.Message;

            Func<Request, Result<Request, string>> usecase = r =>
            {
                var resultF = combinedValidation
                    .ComposeR(canonicalizeEmail.SwitchR<Request, Request, string>())
                    .ComposeR(updateDatabase.Tee().TryCatchR(exHandler))
                    .Compose(log);

                return resultF(r);
            };

            var goodInput = new Request("Alice", "good");
            var badInput = new Request("", "");

            var badResult = usecase(badInput);
            Debug.WriteLine($"Bad Result = {badResult}");

            var goodRestul = usecase(goodInput);
            Debug.WriteLine($"Good Result = {goodRestul}");
        }

        private class Request
        {
            public string Name { get; }
            public string Email { get; }

            public Request(string name, string email)
            {
                Name = name;
                Email = email;
            }

            public override string ToString()
            {
                return $"Request {{Name: {Name}, Email: {Email}}}";
            }
        }
    }
}