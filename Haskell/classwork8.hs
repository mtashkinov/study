data P = Z | S P

instance Eq P where
    Z == Z = True
    S x == S y = x == y
    _ == _ = False
    
instance Ord P where
    compare Z Z = EQ
    compare _ Z = GT
    compare Z _ = LT
    compare (S x) (S y) = compare x y
    
instance Show P where
    show Z = "Z"
    show (S x) = "S" ++ show x
    
instance Num P where
    (S x) + (S y) = S $ S $ x + y
    x + y = case x of
            Z -> y
            _ -> x
            
    (S x) - (S y) = x - y
    x - Z = x
    Z - x = error "Negative number"
    
    (S x) * y@(S _) = y + x * y
    _ * _ = Z
    
    negate _ = error "Negative number"
    
    abs x = x
    
    signum Z = Z
    signum _ = S Z
    
    fromInteger x = S $ fromInteger (x - 1)
    fromInteger 0 = Z
    

    