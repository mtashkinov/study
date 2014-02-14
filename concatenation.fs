(* Tashkinov Mikhail (c) 2014
   Concatinate two lists
*)

let rec addToEnd list el =
    match list with
    | [] -> [el]
    | head :: tail -> head :: addToEnd tail el

let rec conc list1 list2 =
    match list2 with
    | [] -> list1
    | head :: tail -> conc (addToEnd list1 head) tail

let list1 = [1; 2; 3]
let list2 = [4; 5; 6]

printfn "%A" (conc list1 list2)