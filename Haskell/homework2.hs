rnorm (a, b) =
    let r = gcd a b in
    (div a r, div b r)

radd (a, b) (c, d) = rnorm (a * d + b * c, b * d)

rsub x (c, d) = radd x (-c, d)

rmul (a, b) (c, d) = rnorm (a * c, b * d)

rdiv x (c, d) = rmul x (d, c)

rinv (a, b) = rnorm (b, a)

map' f l = [f x | x <- l]

zip' (x:xs) (y:ys) = (x, y) : zip' xs ys
zip' _ _ = []

-- Как вариант
zip'' x y = [(x !! n, y !! n) | n <- [0..min (length x) (length y) - 1]]

unzip' x = ([a | (a, _) <- x], [b | (_, b) <- x])

--Возможно неправильно понял смысл функции
flatten l = [a | x <- l, a <- x]