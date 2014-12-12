prim x = prim' 2
    where
    prim' d = d * d > x || (mod x d /= 0 && prim' (d + 1))
    
bor x y = if x then True else y

sum' [] = 0
sum' (h:tl) = h + sum' tl

concat' [] y = y
concat' (h:tl) y = h : concat' tl y

length' [] = 0
length' (h:tl) = 1 + length' tl

inverse' [] = []
inverse' (h:tl) = concat' (inverse' tl) [h]

diveders

