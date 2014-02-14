(* Tashkinov Mikhail (c) 2014
   Create list with all squares, which less then n
*)


let n = System.Int32.Parse(System.Console.ReadLine())

let rec sqrListCreate i =
     if i * i <= n then i * i :: sqrListCreate (i + 1)
     else []

printfn "%A" (sqrListCreate 1)