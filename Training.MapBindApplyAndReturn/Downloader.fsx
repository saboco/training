// https://fsharpforfunandprofit.com/posts/elevated-world/

#load "Result.fs"
#load "Async.fs"
#load "AsyncResult.fs"
#load "ListM.fs"
#load "Downloader.fs"
#load "Timer.fs"

open Downloader
open Timer

System.Uri ("http://google.com") 
|> getUriContent 
|> Async.RunSynchronously 
|> showContentResult 

System.Uri ("http://example.bad") 
|> getUriContent 
|> Async.RunSynchronously 
|> showContentResult 

System.Uri ("http://google.com") 
|> getUriContentSize 
|> Async.RunSynchronously 
|> showContentSizeResult 

System.Uri ("http://example.bad") 
|> getUriContentSize
|> Async.RunSynchronously 
|> showContentSizeResult 

let goodSites = [
    "http://google.com"
    "http://bbc.co.uk"
    "http://fsharp.org"
    "http://microsoft.com"
    ]

let badSites = [
    "http://example.com/nopage"
    "http://bad.example.com"
    "http://verybad.example.com"
    "http://veryverybad.example.com"
    ]

let f() = 
    largestPageSizeA goodSites
    |> Async.RunSynchronously 
    |> showContentSizeResult 

time 10 "largestPageSizeA_Good" f

let f'() = 
    largestPageSizeA badSites
    |> Async.RunSynchronously 
    |> showContentSizeResult 

time 10 "largestPageSizeA_Bad" f'


let largestPageSizeM_AR urls = 
    urls
    |> List.map (fun s -> System.Uri(s) |> getUriContentSize)
    |> ListM.sequenceAsyncResultM 
    |> AsyncResult.map maxContentSize

let f''() = 
    largestPageSizeM_AR goodSites
    |> Async.RunSynchronously 
    |> showContentSizeResult 
time 10 "largestPageSizeM_AR_Good" f''

let f'''() = 
    largestPageSizeM_AR badSites
    |> Async.RunSynchronously 
    |> showContentSizeResult 
time 10 "largestPageSizeM_AR_Bad" f'''