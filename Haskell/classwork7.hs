import Tokenizer
import qualified Tokenizer

import ParserCombinators
import qualified ParserCombinators

data T = X String | C Int | A T T | S T T | M T T | D T T

parseE = parseM ||| (parseE ||> lift add ||> lift parseM) ||| (parseM ||> lift sub ||> lift parseM)

parseM = parseP ||| (parseM ||> lift mul ||> lift parseP) ||| (parseM ||> lift dv ||> lift parseP)

parseP = ident ||| int ||| (lp ||> lift parseE ||> lift rp)
