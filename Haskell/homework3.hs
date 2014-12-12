maxL l@(x:_) = foldl max x l

minL l@(x:_) = foldl min x l

fib@(_:xs) = 1:1:(zipWith (+) fib xs)

prim = 2:gen [3,5..]
    where
    gen (x:xs) = x:gen [a | a <- xs, mod a x /= 0]
    
prim' = 2:[a | a <- [3,5..], isPrim a prim']
    where
    isPrim a (x:xs) = x * x > a || (mod a x /= 0 && isPrim a xs)