namespace OrderTaking.AppServices

open System
open OrderTaking.Common

// ======================================================
// Input Dtos
// ======================================================

type CustomerInfoDto = {
    Name : string
    Email : string
    }

type AddressDto = Undefined

type OrderFormDto = {
    OrderId : string
    CustomerInfo : CustomerInfoDto // DTO subtype 
    ShippingAddress : AddressDto   // DTO subtype 
    // etc
    IsQuoteChecked : bool  // is the box checked on the form?
    }

// ======================================================
// Commands
// ======================================================
   
type PlaceOrder = Command<OrderFormDto>
type ChangeOrder = Undefined
type CancelOrder = Undefined

type OrderTakingCommand = 
    | PlaceOrder of PlaceOrder
    | ChangeOrder of ChangeOrder 
    | CancelOrder of CancelOrder


// ======================================================
// API
// ======================================================

type PlaceOrderWorkflow = 
    PlaceOrder    // input command
     -> AsyncResult<PlacedOrderAcknowledgment,PlaceOrderError> // output

/// Success output of PlaceOrder process
and PlacedOrderAcknowledgment = Undefined
   
/// Failure output of PlaceOrder process
and PlaceOrderError = Undefined
