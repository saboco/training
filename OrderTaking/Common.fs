module OrderTaking.Common

open System

type Undefined = exn  

type Command<'data> = {
    Data : 'data
    Timestamp: DateTime
    UserId: string
    // etc
    }

type AsyncResult<'success, 'failure> = Async<Result<'success, 'failure>>


