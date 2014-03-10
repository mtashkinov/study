(* Tashkinov Mikhail (c) 2014
   Map at CPS
*)

let func s f a = s (f a)

let rec map f l g =
    match l with
    | [] -> g []
    | hd :: tl -> map f tl (fun x -> g ((f hd) :: x))

let l = [1; 2; 3; 4]

map (func (printfn "%A") (fun x -> x * x)) l (printfn "%A")