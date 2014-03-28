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