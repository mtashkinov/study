(* Tashkinov Mikhail (c) 2014
   Tests for email check
*)

module RegExprTest

open RegExpr
open NUnit.Framework

[<TestFixture>]
type Tests() =
    [<Test>]
    member x.Test1() =
        Assert.AreEqual(isEmail "abcd@yandex.ru", true, "Обычный email")
    [<Test>]
    member x.Test2() =
        Assert.AreEqual(isEmail ".abcd@yandex.ru", false, "email не может начинаться с точки")
    [<Test>]
    member x.Test3() =
        Assert.AreEqual(isEmail "_abcd@yandex.ru", true, "email не может начинаться с подчёркивания")
    [<Test>]
    member x.Test4() =
        Assert.AreEqual(isEmail "abc@yandex.ru", false, "Левая часть email не может быть короче 4х символов")
    [<Test>]
    member x.Test5() =
        Assert.AreEqual(isEmail "abc0@yandex.ru", true, "Левая часть email может оканчиваться на цифру ")
    [<Test>]
    member x.Test6() =
        Assert.AreEqual(isEmail "abcdefghijklmnopqrstuvwxyz1234567890@yandex.ru", false, "Левая часть email не может быть слишком длинной(длиннее 32х символов)")
    [<Test>]
    member x.Test7() =
        Assert.AreEqual(isEmail "abcdyandex.ru", false, "email должен содержать @")
    [<Test>]
    member x.Test8() =
        Assert.AreEqual(isEmail "abcd@yandex.spb.petergof.ru", true, "email может содержать более 1 точки после @")
    [<Test>]
    member x.Test9() =
        Assert.AreEqual(isEmail "abcd@yandex.spb.petergof.russia", false, "После последней точкидолжно быть от 2х до 4х символов")
    [<Test>]
    member x.Test10() =
        Assert.AreEqual(isEmail "ab-cd@yandex.spb.petergof.ru", true, "email может содержать -")
    [<Test>]
    member x.Test11() =
        Assert.AreEqual(isEmail "a..b@yandex.spb.petergof.ru", false, "Между точками должно что-то быть")
    [<Test>]
    member x.Test12() =
        Assert.AreEqual(isEmail "paints_department@hermitage.museum", true, "Бывают валидные имена")



