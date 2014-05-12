(* Tashkinov Mikhail (c) 2014
   Class Work
*)

let fibList n =
    let rec fibList' fst sec =
        if (sec > n) then [fst]
        else fst :: fibList' sec (fst + sec)
    fibList' 1 1

let rec filterSum f l =
    match l with
    | [] -> 0
    | hd :: tl -> if f hd then hd + filterSum f tl
                  else filterSum f tl

printfn "%A" (filterSum (fun x -> x % 2 = 0) (fibList 4000000))

let rec findMinDevider n dev =
    if (n % dev = 0L) then dev
    else if (n < dev * dev) then n
         else findMinDevider n (dev + 1L)

let rec GPD (n : int64) =
    let dev = findMinDevider n 2L in
        if (dev = n) then (int64 n)
        else GPD (n / dev)

printfn "%A" (GPD 600851475143L)


let rec fact n =
    if (n = 1) then 1I
    else bigint n * fact (n - 1)

let rec digitsSum n =
    if n = 0I then 0I
    else (n % 10I) + digitsSum (n / 10I)

printfn "%A" (digitsSum (fact 100))

type Expr =
    | Const of int
    | Var of string
    | Add of Expr * Expr
    | Sub of Expr * Expr
    | Mul of Expr * Expr
    | Div of Expr * Expr

let rec calc expr =
    match expr with
    | Const x -> Const x
    | Var s -> Var s

    | Add (Const x, Const y) -> Const (x + y)
    | Add (Const 0, x) -> calc x
    | Add (x, Const 0) -> calc x
    | Add (x, y) -> let a = calc x
                    let b = calc y
                    if (x = a) && (y = b) then Add (a, b)
                                          else calc (Add (a, b))

    | Sub (Const x, Const y) -> Const (x - y)
    | Sub (Var x, Var y) -> if (x = y) then Const 0
                            else Sub (Var x, Var y)
    | Sub (x, y) -> let a = calc x
                    let b = calc y
                    if (x = a) && (y = b) then Sub (a, b)
                                          else calc (Sub (a, b))

    | Mul (Const x, Const y) -> Const (x * y)
    | Mul (Const 0, x) -> Const 0
    | Mul (x, Const 0) -> Const 0
    | Mul (Const 1, x) -> calc x
    | Mul (x, Const 1) -> calc x
    | Mul (x, y) -> let a = calc x
                    let b = calc y
                    if (x = a) && (y = b) then Mul (a, b)
                                          else calc (Mul (a, b))

    | Div (Const x, Const y) -> Const (x / y)
    | Div (x, Const 1) -> calc x
    | Div (x, y) -> let a = calc x
                    let b = calc y
                    if (x = a) && (y = b) then Div (a, b)
                                          else calc (Div (a, b))
  
printfn "%A" (calc (Add (Add (Const 3, Const 5), (Add (Const 4, Const 5)))))