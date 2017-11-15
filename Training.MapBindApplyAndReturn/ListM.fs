// https://fsharpforfunandprofit.com/posts/elevated-world/

module ListM

    /// Map a Async producing function over a list to get a new Async 
    /// using applicative style
    /// ('a -> Async<'b>) -> 'a list -> Async<'b list>
    let rec traverseAsyncA f list =

        // define the applicative functions
        let (<*>) = Async.apply
        let retn = Async.retn

        // define a "cons" function
        let cons head tail = head :: tail

        // right fold over the list
        let initState = retn []
        let folder head tail = 
            retn cons <*> (f head) <*> tail

        List.foldBack folder list initState 

    /// Transform a "list<Async>" into a "Async<list>" 
    /// and collect the results using apply.
    let sequenceAsyncA x = traverseAsyncA id x

    /// Map a Result producing function over a list to get a new Result 
    /// using applicative style
    /// ('a -> Result<'b>) -> 'a list -> Result<'b list>
    let rec traverseResultA f list =

        // define the applicative functions
        let (<*>) = Result.apply
        let retn = Result.Success

        // define a "cons" function
        let cons head tail = head :: tail

        // right fold over the list
        let initState = retn []
        let folder head tail = 
            retn cons <*> (f head) <*> tail

        List.foldBack folder list initState 

    /// Transform a "list<Result>" into a "Result<list>" 
    /// and collect the results using apply.
    let sequenceResultA x = traverseResultA id x


    /// Map an AsyncResult producing function over a list to get a new AsyncResult
    /// using monadic style
    /// ('a -> AsyncResult<'b>) -> 'a list -> AsyncResult<'b list>
    let rec traverseAsyncResultM f list =

        // define the monadic functions
        let (>>=) x f = AsyncResult.bind f x
        let retn = AsyncResult.retn

        // define a "cons" function
        let cons head tail = head :: tail

        // right fold over the list
        let initState = retn []
        let folder head tail = 
            f head >>= (fun h -> 
            tail >>= (fun t ->
            retn (cons h t) ))

        List.foldBack folder list initState 

    /// Transform a "list<AsyncResult>" into a "AsyncResult<list>"
    /// and collect the results using bind.
    let sequenceAsyncResultM x = traverseAsyncResultM id x



