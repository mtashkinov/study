(* Tashkinov Mikhail (c) 2014
   List elements filter
*)

let rec filter list =
    match list with
    | [] -> []
    | head :: tail -> if head > 2 then filter tail
                      else head :: filter tail
let list = [0; 4; 2; 1; 5]
printfn "%A" (filter list)