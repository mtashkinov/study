module Main

open Shop

type AnytimeFullShop() = 
    interface IShop with
        member x.CanSell (good : Good) = true
        member x.Sell (goods:Good list) = goods

type TestCalendar (day : System.DayOfWeek) =
    interface Customer.ICalendar  with
        member x.DayOfWeek = day
 
let calendar day = new TestCalendar(day) :> Customer.ICalendar

let shouldDrunk (day : System.DayOfWeek) = 
    let customer = new Customer.Customer(calendar day)
    let allInclusiveShop = new AnytimeFullShop()
 
    customer.GoShopping allInclusiveShop
    customer.GetDrunk ()
    customer.IsDrunk

printfn "%A" (shouldDrunk System.DayOfWeek.Friday)