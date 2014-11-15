module ShoppingTests.Tests 
 
open NUnit.Framework
open NSubstitute
open Shop

 
type EmptyShop() = 
    interface IShop with
        member x.CanSell (good : Good) = false
        member x.Sell (goods:Good list) = []
 
type AnytimeFullShop() = 
    interface IShop with
        member x.CanSell (good : Good) = true
        member x.Sell (goods:Good list) = goods
 
type TestCalendar (day : System.DayOfWeek) =
    interface Customer.ICalendar  with
        member x.DayOfWeek = day
 
let calendar day = new TestCalendar(day) :> Customer.ICalendar
let calendarFriday = calendar System.DayOfWeek.Friday 
   
[<Test>]
let test1 () = 
    let customer = new Customer.Customer(calendarFriday)
    Assert.AreEqual(customer.IsDrunk, false)
 
[<Test>]
let test2 () = 
    let customer = new Customer.Customer(calendarFriday)
    let emptyShop = new EmptyShop()
 
    customer.GoShopping emptyShop
    customer.GetDrunk ()
    Assert.AreEqual(customer.IsDrunk, false)
 
[<Test>]
let test3 () = 
    let customer = new Customer.Customer(calendarFriday)
    let allInclusiveShop = new AnytimeFullShop()
 
    customer.GoShopping allInclusiveShop
    customer.GetDrunk ()
    Assert.AreEqual(customer.IsDrunk, true)
 
[<Test>]
let ``test not drunk on Thursday`` () = 
    let customer = new Customer.Customer(calendar System.DayOfWeek.Thursday)
    let allInclusiveShop = new AnytimeFullShop()
 
    customer.GoShopping allInclusiveShop
    customer.GetDrunk ()
    Assert.AreEqual(customer.IsDrunk, false)
 
let shouldDrunk (day : System.DayOfWeek) = 
    let customer = new Customer.Customer(calendar day)
    let allInclusiveShop = new AnytimeFullShop()
 
    customer.GoShopping allInclusiveShop
    customer.GetDrunk ()
    if day = System.DayOfWeek.Friday then customer.IsDrunk
    else not customer.IsDrunk
 
[<Test>]
let testQuick() = 
    FsCheck.Check.Quick shouldDrunk

[<Test>]
let testNSubstitute() = 
    let day = System.DayOfWeek.Friday // Не смог разобраться как генерировать свой случайный элемент типа
    let customer = new Customer.Customer(calendar day)
    let shop = Substitute.For<IShop>()

    customer.GoShopping shop
    Received.InOrder(fun () ->
        ignore (shop.CanSell(Arg.Any<Good>()))
        ignore (shop.Sell(Arg.Any<list<Good>>())))

[<Test>]
let testNSubstitute2() =
    let day = System.DayOfWeek.Friday
    let customer = new Customer.Customer(calendar day)
    let shop = Substitute.For<IShop>()
    let list = new System.Collections.Generic.List<Good>()
    let isContains = ref true

    ignore (shop
                .CanSell(Arg.Any<Good>()).Returns(false)
                .AndDoes(fun x -> list.Add(x.[0] :?> Good)))

    ignore (shop
                .Sell(Arg.Any<List<Good>>()).Returns([])
                .AndDoes(fun x -> isContains := !isContains && List.fold (fun a b -> not (list.Contains(b)) && a) true (x.[0] :?> list<Good>)))

    customer.GoShopping shop
    Assert.AreEqual(!isContains, true)
    


