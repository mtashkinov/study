data Lambda = Var String | App Lambda Lambda | Lam String Lambda
    deriving (Show)
data Lambda' = Var' Int | App' Lambda' Lambda' | Lam' Lambda'
    deriving (Show)    

fromLambda = de [] where
    de acc (Lam x y) = Lam' (de (x:acc) y )
    de acc (Var x) = Var' (fnd acc 0) where
        fnd (y:ys) ac = if y == x then ac else fnd ys (ac+1)
    de acc (App x y) = App' (de acc x) (de acc y)
    
toLambda = unde [] where
    unde acc (Var' x) = Var ((!!) acc x)
    unde acc (App' x y) = App (unde acc x) (unde acc y)
    unde acc (Lam' x) = Lam next (unde (next:acc) x) where
        next = (!!) names $ length acc
        names = [x:name | name <- "" : names, x <- ['a'..'z']]
        
subst = subst' 0 where     
    subst' y (Var' x) b | x == y = b
    subst' _ x@(Var' _) _ = x
    subst' y (App' m n) b = App' (subst' y m b) (subst' y n b)
    subst' x (Lam' a) b = Lam' (subst' (x + 1) a (shift 0 b)) where
        shift n (App' a b) = App' (shift n a) (shift n b)
        shift n (Lam' a) = Lam' (shift (n + 1) a)
        shift n (Var' i) | i >= n = Var' (i + 1)
        shift _ x  = x
    
red x@(Var' _) = x
red (Lam' a) = Lam' (red a)
red (App' (Lam' a) b) = red $ subst a b
red (App' a b) =
    let l = red a in
    case l of
    (Lam' a') -> red $ subst a' b
    _ -> App' l (red b)
    
cbn x@(Var' _) = x
cbn x@(Lam' _) = x
cbn (App' a b) =
    let l = cbn a in
    case l of
    (Lam' a') -> cbn $ subst a' b
    _ -> App' l b
    
cbv x@(Var' _) = x
cbv x@(Lam' _) = x
cbv (App' a b) =
    let l = cbv a in
    let m = cbv b in
    case l of
    (Lam' a') -> cbv $ subst a' m
    _ -> App' l m