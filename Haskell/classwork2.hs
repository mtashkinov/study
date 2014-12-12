unzip' ((x, y):xs) = (x:tx, y:ty) where
                     (tx, ty) = unzip xs
                     
inverse = inverse' []
    where 
    inverse' acc [] = acc
    inverse' acc (x:xs) = inverse' (x:acc) xs

-- Описание инфиксной операции
infixr 5 +++
--Правоассациативная операция, infixl - лево
[] +++ y = y
(x:xs) +++ y = x : (xs +++ y)

foldr' f a [] = a
foldr' f a (x:xs) = f x (foldr' f a xs)