// https://fsharpforfunandprofit.com/posts/monoids-part3/

namespace Training.Monoids

module WorkingWithNonMonoids =

    module CharsToRemove = 
        // Associativity exemple 
        /// store a list of chars to remove
        type CharsToRemove = 
            | CharsToRemove of Set<char>
        
        /// construct a new CharsToRemove
        let subtract (s : string) = 
            s.ToCharArray()
            |> Set.ofArray
            |> CharsToRemove
        
        /// apply a CharsToRemove to a string
        let applyTo (s : string) (CharsToRemove chs) = 
            let isIncluded ch = Set.exists ((=) ch) chs |> not
            let chars = s.ToCharArray() |> Array.filter isIncluded
            System.String(chars)
        
        // combine two CharsToRemove to get a new one
        let (++) (CharsToRemove c1) (CharsToRemove c2) = CharsToRemove(Set.union c1 c2)

//***********************************************************
//************* A case study: Average *********************** 
//***********************************************************

    module Average = 
        type Avg = 
            { total : int
              count : int }
        
        let addAvg avg1 avg2 = 
            { total = avg1.total + avg2.total
              count = avg1.count + avg2.count }
        
        let zero = 
            { total = 0
              count = 0 }
        
        
        let (++) = addAvg
        
        let avg n = {total=n; count=1}
        
        let calcAvg avg = 
            if avg.count = 0 
            then 0.0  
            else float avg.total / float avg.count
            
        let calcAvg2 avg = 
            if avg.count = 0 
            then None
            else Some (float avg.total / float avg.count)
            

    module customer = 

        type Customer = { Name: string; Total: int }

        let doSomething c = 
            let  { Name = name; Total = total} = c
            printfn "Name is %s and Total is %i" name total

        let doSomething' c =
            printf "Name is %s and Total is %i" c.Name c.Total
    