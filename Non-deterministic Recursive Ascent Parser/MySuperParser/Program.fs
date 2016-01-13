open LanguagePrimitives

type Symbol<'Nonterminal, 'Terminal
    when 'Nonterminal : comparison
    and 'Terminal : comparison> =
    /// An elementary symbol of the language described by the grammar.
    /// Terminal symbols are often called "tokens", especially when
    /// discussing lexical analysers and parsers.
    | Terminal of 'Terminal
    /// Nonterminal symbols are groups of zero or more terminal symbols;
    /// these groups are defined by the production rules of the grammar.
    | Nonterminal of 'Nonterminal

    /// <inherit />
    override this.ToString () =
        match this with
        | Terminal token ->
            token.ToString ()
        | Nonterminal nonterm ->
            nonterm.ToString ()

[<Measure>] type ParserPosition

type Grammar<'Nonterminal, 'Terminal
    when 'Nonterminal : comparison
    and 'Terminal : comparison> = {
    //
    Terminals : Set<'Terminal>;
    //
    Nonterminals : Set<'Nonterminal>;
    //
    Productions : Map<'Nonterminal, Symbol<'Nonterminal, 'Terminal>[][]>;
}

type Item<'Nonterminal, 'Terminal, 'Lookahead
    when 'Nonterminal : comparison
    and 'Terminal : comparison
    and 'Lookahead : comparison> = {
    //
    Nonterminal : 'Nonterminal;
    //
    Production : Symbol<'Nonterminal, 'Terminal>[];
    //
    Lookahead : 'Lookahead;
    //
    Position : int<ParserPosition>;
} with
    member this.CurrentSymbol
        with get () =
            let position = int this.Position
            if position = Array.length this.Production then None
            else Some this.Production.[position]

type Items<'Nonterminal, 'Terminal, 'Lookahead
    when 'Nonterminal : comparison
    and 'Terminal : comparison
    and 'Lookahead : comparison> =
    Set<Item<'Nonterminal, 'Terminal, 'Lookahead>>

type MyParserState<'Nonterminal, 'Terminal, 'Lookahead
    when 'Nonterminal : comparison
    and 'Terminal : comparison
    and 'Lookahead : comparison> =
    Set<Item<'Nonterminal, 'Terminal, 'Lookahead> * int>
                

    
let rec private closureImpl (productions : Map<'Nonterminal, Symbol<'Nonterminal, 'Terminal>[][]>) items pendingItems : Items<_,_,_> =
    match pendingItems with
    | [] ->
        items
    | _ ->
        // Process the worklist.
        let items, pendingItems =
            ((items, []), pendingItems)
            ||> List.fold (fun (items, pendingItems) (item : Item<_,_,_>) ->
                // Add the current item to the item set.
                let items = Set.add item items                

                // If the position is at the end of the production, or if the current symbol
                // is a terminal, there's nothing that needs to be done for this item.
                match item.CurrentSymbol with
                | None
                | Some (Symbol.Terminal _) ->
                    items, pendingItems
                | Some (Symbol.Nonterminal nontermId) ->
                    // For all productions of this nonterminal, create a new item
                    // with the parser position at the beginning of the production.
                    // Add these new items into the set of items.
                    let pendingItems =
                        /// The productions of this nonterminal.
                        let nontermProductions = Map.find nontermId productions

                        (pendingItems, nontermProductions)
                        ||> Array.fold (fun pendingItems production ->
                            let newItem = {
                                Nonterminal = nontermId;
                                Production = production;
                                Position = GenericZero;
                                Lookahead = (); }

                            // Only add this item to the worklist if it hasn't been seen yet.
                            if Set.contains newItem items then pendingItems
                            else newItem :: pendingItems)

                    // Return the updated item set and worklist.
                    items, pendingItems)

        // Recurse to continue processing.
        // OPTIMIZE : It's not really necessary to reverse the list here -- we could just as easily
        // process the list in reverse but for now we'll process it in order to make the algorithm
        // easier to understand/trace.
        closureImpl productions items (List.rev pendingItems)

/// Computes the closure of a set of items.
let closure (productions : Map<'Nonterminal, Symbol<'Nonterminal, 'Terminal>[][]>) items : Items<_,_,_> =
    // Call the recursive implementation, starting with the specified initial item set.
    Set.difference (closureImpl productions Set.empty (Set.toList items)) items

/// Moves the 'dot' (the current parser position) past the
/// specified symbol for each item in a set of items.
let goto symbol items closure (productions : Map<'Nonterminal, Symbol<'Nonterminal, 'Terminal>[][]>) : Items<_,_,_> =
    (Set.empty, Set.union items closure)
    ||> Set.fold (fun updatedItems (item : Item<_,_,_>) ->
        // If the next symbol to be parsed in the production is the
        // specified symbol, create a new item with the position advanced
        // to the right of the symbol and add it to the updated items set.
        match item.CurrentSymbol with
        | Some sym when sym = symbol ->
            let updatedItem =
                { item with
                    Position = item.Position + 1<_>; }
            Set.add updatedItem updatedItems

        | _ ->
            updatedItems)


let final (item : Item<_,_,_>) : bool =
    item.CurrentSymbol = None

let lhs (item : Item<_,_,_>) : Symbol<'Nonterminal, 'Terminal> =
    Symbol.Nonterminal item.Nonterminal

let pop (item : Item<_,_,_>) : Item<_,_,_> =
    { item with
        Position = item.Position - 1<_>}

let eps (item : Item<_,_,_>) : bool =
    item.Position = 0<_> && item.CurrentSymbol = None

let string : string = "111000"
let mutable productions : Map<'Nonterminal, Symbol<'Nonterminal, 'Terminal>[][]> = Map.empty

let findEpsilonNonterminals (items : Items<_,_,_>) : Set<'Nonterminal> =
    let mutable nonterminals : Set<'Nonterminal> = Set.empty
    Set.iter (fun x -> if eps x then nonterminals <- nonterminals.Add x.Nonterminal) items
    nonterminals

let findFinalItems (items : Items<_,_,_>) position : MyParserState<_,_,_> =
    let mutable finalItems : MyParserState<_,_,_> = Set.empty
    Set.iter (fun x -> if final x then finalItems <- finalItems.Add (x, position)) items
    finalItems

let rec afterApplying (items : Items<_,_,_>) (symbol : Symbol<'Nonterminal, 'Terminal>) position : MyParserState<_,_,_> =
    if position >= String.length string || items.IsEmpty then Set.empty
    else
        let closure = closure productions items
        let gotoItems = goto symbol items closure productions
        let nextItems = beforeApplying gotoItems position
        let mutable result : MyParserState<_,_,_> = Set.empty
        let mutable recursionItems : MyParserState<_,_,_> = Set.empty
        Set.iter (fun (a, b) -> 
            let pop = pop a
            if items.Contains pop then result <- result.Add (pop, b)
            if closure.Contains pop then recursionItems <- recursionItems.Add (a, b)) nextItems
        Set.iter (fun (a, b) -> result <- Set.union result (afterApplying items (lhs a) b)) recursionItems
        result
and beforeApplying (items : Items<_,_,_>) position : MyParserState<_,_,_> =
    if position >= String.length string || items.IsEmpty then Set.empty
    else
        let closure = closure productions items
        let mutable result : MyParserState<_,_,_> = Set.empty
        if position < String.length string - 1 then
            result <- afterApplying items (Symbol.Terminal string.[position + 1]) (position + 1)
        result <- Set.union result (findFinalItems items position)
        let firstNonterminals = findEpsilonNonterminals closure
        Set.iter (fun x -> result <- Set.union result (afterApplying items (Symbol.Nonterminal x) position)) firstNonterminals
        result
                

productions <- productions.Add ('S', [|[|Symbol.Terminal '1'; Symbol.Nonterminal 'S'; Symbol.Terminal '0'|]; [||]|])
productions <- productions.Add ('A', [|[|Symbol.Nonterminal 'S'|]|])
let startItem = {
                    Nonterminal = 'A';
                    Production = (productions.Item 'A').[0];
                    Position = GenericZero;
                    Lookahead = (); }
let mutable items : Items<'Nonterminal, 'Terminal, 'Lookahead> = Set.empty
items <- items.Add startItem
printfn "%A" (beforeApplying items -1)