module V1 =
    type Number =
        | Zero of (unit -> unit)
        | Next of (unit -> Number)

    let zero = Zero (fun () -> ())
    let one = Next (fun () -> zero)
    let two = Next (fun () -> one)

    let count n =
        let rec loop acc n =
            match n with
            | Zero _ -> acc
            | Next f -> loop (acc + 1) (f())
        loop 0 n

    count zero
    count one
    count two

module V2 =

    type Number =
        | Zero
        | Next of (unit -> Number)

    let zero = Zero
    let one = Next (fun () -> zero)
    let two = Next (fun () -> one)
    let three = Next (fun () -> two)

    let count n = 
        let rec loop acc n =
            match n with
            | Zero -> acc
            | Next f -> loop (acc + 1) (f ())
        loop 0 n

    count three

module V3 =

    type Peano =
        | Zero
        | Succ of Peano

    let zero = Zero
    let one = Succ zero
    let two = Succ one

    let rec count n =
        match n with
        | Zero -> 0
        | Succ p -> 1 + count p

    count two