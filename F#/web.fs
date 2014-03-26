(* Tashkinov Mikhail (c) 2014
   Creates list of images URL
*)

module ListCr

open WebR
open System

let urls =
    [
     "http://yandex.ru";
     "http://google.ru"
    ]

let n = 2

let rec imagesCount (s:string) (pos:int) =
    let count = 0
    let x = s.IndexOf("<img", pos)
    if x = -1 then 0
        else 1 + imagesCount s (x + 4)

let rec getImages (s:string) (pos:int) =
    if (pos = 0) && (imagesCount s 0 <= n) then []
    else
        let count = 0
        let x = s.IndexOf("<img", pos)
        if x = -1 then []
        else 
            let a = s.IndexOf("src=", x + 4)
            let b = s.IndexOf("\"", a + 5)
            s.Substring(a + 5, b - a - 5) :: getImages s (b + 1)

let rec imgParse urls g =
    match urls with
    | [] -> g []
    | hd :: tl -> getUrl hd (fun x -> imgParse tl (fun y -> g (getImages x 0 @ y)))

imgParse urls (printfn "%A")
Console.ReadLine()
