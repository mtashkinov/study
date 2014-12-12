data Lambda = Var String | App Lambda Lambda | Lam String Lambda

show' (Var x) = x
show' (Lam x y) = "\\" ++ x ++ show' y
show' (App x y) = (lam_x $ show' x) ++ (app_y $ show' y) where
    lam_x = case x of Lam _ _ -> br; _ -> id
    app_y = case y of App _ _ -> br; _ -> id
    br x = "(" ++ x ++ ")"
    
fv (Var x) = [x]
fv (App a b) = (fv a) ++ (fv b)
fv (Lam x a) = filter (/= x) (fv a)

subst (Var a) x b | a == x = b
subst (Var a) _ _ = Var a
subst (App a c) x b = App (subst a x b) (subst c x b)
subst (Lam y a) x b | y == x = (Lam x a)
subst (Lam y a) x b = Lam y (subst a x b)

subst' (Var a) x b | a == x = b
subst' (Var a) _ _ = Var a
subst' (App a c) x b = App (subst' a x b) (subst' c x b)
subst' (Lam y a) x b | y == x = (Lam x a)
subst' (Lam y a) x b | elem y (fv b) = subst' (Lam z (subst' a y (Var z))) x b where
    z = gen "z"
    ar = fv a
    br = fv b
    gen x = if (elem x ar) || (elem x br) then gen ('z':x) else x
subst' (Lam y a) x b = Lam y (subst' a x b)

red a@(Var _) = a
red (Lam x a) = Lam x (red a)
red (App (Lam x a) b) = red $ subst' a x b
red (App a b) =
    let l = red a in
    case l of
    (Lam x a') -> red $ subst' a' x b
    _ -> App l (red b)

