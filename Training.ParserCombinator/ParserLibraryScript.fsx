// Related blog post: http://fsharpforfunandprofit.com/posts/understanding-parser-combinators-3/

#load "ParserLibrary.fs"

open System
open ParserLibrary.Core

let inputABC = "ABC"
let inputZBC = "ZBC"

let parseA = pchar 'A'
let parseB = pchar 'B'
let parseC = pchar 'C'
let parseAThenB = parseA .>>. parseB 
let parseAOrElseB = parseA <|> parseB 
let bOrElseC = parseB <|> parseC
let aAndThenBorC = parseA .>>. bOrElseC
let parseLowercase = anyOf ['a'..'z']
let digit = anyOf ['0'..'9']
let parseABC = pstring "ABC"
let manyA = many (pchar 'A')
let manyAB = many (pstring "AB")
let whitespaceChar = anyOf [' '; '\t'; '\n']
let whitespace = many whitespaceChar 
// define parser for one or more digits
let digits = many1 digit 

let ab = pstring "AB"
let cd = pstring "CD"
let ab_cd = (ab .>> whitespace) .>>. cd

let pdoublequote = pchar '"' 
let quotedInteger = between pdoublequote pint pdoublequote 
// use .>> below
let digitThenSemicolon = digit .>> opt (pchar ';')  

let comma = pchar ','
let zeroOrMoreDigitList = sepBy digit comma
let oneOrMoreDigitList = sepBy1 digit comma

let parseThreeDigitsAsStr = 
    (digit .>>. digit .>>. digit)
    |>> fun ((c1, c2), c3) -> String [| c1; c2; c3 |]

let parseThreeDigitsAsInt = mapP int parseThreeDigitsAsStr

run (pchar 'A') inputABC
run (pchar 'A') inputZBC

run parseA inputABC
run parseA inputZBC

// testing parseAThenB
run parseAThenB "ABC"  // Success (('A', 'B'), "C")
run parseAThenB "ZBC"  // Failure "Expecting 'A'. Got 'Z'"
run parseAThenB "AZC"  // Failure "Expecting 'B'. Got 'Z'"

// testing parseAOrElseB
run parseAOrElseB "AZZ"  // Success ('A', "ZZ")
run parseAOrElseB "BZZ"  // Success ('B', "ZZ")
run parseAOrElseB "CZZ"  // Failure "Expecting 'B'. Got 'C'"

// testing aAndThenBorC
run aAndThenBorC "ABZ"  // Success (('A', 'B'), "Z")
run aAndThenBorC "ACZ"  // Success (('A', 'C'), "Z")
run aAndThenBorC "QBZ"  // Failure "Expecting 'A'. Got 'Q'"
run aAndThenBorC "AQZ"  // Failure "Expecting 'C'. Got 'Q'"

// testing parseLowercase
run parseLowercase "aBC"  // Success ('a', "BC")
run parseLowercase "ABC"  // Failure "Expecting 'z'. Got 'A'"

// testing parseDigit 
run digit "1ABC"  // Success ("1", "ABC")
run digit "9ABC"  // Success ("9", "ABC")
run digit "|ABC"  // Failure "Expecting '9'. Got '|'"

// testing parseThreeDigitsAsStr 
run parseThreeDigitsAsStr "123A"  // Success ("123", "A")

// testing parseThreeDigitsAsStr
run parseThreeDigitsAsInt "123A"  // Success ("123", "A")

// testing parseABC 
run parseABC "ABCDE"  // Success ("ABC", "DE")
printResult (run parseABC "A|CDE")  // Failure "Expecting 'B'. Got '|'"
run parseABC "AB|DE"  // Failure "Expecting 'C'. Got '|'"

// testing manyA
// test some success cases
run manyA "ABCD"  // Success (['A'], "BCD")
run manyA "AACD"  // Success (['A'; 'A'], "CD")
run manyA "AAAD"  // Success (['A'; 'A'; 'A'], "D")
// test a case with no matches
run manyA "|BCD"  // Success ([], "|BCD")

// testing manyAB
run manyAB "ABCD"  // Success (["AB"], "CD")
run manyAB "ABABCD"  // Success (["AB"; "AB"], "CD")
run manyAB "ZCD"  // Success ([], "ZCD")
run manyAB "AZCD"  // Success ([], "AZCD")

// testing whitespace 
run whitespace "ABC"  // Success ([], "ABC")
run whitespace " ABC"  // Success ([' '], "ABC")
run whitespace "\tABC"  // Success (['\t'], "ABC")

// testing digits
run digits "1ABC"  // Success (['1'], "ABC")
run digits "12BC"  // Success (['1'; '2'], "BC")
run digits "123C"  // Success (['1'; '2'; '3'], "C")
run digits "1234"  // Success (['1'; '2'; '3'; '4'], "")

run digits "ABC"   // Failure "Expecting '9'. Got 'A'"

// testing pint
run pint "1ABC"  // Success (1, "ABC")
run pint "12BC"  // Success (12, "BC")
run pint "123C"  // Success (123, "C")
run pint "1234"  // Success (1234, "")

run pint "ABC"   // Failure "Expecting '9'. Got 'A'"

// testing pint
run digitThenSemicolon "1;"  // Success (('1', Some ';'), "")
run digitThenSemicolon "1"   // Success (('1', None), "")

run pint "123C"   // Success (123, "C")
run pint "-123C"  // Success (-123, "C")

run ab_cd "AB \t\nCD"   // Success (("AB", "CD"), "")

// testing quitedInteger
run quotedInteger "\"1234\""   // Success (1234, "")
run quotedInteger "1234"       // Failure "Expecting '"'. Got '1'"

// testing oneOrMoreDigitList 
run oneOrMoreDigitList "1;"      // Success (['1'], ";")
run oneOrMoreDigitList "1,2;"    // Success (['1'; '2'], ";")
run oneOrMoreDigitList "1,2,3;"  // Success (['1'; '2'; '3'], ";")
run oneOrMoreDigitList "Z;"      // Failure "Expecting '9'. Got 'Z'"


// testing zeroOrMoreDigitList 
run zeroOrMoreDigitList "1;"     // Success (['1'], ";")
run zeroOrMoreDigitList "1,2;"   // Success (['1'; '2'], ";")
run zeroOrMoreDigitList "1,2,3;" // Success (['1'; '2'; '3'], ";")
run zeroOrMoreDigitList "Z;"     // Success ([], "Z;")

