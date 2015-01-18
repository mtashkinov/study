module MonadParserCombinators where

newtype Parser a = P (String -> [(a, String)])

instance Monad Parser where
    (P p) >>= q = P (\s -> concat [apply (q a) s | (a, s) <- p s])
    return a = P (\s -> [(a, s)])

apply :: Parser a -> String -> [(a, String)]
apply (P p) = p 

fail :: Parser a
fail = P (\ _ -> [])

any :: Parser ()
any = P p where
  p []     = [((), [])]
  p (_:ss) = [((), ss)]

sym :: Char -> Parser Char
sym c = P p where p (x:xs) | x == c = [(x, xs)]
                  p _               = []

lift :: Parser a -> b -> Parser a
lift p _ = p

infixl 2 |||
(|||) :: Parser a -> Parser a -> Parser a
(P p) ||| (P q) = P (\ s -> p s ++ q s)

many :: Parser a -> Parser [a]
many a = (do
        x <- a
        y <- many a
        return (x:y)) ||| return []

some :: Parser a -> Parser [a]
some a = do
        x <- a
        y <- many a
        return (x:y)

opt :: Parser a -> Parser (Maybe a)
opt a = do
        x <- a 
        return (Just x) ||| return Nothing

eof :: Eq s => [(a, [s])] -> [a]
eof = map fst . filter ((==[]) . snd)