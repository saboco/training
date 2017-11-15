
#load "Result.fs"
#load "Async.fs"
#load "AsyncResult.fs"
#load "ProductApi.fs"

open ProductApi
open ProductApi.ProductApi

do
    use api = new ApiClient()
    api.Get "K1" |> printfn "[K1] %A"

    api.Set "K2" "hello" |> ignore
    api.Get<string> "K2" |> printfn "[K2] %A"

    api.Set "K3" "hello" |> ignore
    api.Get<int> "K3" |> printfn "[K3] %A"



let setupTestData (api:ApiClient) =
    //setup purchases
    api.Set (CustId "C1") [ProductId "P1"; ProductId "P2"] |> ignore
    api.Set (CustId "C2") [ProductId "PX"; ProductId "P2"] |> ignore

    //setup product info
    api.Set (ProductId "P1") {ProductName="P1-Name"} |> ignore
    api.Set (ProductId "P2") {ProductName="P2-Name"} |> ignore
    // P3 missing

    // setupTestData is an api-consuming function
    // so it can be put in an ApiAction 
    // and then that apiAction can be executed
let setupAction = ApiAction setupTestData
ApiAction.execute setupAction 

CustId "C1"
|> getPurchaseInfo
|> ApiAction.execute
|> showResult

CustId "CX"
|> getPurchaseInfo
|> ApiAction.execute
|> showResult

CustId "C2"
|> getPurchaseInfo
|> ApiAction.execute
|> showResult

CustId "C2"
|> getPurchasesInfoWithLog
|> ApiAction.execute
|> showResult

