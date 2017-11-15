// https://fsharpforfunandprofit.com/posts/elevated-world/

namespace Training.MapBindApplyAndReturn

module MapBindApplyAndReturn =   

    type E<'T> = Data of 'T  
    
    type MapE<'a,'b> = ('a -> 'b) -> E<'a> -> E<'b>
    type ReturnE<'a> = 'a -> E<'a>
    type ApplyE<'a, 'b> = E<('a -> 'b)> -> E<'a> -> E<'b>
    type BindE<'a, 'b> = ('a -> E<'b> ) ->  (E<'a> -> E<'b>)
    type TraverseE<'a, 'b> = ('a -> E<'b>) -> ( 'a list -> E<'b list>)
    type SequenceE<'a> = E<'a> list -> E<'a list>
    
    let mapE: MapE<'a, 'b>=
        fun f x -> 
            let (Data data) = x
            Data (f data)    
        
    let returnE : ReturnE<'a> = fun x -> (Data x)    
    
    let applyE : ApplyE<'a, 'b> =
        fun fE xE ->
            let (Data a) = xE
            let (Data f) = fE
            Data (f a)    

    let applyE2 fE aE bE =
           let (Data f) = fE
           let (Data a) = aE
           let (Data b) = bE
           Data (f a b)

    let applyE2' fE aE bE =
        applyE (applyE fE aE) bE

    let (<*>) = applyE

    let applyE2'' fE aE bE=
        fE <*> aE <*> bE

    let bindE : BindE<'a, 'b>  =
        fun f xE -> 
            let (Data x) = xE
            f x

    let bindE' xE f = bindE f xE

    let (>>=) = bindE'

    // identity (firts manad law)
    let idE xE = 
        bindE returnE xE    


module Option = 
    
    let apply = fun fOpt xOpt -> 
        match fOpt, xOpt with
        | Some f, Some x ->  Some (f x)
        | _ -> None

    let (<*>) = apply 
    let (<!>) = Option.map

    let lift2 f x y = 
        f <!> x <*> y
        
    let lift3 f x y z = 
        f <!> x <*> y <*> z
        
    let lift4 f x y z w = 
        f <!> x <*> y <*> z <*> w
    
    let bind f xOpt = 
        match xOpt with
        | Some x -> f x
        | _ -> None

module List =
    // The apply function for lists
    // [f;g] apply [x;y] becomes [f x; f y; g x; g y]
    let apply (fList: ('a->'b) list) (xList: 'a list)  = 
        [ for f in fList do
          for x in xList do
              yield f x ]

    let (<*>) = apply
    let (<!>) = List.map

    let lift2 f x y = 
        f <!> x <*> y
        
    let lift3 f x y z = 
        f <!> x <*> y <*> z
        
    let lift4 f x y z w = 
        f <!> x <*> y <*> z <*> w

    let bindList (f: 'a->'b list) (xList: 'a list)  = 
        [ for x in xList do 
          for y in f x do 
              yield y ]

        