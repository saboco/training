// https://fsharpforfunandprofit.com/posts/elevated-world/

module Downloader

open Result
// define a millisecond Unit of Measure
type [<Measure>] ms

/// Custom implementation of WebClient with settable timeout
type WebClientWithTimeout(timeout:int<ms>) =
    inherit System.Net.WebClient()

    override this.GetWebRequest(address) =
        let result = base.GetWebRequest(address)
        result.Timeout <- int timeout 
        result

// The content of a downloaded page 
type UriContent = 
    UriContent of System.Uri * string

// The content size of a downloaded page 
type UriContentSize = 
    UriContentSize of System.Uri * int

/// Get the contents of the page at the given Uri
/// Uri -> Async<Result<UriContent>>
let getUriContent (uri:System.Uri) = 
    async {
        use client = new WebClientWithTimeout(1000<ms>) // 1 sec timeout
        try
            printfn " [%s] Started ..." uri.Host
            let! html = client.DownloadStringTaskAsync(uri) |> Async.AwaitTask
            printfn " [%s] ... finished" uri.Host
            let uriContent = UriContent (uri, html)
            return (Result.Success uriContent)
        with
        | ex -> 
            printfn " [%s] ... exception" uri.Host
            let err = sprintf "[%s] %A" uri.Host ex.Message
            return Result.Failure [err ]
        }
/// Make a UriContentSize from a UriContent
/// UriContent -> Result<UriContentSize>
let makeContentSize (UriContent (uri, html)) = 
    if System.String.IsNullOrEmpty(html) then
        Result.Failure ["empty page"]
    else
        let uriContentSize = UriContentSize (uri, html.Length)
        Result.Success uriContentSize 

/// Get the largest UriContentSize from a list
/// UriContentSize list -> UriContentSize
let maxContentSize list = 

    // extract the len field from a UriContentSize 
    let contentSize (UriContentSize (_, len)) = len

    // use maxBy to find the largest  
    list |> List.maxBy contentSize 


/// Get the size of the contents of the page at the given Uri
/// Uri -> Async<Result<UriContentSize>>
let getUriContentSize uri =
    getUriContent uri 
    |> Async.map (Result.bind makeContentSize)

let largestPageSizeA urls = 
    urls
    // turn the list of strings into a list of Uris
    // (In F# v4, we can call System.Uri directly!)
    |> List.map (fun s -> System.Uri(s))   
    
    // turn the list of Uris into a "Async<Result<UriContentSize>> list" 
    |> List.map getUriContentSize
    
    // turn the "Async<Result<UriContentSize>> list" 
    // into an "Async<Result<UriContentSize> list>"
    |> ListM.sequenceAsyncA
    
    // turn the "Async<Result<UriContentSize> list>" 
    // into a "Async<Result<UriContentSize list>>"
    |> Async.map ListM.sequenceResultA
    
    // find the largest in the inner list to get 
    // a "Async<Result<UriContentSize>>"
    |> Async.map (Result.map maxContentSize)


let showContentResult result =
    match result with
    | Success (UriContent (uri, html)) -> 
        printfn "SUCCESS: [%s] First 100 chars: %s" uri.Host (html.Substring(0,100)) 
    | Failure errs -> 
        printfn "FAILURE: %A" errs

        

let showContentSizeResult result =
    match result with
    | Success (UriContentSize (uri, len)) -> 
        printfn "SUCCESS: [%s] Content size is %i" uri.Host len 
    | Failure errs -> 
        printfn "FAILURE: %A" errs

