module WordsLexer where

import MonadParserCombinators
import qualified MonadParserCombinators

oneOf :: [Parser a] -> Parser a
oneOf = foldl (\ a b -> a ||| b) MonadParserCombinators.fail

letter = oneOf (map sym "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_")
digit  = oneOf (map sym "0123456789")
wss     = oneOf (map sym " \t\r")
puncts  = oneOf (map sym ".,:?;!'\"")

data Token = W String | Eoln | Pu Char deriving Show

ident :: Parser Token
ident = do
        x <- letter 
        xs <- many (letter ||| digit)
        return (W (x:xs))

ws = many wss
    
eoln = do
    sym '\n'
    return Eoln
    
punct = do
    x <- puncts
    return $ Pu x
    
tokenizer = do
    ws
    many token where
        token = do
            x <- oneOf [ident, punct, eoln]
            ws
            return x
            

tokenize :: String -> [Token]
tokenize s = 
  case eof $ apply tokenizer s of
    [tokens] -> tokens
    []       -> error "no tokenization"
    _        -> error "ambiguous tokenization"