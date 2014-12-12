data Tsil a = Lin | Snoc (Tsil a) a
    deriving (Show) 

merge_sort l@(_:_:_) = merge (merge_sort a) (merge_sort b)
    where 
    (a, b) = splitAt (div (length l) 2) l
    merge [] x = x
    merge x [] = x
    merge a@(x:xs) b@(y:ys) = if x < y then x:(merge xs b) else y:(merge a ys)
merge_sort x = x

from_List = from_List' . reverse 
    where 
    from_List' [] = Lin
    from_List' (x:xs) = Snoc (from_List' xs) x

to_List = reverse . to_List'
    where
    to_List' Lin = []
    to_List' (Snoc b a) = a:(to_List' b)

length' Lin = 0
length' (Snoc b _) = 1 + length' b

map' _ Lin = Lin
map' f (Snoc b a) = Snoc (map' f b) (f a)

reverse' = reverse'' Lin
    where 
    reverse'' acc Lin = acc
    reverse'' acc (Snoc b a) = reverse'' (Snoc acc a) b

concat' y Lin = y
concat' x (Snoc b a) = Snoc (concat' x b) a

flatten' Lin = Lin
flatten' (Snoc b a) = concat' (flatten' b) a

--foldl и foldr поменяны местами т.к. теперь левая свёртка - правая и наоборот
foldr' _ a Lin = a
foldr' f a (Snoc xs x) = foldr' f (f a x) xs

foldl' _ a Lin = a
foldl' f a (Snoc xs x) = f x (foldr' f a xs)