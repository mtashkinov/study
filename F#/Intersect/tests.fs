(* Tashkinov Mikhail (c) 2014
   Test for intersect
*)

module Tests

open Intersect
open NUnit.Framework

let eps = 0.0001

let compare x y = abs(x - y) < eps

let setCompare s1 s2 = 
    match s1 with
    | NoPoint -> s1 = s2
    | Point(a, b) -> match s2 with
                     | Point(c, d) -> (compare a c) && (compare b d) 
                     | _ -> false

    | Line (a, b) -> match s2 with
                     | Line(c, d) -> (compare a c) && (compare b d)
                     | _ -> false

    | VerticalLine x -> match s2 with
                        | VerticalLine y -> if compare x y then true
                                            else false
                        | _ -> false
    | LineSegment ((x1, y1), (x2, y2)) -> match s2 with
                                          | LineSegment ((x3, y3), (x4, y4)) -> ((compare x1 x3) && (compare x2 x4) && (compare y1 y3) && (compare y2 y4)) ||
                                                                                ((compare x1 x4) && (compare x2 x3) && (compare y1 y4) && (compare y2 y3))
                                          | _ -> false

[<TestFixture>]
type Tests() =
    [<Test>]
    member x.Test1() =
        Assert.AreEqual((setCompare (intersect (Point (5.12345678, 5.6789012)) (Point (5.12345678, 5.6789012))) (Point (5.12345678, 5.6789012))), true, "Совпадение точек")
    [<Test>]
    member x.Test2() =
        Assert.AreEqual((setCompare (intersect (Point (5.1, 5.6)) (Point (5.1, 5.7))) (NoPoint)), true, "Не совпадение точек")
    [<Test>]
    member x.Test3() =
        Assert.AreEqual((setCompare (intersect (Point (10.562435, 35.376152390645)) (Line (3.254567, 1.0))) (Point (10.562435, 35.376152390645))), true, "Точка лежит на прямой")
    [<Test>]
    member x.Test4() =
        Assert.AreEqual((setCompare (intersect (Point (10.562435, 35.376152390645)) (Line (3.254567, 0.0))) (NoPoint)), true, "Точка не лежит на прямой")
    [<Test>]
    member x.Test5() =
        Assert.AreEqual((setCompare (intersect (Point (10.562435, 35.376152390645)) (VerticalLine 10.562435)) (Point (10.562435, 35.376152390645))), true, "Точка лежит на вертикальной линии")
    [<Test>]
    member x.Test6() =
        Assert.AreEqual((setCompare (intersect (Point (10.562435, 35.376152390645)) (VerticalLine 10.552435)) (NoPoint)), true, "Точка не лежит на вертикальной линии")
    [<Test>]
    member x.Test7() =
        Assert.AreEqual((setCompare (intersect (Point (10.562435, 35.376152390645)) (LineSegment ((1.0, 4.254567), (11.0, 36.800237)))) (Point (10.562435, 35.376152390645))), true, "Точка лежит на отрезке")
    [<Test>]
    member x.Test8() =
        Assert.AreEqual((setCompare (intersect (Point (11.0, 36.800237)) (LineSegment ((1.0, 4.254567), (11.0, 36.800237)))) (Point (11.0, 36.800237))), true, "Точка является одним из концов отрезка")
    [<Test>]
    member x.Test9() =
        Assert.AreEqual((setCompare (intersect (Point (0.0, 1.0)) (LineSegment ((1.0, 4.254567), (11.0, 36.800237)))) (NoPoint)), true, "Точка лежит на прямой на которой лежит отрезок, но не принадлежит ему")
    [<Test>]
    member x.Test10() =
        Assert.AreEqual((setCompare (intersect (Point (11.0, 36.810237)) (LineSegment ((1.0, 4.254567), (11.0, 36.800237)))) (NoPoint)), true, "Точка не принадлежит отрезку")
    [<Test>]
    member x.Test11() =
        Assert.AreEqual((setCompare (intersect (Line (3.254567, 1.0)) (Point (10.562435, 35.376152390645))) (Point(10.562435, 35.376152390645))), true, "Точка лежит на линии")
    [<Test>]
    member x.Test12() =
        Assert.AreEqual((setCompare (intersect (Line (3.254567, 0.0)) (Point (10.562435, 35.376152390645))) (NoPoint)), true, "Точка не лежит на линии")
    [<Test>]
    member x.Test13() =
        Assert.AreEqual((setCompare (intersect (Line (3.254567, 1.234567)) (Line (3.254567, 1.234567))) (Line (3.254567, 1.234567))), true, "Совпадение линий")
    [<Test>]
    member x.Test14() =
        Assert.AreEqual((setCompare (intersect (Line (3.254567, 1.234567)) (Line (-2.254567, 1.0))) (Point (-0.0425778, 1.095994502))), true, "Пересечение линий")
    [<Test>]
    member x.Test15() =
        Assert.AreEqual((setCompare (intersect (Line (3.254567, 1.234567)) (Line (3.254567, 1.0))) (NoPoint)), true, "Линии параллельны")
    [<Test>]
    member x.Test16() =
        Assert.AreEqual((setCompare (intersect (Line (3.254567, 1.234567)) (VerticalLine 3.0)) (Point (3.0, 10.998268))), true, "Пересечение линии с вертикальной линией")
    [<Test>]
    member x.Test17() =
        Assert.AreEqual((setCompare (intersect (Line (3.254567, 1.234567)) (LineSegment ((1.0, 4.489134), (5.0, 17.507402)))) (LineSegment ((1.0, 4.489134), (5.0, 17.507402)))), true, "Отрезок принадлежит прямой")
    [<Test>]
    member x.Test18() =
        Assert.AreEqual((setCompare (intersect (Line (3.254567, 1.234567)) (LineSegment ((0.0, 1.0), (-1.0, 3.254567)))) (Point(-0.0425778, 1.095994502))), true, "Пересечение прямой с отрезком")
    [<Test>]
    member x.Test19() =
        Assert.AreEqual((setCompare (intersect (Line (3.254567, 1.234567)) (LineSegment ((1.0, 4.489134), (6.0, 17.507402)))) (Point (1.0, 4.489134))), true, "Конец отрезка принадлежит прямой")
    [<Test>]
    member x.Test20() =
        Assert.AreEqual((setCompare (intersect (Line (3.254567, 0.234567)) (LineSegment ((1.0, 4.489134), (5.0, 17.507402)))) (NoPoint)), true, "Линия параллельна отрезку")
    [<Test>]
    member x.Test21() =
        Assert.AreEqual((setCompare (intersect (Line (3.254567, 1.234567)) (LineSegment ((1.0, 1.0), (11.0, 36.800237)))) (NoPoint)), true, "Линия не пересекает отрезок")
    [<Test>]
    member x.Test22() =
        Assert.AreEqual((setCompare (intersect (VerticalLine 10.562435) (Point (10.562435, 35.376152390645))) (Point (10.562435, 35.376152390645))), true, "Точка лежит на вертикальной линии")
    [<Test>]
    member x.Test23() =
        Assert.AreEqual((setCompare (intersect (VerticalLine 10.552435) (Point (10.562435, 35.376152390645))) (NoPoint)), true, "Точка не лежит на вертикальной линии")
    [<Test>]
    member x.Test24() =
        Assert.AreEqual((setCompare (intersect (VerticalLine 3.0) (Line (3.254567, 1.234567))) (Point (3.0, 10.998268))), true, "Пересечение вертикальной линии с прямой")
    [<Test>]
    member x.Test25() =
        Assert.AreEqual((setCompare (intersect (VerticalLine 5.54321012) (VerticalLine 5.54321012)) (VerticalLine 5.54321012)), true, "Совпадение вертикальных линий")
    [<Test>]
    member x.Test26() =
        Assert.AreEqual((setCompare (intersect (VerticalLine 5.54321012) (VerticalLine 3.54321012)) (NoPoint)), true, "Вертикальные линии не совпадают")
    [<Test>]
    member x.Test27() =
        Assert.AreEqual((setCompare (intersect (VerticalLine 3.0) (LineSegment ((1.0, 4.489134), (5.0, 17.507402)))) (Point (3.0, 10.998268))), true, "Пересечение вертикальной линии и отрезка")
    [<Test>]
    member x.Test28() =
        Assert.AreEqual((setCompare (intersect (VerticalLine 5.0) (LineSegment ((1.0, 4.489134), (5.0, 17.507402)))) (Point (5.0, 17.507402))), true, "Вертикальная линия проходит через один из концов отрезка")
    [<Test>]
    member x.Test29() =
        Assert.AreEqual((setCompare (intersect (VerticalLine 5.7893) (LineSegment ((5.7893, 4.489134), (5.7893, 17.507402)))) (LineSegment ((5.7893, 4.489134), (5.7893, 17.507402)))), true, "Отрезок лежит на вертикальной линии")
    [<Test>]
    member x.Test30() =
        Assert.AreEqual((setCompare (intersect (VerticalLine 6.0) (LineSegment ((1.0, 4.489134), (5.0, 17.507402)))) (NoPoint)), true, "Вертикальная линия не пересекает отрезок")
    [<Test>]
    member x.Test31() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((1.0, 4.254567), (11.0, 36.800237))) (Point (10.562435, 35.376152390645))) (Point (10.562435, 35.376152390645))), true, "Точка лежит на отрезке")
    [<Test>]
    member x.Test32() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((1.0, 4.254567), (11.0, 36.800237))) (Point (11.0, 36.800237))) (Point (11.0, 36.800237))), true, "Точка является одним из концов отрезка")
    [<Test>]
    member x.Test33() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((1.0, 4.254567), (11.0, 36.800237))) (Point (0.0, 1.0))) (NoPoint)), true, "Точка лежит на продолжении отрезка")
    [<Test>]
    member x.Test34() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((1.0, 4.254567), (11.0, 36.800237))) (Point (11.0, 36.810237))) (NoPoint)), true, "Точка не лежит на отрезке")
    [<Test>]
    member x.Test35() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((1.0, 4.489134), (5.0, 17.507402))) (Line (3.254567, 1.234567))) (LineSegment ((1.0, 4.489134), (5.0, 17.507402)))), true, "Отрезок лежит на прямой")
    [<Test>]
    member x.Test36() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((0.0, 1.0), (-1.0, 3.254567))) (Line (3.254567, 1.234567))) (Point(-0.0425778, 1.095994502))), true, "Отрезок пересекается прямой")
    [<Test>]
    member x.Test37() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((1.0, 4.489134), (6.0, 17.507402))) (Line (3.254567, 1.234567))) (Point (1.0, 4.489134))), true, "Отрезок пересекается прямой")
    [<Test>]
    member x.Test38() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((1.0, 4.489134), (5.0, 17.507402))) (Line (3.254567, 0.234567))) (NoPoint)), true, "Отрезок параллелен прямой")
    [<Test>]
    member x.Test39() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((1.0, 1.0), (11.0, 36.800237))) (Line (3.254567, 1.234567))) (NoPoint)), true, "Отрезок не пересекается прямой")
    [<Test>]
    member x.Test40() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((1.0, 4.489134), (5.0, 17.507402))) (VerticalLine 3.0)) (Point (3.0, 10.998268))), true, "Отрезок пересекается вертикальной линией")
    [<Test>]
    member x.Test41() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((1.0, 4.489134), (5.0, 17.507402))) (VerticalLine 5.0)) (Point (5.0, 17.507402))), true, "Конец отрезка принадлежит вертикальной линии")
    [<Test>]
    member x.Test42() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((5.7893, 4.489134), (5.7893, 17.507402))) (VerticalLine 5.7893)) (LineSegment ((5.7893, 4.489134), (5.7893, 17.507402)))), true, "Отрезок лежит на вертикальной линии")
    [<Test>]
    member x.Test43() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((1.0, 4.489134), (5.0, 17.507402))) (VerticalLine 6.0)) (NoPoint)), true, "Отрезок не пересекает вертикальную линию")
    [<Test>]
    member x.Test44() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((1.0, 4.489134), (5.0, 17.507402))) (LineSegment ((5.0, 17.507402), (1.0, 4.489134)))) (LineSegment ((1.0, 4.489134), (5.0, 17.507402)))), true, "Отрезки совпадают")
    [<Test>]
    member x.Test45() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((1.0, 4.489134), (5.0, 17.507402))) (LineSegment ((2.0, 7.743701), (3.0, 10.998268)))) (LineSegment ((2.0, 7.743701), (3.0, 10.998268)))), true, "Второй отрезок вложен в первый")
    [<Test>]
    member x.Test46() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((2.0, 7.743701), (3.0, 10.998268))) (LineSegment ((1.0, 4.489134), (5.0, 17.507402)))) (LineSegment ((2.0, 7.743701), (3.0, 10.998268)))), true, "Первый отрезок вложен во второй")
    [<Test>]
    member x.Test47() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((1.0, 4.489134), (3.0, 10.998268))) (LineSegment ((2.0, 7.743701), (5.0, 17.507402)))) (LineSegment ((2.0, 7.743701), (3.0, 10.998268)))), true, "Отрезки лежат на одной прямой и пересекаются")
    [<Test>]
    member x.Test48() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((2.0, 7.743701), (5.0, 17.507402))) (LineSegment ((1.0, 4.489134), (3.0, 10.998268)))) (LineSegment ((2.0, 7.743701), (3.0, 10.998268)))), true, "Отрезки лежат на одной прямой и пересекаются")
    [<Test>]
    member x.Test49() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((2.0, 7.743701), (2.0, 17.507402))) (LineSegment ((2.0, 4.489134), (2.0, 10.998268)))) (LineSegment ((2.0, 7.743701), (2.0, 10.998268)))), true, "Отрезки лежат на одной вертикальной прямой и пересекаются")
    [<Test>]
    member x.Test50() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((3.0, 10.998268), (5.0, 17.507402))) (LineSegment ((1.0, 4.489134), (2.0, 7.743701)))) (NoPoint)), true, "Отрезки лежат на одной прямой и не пересекаются")
    [<Test>]
    member x.Test51() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((2.0, 7.743701), (5.0, 17.507402))) (LineSegment ((2.0, 7.743701), (5.0, 10.998268)))) (Point (2.0, 7.743701))), true, "Отрезки лежат на одной прямой и имеют общую точку")
    [<Test>]
    member x.Test52() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((-0.0425778, 1.095994502), (5.0, 17.507402))) (LineSegment ((0.0, 1.0), (-0.0425778, 1.095994502)))) (Point(-0.0425778, 1.095994502))), true, "Отрезки имеют общий конец")
    [<Test>]
    member x.Test53() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((-1.0, -2.02), (5.0, 17.507402))) (LineSegment ((0.0, 1.0), (-1.0, 3.254567)))) (Point(-0.0425778, 1.095994502))), true, "Пересечение отрезков в одной точке")
    [<Test>]
    member x.Test54() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((2.0, 7.743701), (5.0, 17.507402))) (LineSegment ((0.0, 1.0), (-1.0, 3.254567)))) (NoPoint)), true, "Отрезки не пересекаются")
    [<Test>]
    member x.Test55() =
        Assert.AreEqual((setCompare (intersect (LineSegment ((1.0, 5.489134), (5.0, 18.507402))) (LineSegment ((5.0, 17.507402), (1.0, 4.489134)))) (NoPoint)), true, "Отрезки параллельны")
