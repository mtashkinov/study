(* Tashkinov Mikhail (c) 2014
   List elements filter
*)

let rec filter f list =
    match list with
    | [] -> []
    | head :: tail -> if (f head) then head :: filter f tail
                      else filter f tail
let list = [0; 4; 2; 1; 5]
printfn "%A" (filter (fun x -> x > 2) list)