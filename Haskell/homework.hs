isGSqr x a = a * a > x

prim 1 = False
prim x = prim' 2
    where
    prim' a | isGSqr x a = True
    prim' a | mod x a == 0 = False
    prim' a = prim' (a + 1)

gcd' a b | b > a = gcd' b a
gcd' a b = 
    let r = mod a b in
    (if r == 0 then id
               else gcd' r) b  

gcd'' a b = (\r -> if r == 0 then b else gcd b r) $ mod a b

rprim a b = gcd' a b == 1

lcm a b = div (a * b) $ gcd' a b

dev x f = dev' 1
    where
    dev' c | isGSqr x c = 0
    dev' c | c * c == x = f c + nxt c
    dev' c | mod x c == 0 = f c + f (div x c) + nxt c
    dev' c = nxt c
    nxt c = dev' (c + 1)

nd x = dev x (\ _ -> 1)

sd x = dev x id

euler 1 = 1
euler x = euler' 1
    where
    euler' c | x <= c = 0
    euler' c | rprim x c = 1 + nxt c
    euler' c = nxt c
    nxt c = euler' (c + 1)
