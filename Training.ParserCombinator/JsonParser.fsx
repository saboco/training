// Related blog post: http://fsharpforfunandprofit.com/posts/understanding-parser-combinators-3/

#load "ParserLibrary.fs"
#load "JsonParser.fs"

open System
open ParserLibrary.Core
open JsonParser


// JsonParser
run jNull "null"

run jNull "nulp" |> printResult  

run jBool "true"

run jBool "false" |> printResult

run jBool "truer" 
|> printResult

run jUnescapedChar "a"   // Success 'a'

run jUnescapedChar "\\" |> printResult

run jEscapedChar "\\\\" // Success '\'
run jEscapedChar "\\t"  // Success '\009'

run jEscapedChar "a" |> printResult

run jUnicodeChar "\\u263A"

run jString "\"\""    // Success ""
run jString "\"a\""   // Success "a"
run jString "\"ab\""  // Success "ab"
run jString "\"ab\\tde\""      // Success "ab\tde"
run jString "\"ab\\u263Ade\""  // Success "ab?de"

run jNumber "123"     // JNumber 123.0
run jNumber "-123"    // JNumber -123.0
run jNumber "123.4"   // JNumber 123.4

run jNumber "1234.4e-123"
run jNumber "2345.34e+3"

run jNumber "-123."   // JNumber -123.0 -- should fail!
run jNumber "00.1"    // JN

run jNumber_ "123"     // JNumber 123.0
run jNumber_ "-123"    // JNumber -123.0

run jNumber_ "-123." |> printResult

run jNumber_ "123.4"   // JNumber 123.4

run jNumber_ "00.4" |> printResult

run jArray "[ 1, 2 ]"
// Success (JArray [JNumber 1.0; JNumber 2.0],

run jArray "[ 1, 2, ]" |> printResult
// Line:0 Col:6 Error parsing array
// [ 1, 2, ]
// ^Unexpected ','

run jObject """{ "a":1, "b" : 2 }"""
// JObject (map [("a", JNumber 1.0); ("b", JNumber 2.0)]),

run jObject """{ "a":1, "b" : 2, }""" |> printResult

let example1 = """{ "name" : "Scott", "isMale" : true, "bday" : {"year":2001, "month":12, "day":25 }, "favouriteColors" : ["blue", "green"] }"""
run jValue example1

let example2= """{"widget": { "debug": "on", "window": { "title": "Sample Konfabulator Widget", "name": "main_window", "width": 500, "height": 500 }, "image": { "src": "Images/Sun.png", "name": "sun1", "hOffset": 250, "vOffset": 250, "alignment": "center" }, "text": { "data": "Click Here", "size": 36, "style": "bold", "name": "text1", "hOffset": 250, "vOffset": 100, "alignment": "center", "onMouseUp": "sun1.opacity = (sun1.opacity / 100) * 90;" } }} """
run jValue example2