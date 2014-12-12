class Map' m where
    empty :: m a b
    add :: (Ord a) => m a b -> a -> b -> m a b
    del :: (Ord a) => m a b -> a -> m a b
    find :: (Ord a) => m a b -> a -> Maybe b
    fold :: (a -> b -> [(a, b)] -> [(a, b)]) -> m a b -> [(a, b)] -> [(a, b)]
    
newtype L a b = L [(a, b)] deriving Show

instance Map' L where
    empty = L []
    
    add (L []) a b = L [(a, b)]
    add (L l@((c, _):_)) a _ | c == a = L l
    add (L l@((c, _):_)) a b | c > a = L $ (a, b):l
    add (L (x:xs)) a b = L (x:l) where
        L l = add (L xs) a b
    
    del (L []) _ = L []
    del (L ((c, _):xs)) a | c == a = L xs
    del (L (x:xs)) a = L (x:l) where
        L l = del (L xs) a
    
    find (L []) _ = Nothing
    find (L ((c, d):xs)) a = if c == a then Just d
                           else find (L xs) a
    
    fold f (L l) c = foldl (\c (a, b) -> f a b c) l c

