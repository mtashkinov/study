(* Tashkinov Mikhail (c) 2014
   Count sum of list elements
*)

let rec sum list =
    match list with
    | [] -> 0
    | head :: tail -> sum tail + head

let rec scan size =
    match size with
    | 0 -> []
    | _ -> let el = System.Int32.Parse(System.Console.ReadLine())
           el :: scan (size - 1)
            
let size = System.Int32.Parse(System.Console.ReadLine())

printfn "%A" (sum (scan size))