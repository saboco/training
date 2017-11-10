using System;
using System.IO;
using System.Net;
using Training.RailwayOrientedProgrammingCs;

namespace Training.RailwayOrientedProgrammingUsecaseExemple
{
    internal static class Program
    {
        private static void Main()
        {
            var addresses = new[]
            {
                new Uri("https://something.out.there"),
                new Uri("https://google.com")
            };

            foreach (var address in addresses)
            {
                Console.WriteLine(ExtractResult(Usecase(address)));
            }
        }

        private static Result<Resource, Failed> Usecase(Uri address)
        {
            var fetchR = ResultExtentions.TryCatchR<Uri, HttpWebResponse, Failed>(Fetch, ExceptionsHandler);
            var validatedFetchR = fetchR
                .Compose(ResultExtentions.BindR<HttpWebResponse, HttpWebResponse, Failed>(ValidateResponse))
                .ComposeR(Execute);

            return validatedFetchR(address);
        }

        private static HttpWebResponse Fetch(Uri address)
        {
            var request = WebRequest.Create(address);
            return (HttpWebResponse) request.GetResponse();
        }

        private static Result<HttpWebResponse, Failed> ValidateResponse(HttpWebResponse response)
        {
            Func<HttpWebResponse, Result<HttpWebResponse, Failed>> notFound = wr =>
                wr.StatusCode == HttpStatusCode.NotFound
                    ? Result<HttpWebResponse, Failed>.NewFailure(new NotFound())
                    : Result<HttpWebResponse, Failed>.NewSuccess(wr);

            Func<HttpWebResponse, Result<HttpWebResponse, Failed>> redirect = wr =>
                wr.StatusCode == HttpStatusCode.Redirect ||
                wr.StatusCode == HttpStatusCode.TemporaryRedirect
                    ? Result<HttpWebResponse, Failed>.NewFailure(
                        new Moved(new Uri(response.Headers[HttpResponseHeader.Location])))
                    : Result<HttpWebResponse, Failed>.NewSuccess(wr);


            Func<HttpWebResponse, Result<HttpWebResponse, Failed>> otherError = wr =>
                wr.StatusCode != HttpStatusCode.OK
                    ? Result<HttpWebResponse, Failed>.NewFailure(new Failed())
                    : Result<HttpWebResponse, Failed>.NewSuccess(wr);

            Func<HttpWebResponse, HttpWebResponse, HttpWebResponse> successF = (wr, _) => wr;
            Func<Failed, Failed, Failed> failedF = (f1, f2) => new Faileds(new[] {f1, f2});

            var valdationF = ResultExtentions.PlusR(successF, failedF,
                ResultExtentions.PlusR(successF, failedF, notFound, redirect), otherError);

            return valdationF(response);
        }

        private static Failed ExceptionsHandler(Exception e)
        {
            switch (e)
            {
                case WebException we when (we.Status == WebExceptionStatus.Timeout):
                    return new Timeout();
                case WebException _:
                    return new NetworkError();
            }
            throw e;
        }

        private static Result<Resource, Failed> Execute(HttpWebResponse r)
        {
            var dataStream = r.GetResponseStream();
            if (dataStream == null) return Result<Resource, Failed>.NewFailure(new Failed());
            
            var data = new StreamReader(dataStream).ReadToEnd();
            return Result<Resource, Failed>.NewSuccess(new Resource(data));
        }

        private static string ExtractResult(Result<Resource, Failed> result)
        {
            Func<Resource, string> successF = s => s.Data;
            Func<Failed, string> failF = f => $"Error: {f}";

            return result.EitherR(successF, failF);
        }
    }
}