module ProductApi

type CustId = CustId of string
type ProductId = ProductId of string
type ProductInfo = {ProductName: string; } 

type ApiClient() =
    // static storage
    static let mutable data = Map.empty<string,obj>

    /// Try casting a value
    /// Return Success of the value or Failure on failure
    member private this.TryCast<'a> key (value:obj) =
        match value with
        | :? 'a as a ->
            Result.Success a 
        | _  ->                 
            let typeName = typeof<'a>.Name
            Result.Failure [sprintf "Can't cast value at %s to %s" key typeName]

    /// Get a value
    member this.Get<'a> (id:obj) = 
        let key =  sprintf "%A" id
        printfn "[API] Get %s" key
        match Map.tryFind key data with
        | Some o -> 
            this.TryCast<'a> key o
        | None -> 
            Result.Failure [sprintf "Key %s not found" key]

    /// Set a value
    member this.Set (id:obj) (value:obj) = 
        let key =  sprintf "%A" id
        printfn "[API] Set %s" key
        if key = "bad" then  // for testing failure paths
            Result.Failure [sprintf "Bad Key %s " key]
        else
            data <- Map.add key value data 
            Result.Success ()
           
    member this.Open() =
        printfn "[API] Opening"

    member this.Close() =
        printfn "[API] Closing"

    interface System.IDisposable with
        member this.Dispose() =
            printfn "[API] Disposing"

type ApiAction<'a> = ApiAction of (ApiClient -> 'a)

module ApiAction = 
    open System    

    /// Evaluate the action with a given api
    /// ApiClient -> ApiAction<'a> -> 'a
    let run api (ApiAction action) = 
        let resultOfAction = action api
        resultOfAction

    /// ('a -> 'b) -> ApiAction<'a> -> ApiAction<'b>
    let map f action = 
        let newAction api =
            let x = run api action 
            f x
        ApiAction newAction

    /// 'a -> ApiAction<'a>
    let retn x = 
        let newAction api =
            x
        ApiAction newAction

    /// ApiAction<('a -> 'b)> -> ApiAction<'a> -> ApiAction<'b>
    let apply fAction xAction = 
        let newAction api =
            let f = run api fAction 
            let x = run api xAction 
            f x
        ApiAction newAction

    /// ('a -> ApiAction<'b>) -> ApiAction<'a> -> ApiAction<'b>
    let bind f xAction = 
        let newAction api =
            let x = run api xAction 
            run api (f x)
        ApiAction newAction

    /// ('a -> ApiAction<'b>) -> ('a list -> ApiAction<'b list>)
    let traverse f list =         
        
        let (<*>) = apply
        let cons head tail = head::tail

        let initState = retn []
        let folder head tail = 
            retn cons <*> (f head) <*> tail

        List.foldBack folder list initState

    /// Create an ApiClient and run the action on it
    /// ApiAction<'a> -> 'a
    let execute action =
        use api = new ApiClient()
        api.Open()
        let result = run api action
        api.Close()
        result


module ApiActionResult = 

    let map f  = 
        ApiAction.map (Result.map f)

    let retn x = 
        ApiAction.retn (Result.retn x)

    let apply fActionResult xActionResult = 
        let newAction api =
            let fResult = ApiAction.run api fActionResult 
            let xResult = ApiAction.run api xActionResult 
            Result.apply fResult xResult 
        ApiAction newAction

    let bind f xActionResult = 
        let newAction api =
            let xResult = ApiAction.run api xActionResult 
            // create a new action based on what xResult is
            let yAction = 
                match xResult with
                | Result.Success x -> 
                    // Success? Run the function
                    f x
                | Result.Failure err -> 
                    // Failure? wrap the error in an ApiAction
                    (Result.Failure err) |> ApiAction.retn
            ApiAction.run api yAction  
        ApiAction newAction

    let traverse f list =

        let (<*>) = apply
        let cons head tail = head::tail

        let initState = retn []
        let folder head tail =
            retn cons <*> f head <*> tail

        List.foldBack folder list initState
    
    let either onSuccess onFailure xActionResult = 
        let newAction api =
            let xResult = ApiAction.run api xActionResult 
            let yAction = 
                match xResult with
                | Result.Success x -> onSuccess x 
                | Result.Failure err -> onFailure err
            ApiAction.run api yAction  
        ApiAction newAction


module ProductApi =    

    open Result

    let traverseWithLog log f list =
        // define the applicative functions
        let (<*>) = ApiActionResult.apply
        let retn = ApiActionResult.retn

        // define a "cons" function
        let cons head tail = head :: tail

        // right fold over the list
        let initState = retn []
        let folder head tail = 
            (f head) 
            |> ApiActionResult.either 
                (fun h -> retn cons <*> retn h <*> tail)
                (fun errs -> log errs; tail)
        List.foldBack folder list initState 


    let getProductInfo (productId:ProductId) =
        let action (api:ApiClient) = 
            api.Get<ProductInfo> productId            
        ApiAction action
        
    let getPurchaseIds (custId:CustId) =
        let action (api:ApiClient) = 
            api.Get<ProductId list> custId
        ApiAction action   

    let getPurchaseInfo =
        let getProductInfoLifted =
            getProductInfo
            |> ApiActionResult.traverse 
            |> ApiActionResult.bind 
    
        getPurchaseIds >> getProductInfoLifted

    let getPurchasesInfoWithLog =
        let log errs = printfn "SKIPPED %A" errs 
        let getProductInfoLifted =
            getProductInfo 
            |> traverseWithLog log 
            |> ApiActionResult.bind 
        getPurchaseIds >> getProductInfoLifted

    
    let showResult result =
        match result with
        | Success (productInfoList) -> 
            printfn "SUCCESS: %A" productInfoList
        | Failure errs -> 
            printfn "FAILURE: %A" errs

    