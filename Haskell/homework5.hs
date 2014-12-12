data P = Z | S P

instance Num P where
    infixl 6 +
    + x = x
    
    infixl 6 -
    - _ = error "A negative number"
    
    
