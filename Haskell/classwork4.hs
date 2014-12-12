red a@(Var _) = a
red (Lam x a) = Lam x a
red (App a b) =
    let l = red a in
    case l of
    (Lam x a') -> red $ subst' a' x b
    _ -> App l b
    
