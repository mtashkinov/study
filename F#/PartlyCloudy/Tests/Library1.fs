module Tests

open NUnit.Framework
open NSubstitute
open FsCheck
open Main

let test (isStorkCall : bool, expectedCreature : CreatureType) (isShining : bool, speed : int, dayLight : DayLightType) =
    let luminary = Substitute.For<ILuminary>()
    let wind = Substitute.For<IWind>()
    let dayLightMock = Substitute.For<IDayLight>()
    let magic = Substitute.For<IMagic>()
    let courier = Substitute.For<ICourier>()

    ignore (luminary.isShining.Returns(isShining))
    ignore (wind.speed.Returns(speed))
    ignore (dayLightMock.dayLight.Returns(dayLight))
    ignore (magic.callDaemon().Returns(courier))
    ignore (magic.callStork().Returns(courier))

    let cloud = new Cloud(dayLightMock, luminary, wind, magic)
    cloud.Create() |> ignore

    if isStorkCall then ignore (magic.Received().callStork())
    else ignore (magic.Received().callDaemon())

    courier
        .Received()
        .giveBaby(Arg.Is<Creature>(fun (x : Creature) -> x.getType() = expectedCreature))

[<Test>]
let Test1() =
    test(true, CreatureType.Kitten)  (true, 0, Noon)

type Generator2 = 
    static member int() = Arb.fromGen <| Gen.choose(3, 5)
    static member bool() = Arb.fromGen <| Gen.constant true
    static member DayLightType() = Arb.fromGen <| Gen.constant Morning

[<Test>]
let Test2() =
    Arb.register<Generator2>() |> ignore
    let myTest = test (true, CreatureType.Puppy)
    FsCheck.Check.Quick myTest

[<Test>]
let Test3() =
    test(false, CreatureType.Balloon) (true, 0, Afternoon)

type Generator4 = 
    static member int() = Arb.fromGen <| Gen.choose(0, 10)
    static member bool() = Arb.fromGen <| Gen.constant true
    static member DayLightType() = Arb.fromGen <| Gen.constant Night

[<Test>]
let Test4() =
    Arb.register<Generator4>() |> ignore
    let myTest = test (false, CreatureType.Hedgehog)
    FsCheck.Check.Quick myTest

type Generator5 = 
    static member int() = Arb.fromGen <| Gen.choose(0, 10)
    static member bool() = Arb.fromGen <| Gen.constant false
    static member DayLightType() = Arb.fromGen <| Gen.constant Night

[<Test>]
let Test5() =
    Arb.register<Generator5>() |> ignore
    let myTest = test (false, CreatureType.Bat)
    FsCheck.Check.Quick myTest

type Generator6 = 
    static member int() = Arb.fromGen <| Gen.choose(0, 10)
    static member bool() = Arb.fromGen <| Gen.constant false
    static member DayLightType() = Arb.fromGen <| Gen.constant Noon

[<Test>]
let Test6() =
    Arb.register<Generator6>() |> ignore
    let myTest = test (true, CreatureType.Piglet)
    FsCheck.Check.Quick myTest

[<Test>]
let Test7() =
    test(true, CreatureType.Bearcub) (false, 10, Morning)

// Тесты на последнюю строчку

let myLastTest = test (false, CreatureType.Piglet)

type Generator8 = 
    static member int() = Arb.fromGen <| Gen.choose(1, 10)
    static member bool() = Arb.fromGen <| Gen.constant true
    static member DayLightType() = Arb.fromGen <| Gen.constant Noon

[<Test>]
let Test8() =
    Arb.register<Generator8>() |> ignore
    FsCheck.Check.Quick myLastTest

type Generator9 = 
    static member int() = Arb.fromGen <| Gen.choose(1, 2)
    static member bool() = Arb.fromGen <| Gen.constant true
    static member DayLightType() = Arb.fromGen <| Gen.constant Morning

[<Test>]
let Test9() = 
    Arb.register<Generator9>() |> ignore
    FsCheck.Check.Quick myLastTest

type Generator10 = 
    static member int() = Arb.fromGen <| Gen.choose(6, 10)
    static member bool() = Arb.fromGen <| Gen.constant true
    static member DayLightType() = Arb.fromGen <| Gen.constant Morning

[<Test>]
let Test10() = 
    Arb.register<Generator10>() |> ignore
    FsCheck.Check.Quick myLastTest

type Generator11 = 
    static member int() = Arb.fromGen <| Gen.choose(1, 10)
    static member bool() = Arb.fromGen <| Gen.constant true
    static member DayLightType() = Arb.fromGen <| Gen.constant Afternoon

[<Test>]
let Test11() = 
    Arb.register<Generator11>() |> ignore
    FsCheck.Check.Quick myLastTest

type Generator12 = 
    static member int() = Arb.fromGen <| Gen.choose(0, 9)
    static member bool() = Arb.fromGen <| Gen.constant false
    static member DayLightType() = Arb.fromGen <| Gen.constant Morning

[<Test>]
let Test12() = 
    Arb.register<Generator12>() |> ignore
    FsCheck.Check.Quick myLastTest

type Generator13 = 
    static member int() = Arb.fromGen <| Gen.choose(0, 10)
    static member bool() = Arb.fromGen <| Gen.constant false
    static member DayLightType() = Arb.fromGen <| Gen.constant Afternoon

[<Test>]
let Test13() = 
    Arb.register<Generator13>() |> ignore
    FsCheck.Check.Quick myLastTest