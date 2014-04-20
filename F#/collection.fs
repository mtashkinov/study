(* Tashkinov Mikhail (c) 2014
   AVL tree
*)

module Map

type Tree<'key, 'value when 'key: comparison> =
        | Node of left: Tree<'key, 'value> * right: Tree<'key, 'value> * key: 'key * value: 'value * lHeight: int * rHeight: int
        | Empty

type Map<'key, 'value when 'key: comparison>private(t:Tree<'key, 'value>) = 
    static member Empty = new Map<_, _>(Tree.Empty)
    member private this.Balance(way) =
        let balance tree way =
            match way with
            | [] -> failwith "Incorrect way to balance"
            | x :: tl -> match tl with
                         | [] -> failwith "Incorrect way to balance"
                         | y :: tl -> if x = y then if x then match tree with
                                                              | Node(s, Node(r, Node(p, q, c1, c2, clh, crh), b1, b2, blh, brh), a1, a2, alh, arh) -> Node(Node(s, r, a1, a2, alh, blh), Node(p, q, c1, c2, clh, crh), b1, b2, max blh alh + 1, brh)
                                                              | _ -> failwith "Incorrect tree to balance"
                                                    else match tree with
                                                         | Node(Node(Node(p, q, c1, c2, clh, crh), r, b1, b2, blh, brh), s, a1, a2, alh, arh) -> Node(Node(p, q, c1, c2, clh, crh), Node(r, s, a1, a2, brh, arh), b1, b2, blh, max brh arh + 1)
                                                         | _ -> failwith "Incorrect tree to balance"
                                      else if x then match tree with
                                                     | Node(s, Node(Node(p, q, c1, c2, clh, crh), r, b1, b2, blh, brh), a1, a2, alh, arh) -> Node(Node(s, p, a1, a2, alh, clh), Node(q, r, b1, b2, crh, brh), c1, c2, max alh clh + 1, max crh brh + 1)
                                                     | _ -> failwith "Incorrect tree to balance"
                                                else match tree with
                                                     | Node(Node(r, Node(p, q, c1, c2, clh, crh), b1, b2, blh, brh), s, a1, a2, alh, arh) -> Node(Node(r, p, b1, b2, blh, clh), Node(q, s, a1, a2, crh, arh), c1, c2, max blh clh + 1, max crh arh + 1)
                                                     | _ -> failwith "Incorrect tree to balance"
        balance t way
    member this.Add(x: 'key, y: 'value) =
        let findNextStep t x =
            match t with
            | Empty -> failwith "Catn't find next step"
            | Node(_, _, y, _, _, _) -> if x > y then [true]
                                                 else [false]
        let rec add tree x y =
            match tree with
            | Empty -> Node(Empty, Empty, x, y, 0, 0)
            | Node(a, b, m, n, hl, hr) -> if x > m then let mutable h = hr + 1
                                                        let c = add b x y
                                                        match c with
                                                        | Empty -> h <- 0
                                                        | Node(_, _, _, _, l, r) -> h <- max l r + 1
                                                        let mutable tr = Node(a, c, m, n, hl, h)
                                                        if h > hl + 1 then tr <- (new Map<_, _>(tr)).Balance (true :: (findNextStep b x))
                                                        tr
                                          else let mutable h = hl + 1
                                               let c = add a x y
                                               match c with
                                               | Empty -> h <- 0
                                               | Node(_, _, _, _, l, r) -> h <- max l r + 1
                                               let mutable tr = Node(c, b, m, n, h, hr)
                                               if h > hr + 1 then tr <- (new Map<_, _>(tr)).Balance (false :: (findNextStep a x))
                                               tr
        new Map<_, _>(add t x y)
    member this.ContainsKey(x: 'key) =
        let rec containsKey tree x =
            match tree with
            | Empty -> false
            | Node(a, b, m, n, _, _) -> if m = x then true
                                                 else containsKey a x ||containsKey b x
        containsKey t x  
    member this.Count =
        let rec count = function
            | Empty -> 0
            | Node(a, b, _, _, _, _) -> 1 + count a + count b
        count t
    member this.IsEmpty = 
        match t with
        | Empty -> true
        | Node(_, _, _, _, _, _) -> false
    member this.Item(x: 'key) =
        let rec item tree x =
            match tree with
            | Empty -> failwith "No such element in Map"
            | Node(a, b, m, n, _, _) -> if m = x then n
                                                 else if x > m then item b x
                                                      else item a x
        item t x
    member this.Remove(x: 'key) =
        let rec findMin = function
            | Empty -> None
            | Node(a, _, m, n, _, _) -> match a with
                                        | Empty -> Some (m, n)
                                        | Node(_, _, _, _, _, _) -> findMin a
        let rec delMin = function
            | Empty -> failwith "No min in Map"
            | Node(a, b, m, n, l, r) -> match a with
                                        | Empty -> b
                                        | Node(_, _, _, _, _, _) -> let c = delMin a
                                                                    let mutable h = l - 1
                                                                    match c with
                                                                    | Empty -> h <- 0
                                                                    | Node(_, _, _, _, hl, hr) -> h <- max hl hr + 1
                                                                    Node(c, b, m, n, h, r)
        let rec delNode = function
            | Empty -> Empty
            | Node(a, b, m, n, l, r) -> let res = findMin b
                                        match res with
                                        | Some(k, v) -> let c = delMin b
                                                        let mutable h = r
                                                        match c with
                                                        | Empty -> h <- 0
                                                        | Node(_, _, _, _, hl, hr) -> h <- max hl hr + 1
                                                        Node(a, c, k, v, l, h)
                                        | None -> a
        let findWay = function
            | Empty -> failwith "Can't find way"
            | Node(a, b, _, _, l, r) -> r > l
        let rec remove tree x =
            match tree with
            | Empty -> Empty
            | Node(a, b, m, n, l, r) -> if m = x then let c = delNode tree
                                                      match c with
                                                      | Empty -> c
                                                      | Node(f, _, _, _, hl, hr) -> if hl > hr + 1 then (new Map<_, _>(c)).Balance [false; findWay f]
                                                                                                   else c
                                                 else if x > m then let mutable c = remove b x
                                                                    let mutable h = r
                                                                    match c with
                                                                    | Empty -> h <- 0
                                                                    | Node(_, _, _, _, hl, hr) -> h <- max hl hr + 1
                                                                    c <- Node(a, c, m, n, l, h)
                                                                    if l > h + 1 then (new Map<_, _>(c)).Balance [false; findWay a]
                                                                                 else c
                                                      else let mutable c = remove a x
                                                           let mutable h = l
                                                           match c with
                                                           | Empty -> h <- 0
                                                           | Node(_, _, _, _, hl, hr) -> h <- max hl hr + 1
                                                           c <- Node(c, b, m, n, h, r)
                                                           if r > h + 1 then (new Map<_, _>(c)).Balance [true; findWay b]
                                                                        else c
        new Map<_, _>(remove t x)
    override this.ToString() =
        let rec toString = function
            | Empty -> "Empty"
            | Node(a, b, m, n, _, _) -> "Node(" + toString a + ", " + toString b + ", " + m.ToString() + ", " + n.ToString() + ")"
        toString t
    member this.TryFind(x: 'key) = 
        let rec tryFind tree x =
            match tree with
            | Empty -> None
            | Node(a, b, m, n, _, _) -> if m = x then Some n
                                                 else let r = tryFind a x
                                                      match r with
                                                      | None -> tryFind b x
                                                      | Some a -> Some a
        tryFind t x
