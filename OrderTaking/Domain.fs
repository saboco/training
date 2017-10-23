namespace OrderTaking.Domain

open System
open OrderTaking.Common
open OrderTaking.AppServices

// ==================================
// Primitives
// ==================================

type OrderId = Undefined
type ProductCode = Undefined
type Price = Undefined


// ==================================
// Order states
// ==================================
    
// unvalidated state    
type UnvalidatedOrder = OrderFormDto
    
// validated state        
type ValidatedOrderLine =  DotDotDot
type ValidatedOrder = {
    OrderId : OrderId
    CustomerInfo : CustomerInfo
    ShippingAddress : Address
    BillingAddress : Address
    OrderLines : ValidatedOrderLine list
    }
and CustomerInfo = DotDotDot
and Address = DotDotDot

// priced state            
type PricedOrderLine = DotDotDot
type PricedOrder = DotDotDot

// all states combined
type Order =
    | Unvalidated of UnvalidatedOrder
    | Validated of ValidatedOrder
    | Priced of PricedOrder
    // etc
//


