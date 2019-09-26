type Pipe<'a,'b,'r> =
    | Yield of ('b * Pipe<'a,'b,'r>)
    | Await of ('a -> Pipe<'a,'b,'r>)
    | Ready of 'r
    
let ret x = Ready x

let rec bind f m = 
    match m with
    | Yield (b, m') -> Yield (b, bind f m')
    | Await k -> Await (fun a -> bind f (k a))
    | Ready r -> Ready (f r)

let (>>=) m f = bind f m

let (>>) m1 m2 = m1 >>= fun _ -> m2

let empty = Ready ()
let yield' b  = Yield (b, empty)
let await = Await (fun b -> Ready b)

let rec forever p = p >> p

let rec map f =
    forever (await >>= (fun a -> yield' (f a)))

// identity
let rec cat = Await (fun a -> Yield (a, cat))

let rec compose u d =
    match u, d with
    | _            , Ready r       -> Ready r
    | _            , Yield (b, d') -> yield' b >> compose d' u
    | Yield (b, u'), Await k       -> compose u' (k b)
    | Await k      , _             -> await >>= (fun a -> compose (k a) d)
    | Ready r      , _             -> Ready r    

let next p =
    match p with
    | Ready _ -> None
    | Yield (a, rest) -> Some (a, rest)
    | Await k -> failwith "pipe requires more input"

let rec fold f acc producer =
    match next producer with
    | Some (a, rest) -> fold f (f acc a) rest
    | None           -> acc

let collect producer =
    let rec loop p acc =
        match next p with
        | Some (a, rest) -> loop rest (a::acc)
        | None -> List.rev acc
    loop producer []    

let incr = (fun k -> await >>= fun n -> yield' (n + 1) |> k)

let sumpairs () =
    await >>= fun a ->
    await >>= fun b ->
    yield' (a + b)

let count =
    let rec loop n = 
        yield' n >> loop (n + 1)
    loop 0

let fib =
    let rec loop (a, b) = 
        yield' a >> loop (b, a + b)
    loop (0,1)

