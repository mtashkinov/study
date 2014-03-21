(* Tashinov Mikhail (c) 2014
   Intersect of sets
*)

let eps = 0.0001

let compare x y = abs(x - y) < eps

let compareOrGr x y = (x > y) || (compare x y)

type Set =
    | NoPoint
    | Point of float * float
    | Line of float * float
    | VerticalLine of float
    | LineSegment of (float * float) * (float * float)

let pointIntersectPoint x1 y1 x2 y2 =
    if compare x1 x2 && compare y1 y2 then Point (x1, y1)
    else NoPoint

let pointIntersectLine x y a b =
    if compare y (a * x + b) then Point (x, y)
    else NoPoint

let pointIntersectVertLine x y a =
    if compare x a then Point (x, y)
    else NoPoint

let isInLineSegment x y x1 y1 x2 y2 =
    (compare ((y - y1) * (x2 - x1))  ((x - x1) * (y2 - y1))) &&
    (((compareOrGr x x1) && (compareOrGr x2 x))  || ((compareOrGr x1 x) && (compareOrGr x x2)))  && (((compareOrGr y y1) && (compareOrGr y2 y))  || ((compareOrGr y1 y) && (compareOrGr y y2)))

let pointIntersectLineSegment x y x1 y1 x2 y2 =
    if isInLineSegment x y x1 y1 x2 y2 then Point (x, y)
    else NoPoint

let lineIntersectLine a1 b1 a2 b2 =
    if compare a1 a2 then
        if compare b1 b2 then Line (a1, b1)
        else NoPoint
    else
        let x = (b2 - b1) / (a1 - a2) in
            Point (x, a1 * x + b1)

let lineIntersectVertLine a b x = Point (x, a * x + b)
   
let lineIntersectLineSegment a b x1 y1 x2 y2 =
    if (compare (a * x1 + b) y1) && (compare (a * x2 + b) y2) then LineSegment ((x1, y1), (x2, y2))
    else if ((a * x1 + b <= y1) && (a * x2 + b >= y2)) || ((a * x1 + b >= y1) && (a * x2 + b <= y2)) then
        let x = ((b - y1) * (x2 - x1) + x1 * (y2 - y1)) / (y2 - y1 - a* x2 + a * x1) in
            Point (x, a * x + b)
        else NoPoint

let vertLineIntersectVertLine x1 x2 =
    if compare x1 x2 then VerticalLine x1
    else NoPoint

let vertLineIntersectLineSegment x x1 y1 x2 y2 =
    if ((x >= x1) && (x <= x2)) || ((x <= x1) && (x >= x2)) then
        let y = (x - x1) * (y2 - y1) / (x2 - x1) + y1
        Point (x, y)
    else NoPoint

let segmentEquation f x y x1 y1 x2 y2 = f ((y - y1) * (x2 - x1)) ((x - x1) * (y2 - y1))

let lineSegmentIntersectLineSegment x1 y1 x2 y2 x3 y3 x4 y4 =
    if (segmentEquation (compare) x3 y3 x1 y1 x2 y2) && (segmentEquation (compare) x4 y4 x1 y1 x2 y2) &&
       ((isInLineSegment x3 y3 x1 y1 x2 y2) || (isInLineSegment x4 y4 x1 y1 x2 y2) || (isInLineSegment x1 y1 x3 y3 x4 y4)) then
        if (isInLineSegment x1 y1 x3 y3 x4 y4) && (isInLineSegment x2 y2 x3 y3 x4 y4) then LineSegment ((x1, y1), (x2, y2))
        else if (isInLineSegment x3 y3 x1 y1 x2 y2) && (isInLineSegment x4 y4 x1 y1 x2 y2) then LineSegment ((x3, y3), (x4, y4))
             else if isInLineSegment x3 y3 x1 y1 x2 y2 then
                      if isInLineSegment x1 y1 x3 y3 x4 y4 then LineSegment ((x1, y1), (x3, y3))
                      else LineSegment ((x2, y2), (x3, y3))
                  else if isInLineSegment x1 y1 x3 y3 x4 y4 then LineSegment ((x1, y1), (x4, y4))
                           else LineSegment ((x2, y2), (x4, y4))
    else let a = (y2 - y1) * (x4 - x3)
         let b = y1 * (x2 - x1) * (x4 - x3)
         let c = (y4 - y3) * (x2 - x1)
         let d = y3 * (x4 - x3) * (x2 - x1)
         let x = (a * x1 - c * x3 + d - b) / (a - c)
         let y = ((x - x1) * (y2 - y1) + y1 * (x2 - x1)) / (x2 - x1)
         if (isInLineSegment x y x1 y1 x2 y2) && (isInLineSegment x y x3 y3 x4 y4) then Point (x, y)
         else NoPoint
        


let pointIntersect x y s =
    match s with
    | NoPoint -> NoPoint
    | Point (x1, y1) -> pointIntersectPoint x y x1 y1
    | Line (a, b) -> pointIntersectLine x y a b
    | VerticalLine a -> pointIntersectVertLine x y a
    | LineSegment ((x1, y1), (x2, y2)) -> pointIntersectLineSegment x y x1 y1 x2 y2

let lineIntersect a b s =
    match s with
    | NoPoint -> NoPoint
    | Point (x, y) -> pointIntersectLine x y a b
    | Line (a1, b1) -> lineIntersectLine a b a1 b1
    | VerticalLine x -> lineIntersectVertLine a b x
    | LineSegment ((x1, y1), (x2, y2)) -> lineIntersectLineSegment a b x1 y1 x2 y2

let vertLineIntersect z s =
    match s with
    | NoPoint -> NoPoint
    | Point (x, y) -> pointIntersectVertLine x y z
    | Line (a, b) -> lineIntersectVertLine a b z
    | VerticalLine x -> vertLineIntersectVertLine z x
    | LineSegment ((x1, y1), (x2, y2)) -> vertLineIntersectLineSegment z x1 y1 x2 y2

let lineSegmentIntersect x1 y1 x2 y2 s =
    match s with
    | NoPoint -> NoPoint
    | Point (x, y) -> pointIntersectLineSegment x y x1 y1 x2 y2
    | Line (a, b) -> lineIntersectLineSegment a b x1 y1 x2 y2
    | VerticalLine x -> vertLineIntersectLineSegment x x1 y1 x2 y2
    | LineSegment ((x3, y3), (x4, y4)) -> lineSegmentIntersectLineSegment x1 y1 x2 y2 x3 y3 x4 y4

let intersect s1 s2 =
    match s1 with
    | NoPoint -> NoPoint
    | Point (x, y) -> pointIntersect x y s2
    | Line (a, b) -> lineIntersect a b s2
    | VerticalLine x -> vertLineIntersect x s2
    | LineSegment ((x1, y1), (x2, y2)) -> lineSegmentIntersect x1 y1 x2 y2 s2

let tests = 
    [
     Point (5.12345678, 5.6789012), Point (5.12345678, 5.6789012), Point (5.12345678, 5.6789012);
     Point (5.1, 5.6), Point(5.1, 5.7), (NoPoint);
     Point (10.562435, 35.376152390645), Line (3.254567, 1.0), Point (10.562435, 35.376152390645);
     Point (10.562435, 35.376152390645), Line (3.254567, 0.0), NoPoint;
     Point (10.562435, 35.376152390645), VerticalLine 10.562435, Point (10.562435, 35.376152390645);
     Point (10.562435, 35.376152390645), VerticalLine 10.552435, NoPoint;
     Point (10.562435, 35.376152390645), LineSegment ((1.0, 4.254567), (11.0, 36.800237)), Point (10.562435, 35.376152390645);
     Point (11.0, 36.800237), LineSegment ((1.0, 4.254567), (11.0, 36.800237)), Point (11.0, 36.800237);
     Point (0.0, 1.0), LineSegment ((1.0, 4.254567), (11.0, 36.800237)), NoPoint;
     Point (11.0, 36.810237), LineSegment ((1.0, 4.254567), (11.0, 36.800237)), NoPoint;
     Line (3.254567, 1.0), Point (10.562435, 35.376152390645), Point(10.562435, 35.376152390645);
     Line (3.254567, 0.0), Point (10.562435, 35.376152390645), NoPoint;
     Line (3.254567, 1.234567), Line (3.254567, 1.234567), Line (3.254567, 1.234567);
     Line (3.254567, 1.234567), Line (-2.254567, 1.0), Point(-0.0425778, 1.095994502);
     Line (3.254567, 1.234567), Line (3.254567, 1.0), NoPoint;
     Line (3.254567, 1.234567), VerticalLine 3.0, Point(3.0, 10.998268);
     Line (3.254567, 1.234567), LineSegment ((1.0, 4.489134), (5.0, 17.507402)), LineSegment ((1.0, 4.489134), (5.0, 17.507402));
     Line (3.254567, 1.234567), LineSegment ((0.0, 1.0), (-1.0, 3.254567)), Point(-0.0425778, 1.095994502);
     Line (3.254567, 1.234567), LineSegment ((1.0, 4.489134), (6.0, 17.507402)), Point (1.0, 4.489134);
     Line (3.254567, 0.234567), LineSegment ((1.0, 4.489134), (5.0, 17.507402)), NoPoint;
     Line (3.254567, 1.234567), LineSegment ((1.0, 1.0), (11.0, 36.800237)), NoPoint;
     VerticalLine 10.562435, Point (10.562435, 35.376152390645), Point (10.562435, 35.376152390645);
     VerticalLine 10.552435, Point (10.562435, 35.376152390645), NoPoint;
     VerticalLine 3.0, Line (3.254567, 1.234567), Point (3.0, 10.998268);
     VerticalLine 5.54321012, VerticalLine 5.54321012, VerticalLine 5.54321012;
     VerticalLine 5.54321012, VerticalLine 3.54321012, NoPoint;
     VerticalLine 3.0, LineSegment ((1.0, 4.489134), (5.0, 17.507402)), Point (3.0, 10.998268);
     VerticalLine 5.0, LineSegment ((1.0, 4.489134), (5.0, 17.507402)), Point (5.0, 17.507402);
     VerticalLine 6.0, LineSegment ((1.0, 4.489134), (5.0, 17.507402)), NoPoint;
     LineSegment ((1.0, 4.254567), (11.0, 36.800237)), Point (10.562435, 35.376152390645), Point (10.562435, 35.376152390645);
     LineSegment ((1.0, 4.254567), (11.0, 36.800237)), Point (11.0, 36.800237),  Point (11.0, 36.800237);
     LineSegment ((1.0, 4.254567), (11.0, 36.800237)), Point (0.0, 1.0), NoPoint;
     LineSegment ((1.0, 4.254567), (11.0, 36.800237)), Point (11.0, 36.810237), NoPoint;
     LineSegment ((1.0, 4.489134), (5.0, 17.507402)), Line (3.254567, 1.234567), LineSegment ((1.0, 4.489134), (5.0, 17.507402));
     LineSegment ((0.0, 1.0), (-1.0, 3.254567)), Line (3.254567, 1.234567), Point(-0.0425778, 1.095994502);
     LineSegment ((1.0, 4.489134), (6.0, 17.507402)), Line (3.254567, 1.234567), Point (1.0, 4.489134);
     LineSegment ((1.0, 4.489134), (5.0, 17.507402)), Line (3.254567, 0.234567), NoPoint;
     LineSegment ((1.0, 1.0), (11.0, 36.800237)), Line (3.254567, 1.234567), NoPoint;
     LineSegment ((1.0, 4.489134), (5.0, 17.507402)), VerticalLine 3.0, Point (3.0, 10.998268);
     LineSegment ((1.0, 4.489134), (5.0, 17.507402)), VerticalLine 5.0, Point (5.0, 17.507402);
     LineSegment ((1.0, 4.489134), (5.0, 17.507402)), VerticalLine 6.0, NoPoint;
     LineSegment ((1.0, 4.489134), (5.0, 17.507402)), LineSegment ((5.0, 17.507402), (1.0, 4.489134)), LineSegment ((1.0, 4.489134), (5.0, 17.507402));
     LineSegment ((1.0, 4.489134), (5.0, 17.507402)), LineSegment ((2.0, 7.743701), (3.0, 10.998268)), LineSegment ((2.0, 7.743701), (3.0, 10.998268));
     LineSegment ((2.0, 7.743701), (3.0, 10.998268)), LineSegment ((1.0, 4.489134), (5.0, 17.507402)), LineSegment ((2.0, 7.743701), (3.0, 10.998268));
     LineSegment ((1.0, 4.489134), (3.0, 10.998268)), LineSegment ((2.0, 7.743701), (5.0, 17.507402)), LineSegment ((2.0, 7.743701), (3.0, 10.998268));
     LineSegment ((2.0, 7.743701), (5.0, 17.507402)), LineSegment ((1.0, 4.489134), (3.0, 10.998268)), LineSegment ((2.0, 7.743701), (3.0, 10.998268));
     LineSegment ((3.0, 10.998268), (5.0, 17.507402)), LineSegment ((1.0, 4.489134), (2.0, 7.743701)), NoPoint;
     LineSegment ((2.0, 7.743701), (5.0, 17.507402)), LineSegment ((2.0, 7.743701), (5.0, 10.998268)), Point (2.0, 7.743701);
     LineSegment ((-1.0, -2.02), (5.0, 17.507402)), LineSegment ((0.0, 1.0), (-1.0, 3.254567)), Point(-0.0425778, 1.095994502);
     LineSegment ((2.0, 7.743701), (5.0, 17.507402)), LineSegment ((0.0, 1.0), (-1.0, 3.254567)), NoPoint;
     LineSegment ((1.0, 5.489134), (5.0, 18.507402)), LineSegment ((5.0, 17.507402), (1.0, 4.489134)), NoPoint;
    ]

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

let rec test l = 
    match l with
    | [] -> printfn "Testing complited"
    | (x, y, z) :: tl -> let a = intersect x y
                         if setCompare a z then printfn "OK"
                         else printfn "Test: %A %A Expected %A, but %A found" x y z a
                         test tl

test tests
