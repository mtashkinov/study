newtype Parser s a = P (s -> [(a, s)])

fail' = P (\_ -> [])

any' = P fun where
    fun (_:xs) = [((), xs)]
    fun [] = [((), [])]
    
sym c = P fun where
    fun (x:xs) | x == c = [(x, xs)]
    fun _ = []
    
val a = P (\s -> [(a, s)])

infixl 2 |||
(P p) ||| (P q) = P fun where
    fun x = (p x) ++ (q x)
    
infixl 3 ||>
(P a) ||> f = P p where
    p s = concat [apply (f ai) si | (ai, si) <- a s]

apply (P a) = a

lift p _ = p

many p = val [] ||| p ||> (\x -> many p ||> (\y -> val (x:y)))

some p = p ||> (lift $ many p)

eof l = [a | (a, []) <- l]
