module JsonParser

open ParserLibrary.Core
open System

type JValue = 
    | JString of string
    | JNumber of float
    | JBool   of bool
    | JNull
    | JObject of Map<string, JValue>
    | JArray  of JValue list

let jNull = 
    pstring "null" 
    >>% JNull   // using new utility combinator
    <?> "null"  

let jBool =   
    let jtrue = 
        pstring "true" 
        >>% JBool true   // map to JBool
    let jfalse = 
        pstring "false" 
        >>% JBool false  // map to JBool 

    // choose between true and false
    jtrue <|> jfalse
    <?> "bool"           // give it a label

let jUnescapedChar = 
    let label = "char"
    satisfy (fun ch -> ch <> '\\' && ch <> '\"') label 

/// Parse an escaped char
let jEscapedChar = 
    [ 
    // (stringToMatch, resultChar)
    ("\\\"",'\"')      // quote
    ("\\\\",'\\')      // reverse solidus 
    ("\\/",'/')        // solidus
    ("\\b",'\b')       // backspace
    ("\\f",'\f')       // formfeed
    ("\\n",'\n')       // newline
    ("\\r",'\r')       // cr
    ("\\t",'\t')       // tab
    ] 
    // convert each pair into a parser
    |> List.map (fun (toMatch,result) -> 
        pstring toMatch >>% result)
    // and combine them into one
    |> choice
    <?> "escaped char" // set label

/// Parse a unicode char
let jUnicodeChar = 
    
    // set up the "primitive" parsers  
    let backslash = pchar '\\'
    let uChar = pchar 'u'
    let hexdigit = anyOf (['0'..'9'] @ ['A'..'F'] @ ['a'..'f'])

    // convert the parser output (nested tuples)
    // to a char
    let convertToChar (((h1,h2),h3),h4) = 
        let str = sprintf "%c%c%c%c" h1 h2 h3 h4
        Int32.Parse(str,Globalization.NumberStyles.HexNumber) |> char

    // set up the main parser
    backslash  >>. uChar >>. hexdigit .>>. hexdigit .>>. hexdigit .>>. hexdigit
    |>> convertToChar 

let quotedString = 
    let quote = pchar '\"' <?> "quote"
    let jchar = jUnescapedChar <|> jEscapedChar <|> jUnicodeChar 

    // set up the main parser
    quote >>. manyChars jchar .>> quote 

/// Parse a JString
let jString = 
    // wrap the string in a JString
    quotedString
    |>> JString           // convert to JString
    <?> "quoted string"   // add label

/// Parse a JNumber
let jNumber = 

    // set up the "primitive" parsers  
    let optSign = opt (pchar '-')

    let zero = pstring "0"

    let digitOneNine = 
        satisfy (fun ch -> Char.IsDigit ch && ch <> '0') "1-9"

    let digit = 
        satisfy (fun ch -> Char.IsDigit ch ) "digit"

    let point = pchar '.'

    let e = pchar 'e' <|> pchar 'E'

    let optPlusMinus = opt (pchar '-' <|> pchar '+')

    let nonZeroInt = 
        digitOneNine .>>. manyChars digit 
        |>> fun (first,rest) -> string first + rest

    let intPart = zero <|> nonZeroInt

    let fractionPart = point >>. manyChars1 digit

    let exponentPart = e >>. optPlusMinus .>>. manyChars1 digit

    // utility function to convert an optional value to a string, or "" if missing
    let ( |>? ) opt f = 
        match opt with
        | None -> ""
        | Some x -> f x

    let convertToJNumber (((optSign,intPart),fractionPart),expPart) = 
        // convert to strings and let .NET parse them! - crude but ok for now.

        let signStr = 
            optSign 
            |>? string   // e.g. "-"

        let fractionPartStr = 
            fractionPart 
            |>? (fun digits -> "." + digits )  // e.g. ".456"

        let expPartStr = 
            expPart 
            |>? fun (optSign, digits) ->
                let sign = optSign |>? string
                "e" + sign + digits          // e.g. "e-12"

        // add the parts together and convert to a float, then wrap in a JNumber
        (signStr + intPart + fractionPartStr + expPartStr)
        |> float
        |> JNumber

    // set up the main parser
    optSign .>>. intPart .>>. opt fractionPart .>>. opt exponentPart
    |>> convertToJNumber
    <?> "number"   // add label

let jNumber_ = jNumber .>> spaces1


let createParserForwardedToRef<'a>() =

    let dummyParser= 
        let innerFn input : Result<'a * Input> = failwith "unfixed forwarded parser"
        {parseFn=innerFn; label="unknown"}
    
    // ref to placeholder Parser
    let parserRef = ref dummyParser 

    // wrapper Parser
    let innerFn input = 
        // forward input to the placeholder
        runOnInput !parserRef input 
    let wrapperParser = {parseFn=innerFn; label="unknown"}

    wrapperParser, parserRef

let jValue,jValueRef = createParserForwardedToRef<JValue>()

let jArray = 

    // set up the "primitive" parsers  
    let left = pchar '[' .>> spaces
    let right = pchar ']' .>> spaces
    let comma = pchar ',' .>> spaces
    let value = jValue .>> spaces   

    // set up the list parser
    let values = sepBy1 value comma

    // set up the main parser
    between left values right 
    |>> JArray
    <?> "array"
    
let jObject = 

    // set up the "primitive" parsers  
    let left = pchar '{' .>> spaces
    let right = pchar '}' .>> spaces
    let colon = pchar ':' .>> spaces
    let comma = pchar ',' .>> spaces
    let key = quotedString .>> spaces 
    let value = jValue .>> spaces

    // set up the list parser
    let keyValue = (key .>> colon) .>>. value
    let keyValues = sepBy1 keyValue comma

    // set up the main parser
    between left keyValues right 
    |>> Map.ofList  // convert the list of keyValues into a Map
    |>> JObject     // wrap in JObject  
    <?> "object"    // add label

jValueRef := choice 
    [
    jNull 
    jBool
    jNumber
    jString
    jArray
    jObject
    ]
