namespace Training.Calculator

open Rop
open System

module Model =   
  
  type Command = Command of (string * string * string)

  type Left = Left of int
  type Right = Right of int
  type Operator =
  | Sum
  | Subtrac
  | Multiply
  | Divide  

  type Operation = Operation of (Left * Operator * Right)
  
  type ParseOperator = string -> Result<Operator, string>  
  type ParseCommand = string -> Result<Command, string>  
  type ParseOperand = string -> Result<int, string>
  type ParseOperation = Command -> Result<Operation, string list> 
  type Operated<'T> = Operated of 'T

  let validateString s =
    not (String.IsNullOrWhiteSpace(s))
  
  let parseCommand = 
    let validateParts : ParseCommand = 
      fun c -> 
        let p = c.Split(' ') 
        if p.Length = 3 &&  validateString p.[0] &&  validateString p.[1] && validateString p.[2] then
          Success (Command (p.[0], p.[1], p.[2]))
        else 
          Failure ["command should be composed by 3 parts, the left operand the operator and the right operant and should be separated by 1 space, ex: 1 + 2"]
    validateParts

  let parseOperand : ParseOperand=
    let f str =
      match System.Int32.TryParse(str) with
      | (true,int) -> Success int
      | _ -> Failure ["not an integer"]      
    f      

  let parseOperator : ParseOperator =
    let f s = 
      match s with
      | "+" -> Success Sum
      | "-" -> Success Subtrac
      | "*" -> Success Multiply
      | "/" -> Success Divide
      | _ -> Failure ["Unknown operator"]
    f    

  let createLeftOperand s =
    s 
    |> parseOperand 
    |> map Left

  let createRightOperand s =
    s
    |> parseOperand
    |> map Right 

  let addLeftWithOperator l o =
    match l, o with
    | Success left, Success op -> Success (left, op)    
    | Success _, Failure err -> Failure err
    | Failure err, Success _ -> Failure err
    | Failure err1, Failure err2 -> Failure (List.append err1 err2)

  let addRigtToLeftAndOperator lo r =
    match lo, r with
    | Success (l, o), Success right -> Success (l, o, right)    
    | Success _, Failure err -> Failure err
    | Failure err, Success _ -> Failure err
    | Failure err1, Failure err2 -> Failure (List.append err1 err2)

  let parseOperation c =    
    let (Command (l, o, r)) = c
    let left = createLeftOperand l
    let operator = parseOperator o
    let right = createRightOperand r
    let lo = addLeftWithOperator left operator
    addRigtToLeftAndOperator lo right
    |> map Operation
  
  let apply = fun fOpt xOpt -> 
    match fOpt, xOpt with
    | Success f, Success x ->  Success (f x)
    | Success _, Failure err ->  Failure err
    | Failure err, Success _ -> Failure err
    | Failure err1, Failure err2 -> Failure (List.append err1 err2)
    
  let traverseResultA f list =
    let (<*>) = apply
    let retn = succeed
    
    let cons head tail = head :: tail
    
    let initState = retn []
    let folder head tail = 
        retn cons <*> (f head) <*> tail

    List.foldBack folder list initState 

  let sequence x = traverseResultA id x

  let createOperations cmds =
    
    let f = parseOperation
    let retn = succeed
    let (<*>) = apply
    let cons head tail = head::tail

    let initState = retn []
    let folder head tail = 
      retn cons <*> (f head) <*> tail
      
    List.foldBack folder cmds initState  

  let execOperation o  =
    let (Operation(Left l, op, Right r)) = o

    match op with
    | Sum -> Some (l + r)
    | Subtrac -> Some (l - r)
    | Multiply -> Some (l * r)
    | Divide -> 
        if r <> 0 then
            Some (l / r)
        else None
  
  let treatCommands cmds =
    cmds
    |> List.map parseCommand
    |> sequence
    |> bind createOperations
    //|> map execOperation
    //|> sequence
    //|> map execOperation