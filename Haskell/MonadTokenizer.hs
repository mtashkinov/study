module Tokenizer where

import MonadParserCombinators
import qualified MonadParserCombinators

oneOf :: [Parser a] -> Parser a
oneOf = foldl (\ a b -> a ||| b) MonadParserCombinators.fail

letter = oneOf (map sym "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_")
digit  = oneOf (map sym "0123456789")
ws     = oneOf (map sym " \t\n\r")

whitespace = many ws

data Token = X String | C Int | A | S | M | D | LP | RP deriving Show

ident :: Parser Token
ident = do
        x <- letter 
        xs <- many (letter ||| digit) 
        return (X (x:xs))

int :: Parser Token
int = do
      x <- some digit
      return $ C (read x)

add :: Parser Token
add = do 
      sym '+'
      return A

sub :: Parser Token
sub = do 
      sym '-'
      return S

mul :: Parser Token
mul = do 
      sym '*'
      return M

dv :: Parser Token
dv = do 
     sym '/'
     return D

lp :: Parser Token
lp = do 
     sym '('
     return LP

rp :: Parser Token
rp = do 
     sym ')'
     return RP

tokenizer :: Parser [Token]
tokenizer = do
    whitespace
    many token where
        token = do
            x <- oneOf [ident, int, add, sub, mul, dv, lp, rp] 
            whitespace
            return x

tokenize :: String -> [Token]
tokenize s = 
  case eof $ apply tokenizer s of
    [tokens] -> tokens
    []       -> error "no tokenization"
    _        -> error "ambiguous tokenization"



