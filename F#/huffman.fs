(* Tashkinov Mikhail (c) 2014
   Huffman
*)

module Huffman

type CodeTree = 
  | Fork of left: CodeTree * right: CodeTree * chars: char list * weight: int
  | Leaf of char: char * weight: int


// code tree

let createCodeTree (chars: char list) : CodeTree = 
    failwith "Not implemented"

//let tree : CodeTree = Fork(Fork(Leaf('b', 1), Leaf('c', 1), ['b'; 'c'], 2), Leaf('a', 2), ['a'; 'b'; 'c'], 4)
let tree = Fork ( Fork (Leaf ('a', 3), Leaf('b', 2), ['a'; 'b'], 5), Fork (Leaf ('c', 2), Leaf('d', 1), ['c'; 'd'], 3) , ['a'; 'b'; 'c'; 'd'], 8)

type Bit = int

let decode (tree: CodeTree)  (bits: Bit list) : char list =
    let rec decode' (wTree: CodeTree) (tree: CodeTree)  (bits: Bit list) : char list = 
        match wTree with
        | Leaf(a, _) -> a :: decode' tree tree bits
        | Fork(l, r, _, _) -> match bits with
                              | [] -> []
                              | hd :: tl ->  if hd = 0 then decode' l tree tl
                                                       else decode' r tree tl
    decode' tree tree bits
                                                
let rec addByte (x: char) (tree: CodeTree) =
    match tree with
    | Fork(a, b, _, _) -> match a with
                          | Leaf(z, _) -> if z = x then [0]
                                                   else 1 :: (addByte x b)
                          | Fork(_, _, k, _) -> if List.exists (fun i -> x = i) k then 0 :: (addByte x a)
                                                                                  else 1 :: (addByte x b)
    | Leaf(a, b) -> []

let rec encode (tree: CodeTree)  (text: char list) : Bit list = 
    match text with
    | [] -> []
    | hd :: tl -> addByte hd tree @ encode tree tl

printfn "%A" (decode tree [1; 1; 0; 0; 0; 1])
