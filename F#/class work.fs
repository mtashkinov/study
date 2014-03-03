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

let reduction f g k x y =
    let a = f x in 
        let b = f y in
            match a with
            | Const c -> match b with
                         | Const d -> Const (k c d)
                         | _ -> g (a, b)
            |_ -> g (a, b)
    

let rec calc expr =
    match expr with
    | Const x -> Const x
    | Var s -> Var s
    | Add (x, y) -> reduction calc Add (+) x y
    | Sub (x, y) -> reduction calc Sub (-) x y
    | Mul (x, y) -> reduction calc Mul (*) x y
    | Div (x, y) -> reduction calc Div (/) x y

printfn "%A" (calc (Mul ((Add (Const 1, Const 2)), (Add (Var "x", Const 2)))))