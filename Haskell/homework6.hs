data E = X String | A E E | C Int | S E E | M E E | D E E
data R m = Ok m | Fail String


instance Monad R where
    Ok x' >>= f = f x'
    (Fail s) >>= _ = Fail s
    return = Ok
             
eval f (X s) = f s
eval _ (C s) = Ok s
eval f (A x y) = do
    x' <- eval f x
    y' <- eval f y
    return $ x' + y'
    
eval f (S x y) = do
    x' <- eval f x
    y' <- eval f y
    return $ x' - y'
    
eval f (M x y) = do
    x' <- eval f x
    y' <- eval f y
    return $ x' * y'
    
eval f (D x y) = do
    x' <- eval f x
    y' <- eval f y
    if y' == 0 then Fail "Division by zero" else return $ x' `div` y'