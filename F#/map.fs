(* Tashkinov Mikhail (c) 2014
   Map at CPS
*)

let sqr'cps x s = s (x * x);

let rec map f l g =
    match l with
    | [] -> g []
    | hd :: tl -> f hd (fun x -> map f tl (fun y -> g (x :: y)))

let l = [2..5]

map sqr'cps l (printfn "%A")