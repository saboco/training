namespace Algorithms

module ``Euclid's algorithm for greates common divisor`` =

    let rec gcd a b =
        match a, b with
        (* From http://mfleck.cs.illinois.edu/building-blocks/version-1.0/number-theory.pdf
        
            If k is a non-zero integer, then k divides zero. 
            The largest common divisorofkand zero is k. 
            So gcd(k,0) = gcd(0, k) =k. 
            However, gcd(0,0) isn’t defined. 
            All integers are common divisors of 0 and 0, so there is no greatest one. *)
        | 0, k | k, 0 -> k
        | a, b when (a < 0 || b < 0) -> 
           invalidOp "not defined for (a or b) < 0"
        | 0, 0 -> invalidOp "greates comon divisor is not defined for a=0 and b=0"
        | a, b when b > a -> invalidArg "a" "a should be greater or equal than b"
        | a, b -> gcd b (a % b)
