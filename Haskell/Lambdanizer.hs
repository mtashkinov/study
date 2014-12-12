module Lamdanizer where

import ParserCombinators
import qualified ParserCombinators

data Lambda = Var String | App Lambda Lambda | Lam String Lambda deriving Show
    
oneOf = foldl (\ a b -> a ||| b) ParserCombinators.fail

letter = oneOf (map sym "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_")
digit  = oneOf (map sym "0123456789")

var = letter ||> (  
          \x  -> many (letter ||| digit) ||> (  
          \xs -> val (Var (x:xs))
        ))
        
lp = sym '('

rp = sym ')'

sl = sym '\\'

po = sym '.'

app = lp ||> lift lambdanizer ||> (
    \x -> rp ||> (lift lp) ||> (lift lambdanizer) ||> (
    \y -> rp ||> (lift $ val (App x y))))

lam = sl ||> (lift var) ||> (
    \(Var x) -> po ||> (lift lambdanizer) ||> (
    \y -> val (Lam x y)))

lambdanizer = var ||| app ||| lam