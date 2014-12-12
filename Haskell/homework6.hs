data E = X String | A E E | C Int

eval f (X s) = f s
eval _ (C s) = s
eval f (A x y) = (eval f x) + eval f y