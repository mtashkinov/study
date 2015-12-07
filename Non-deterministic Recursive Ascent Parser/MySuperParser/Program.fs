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

/// Computes the LR(0) closure of a set of items.
let closure (productions : Map<'Nonterminal, Symbol<'Nonterminal, 'Terminal>[][]>) items =
    // Call the recursive implementation, starting with the specified initial item set.
    closureImpl productions Set.empty (Set.toList items)

/// Moves the 'dot' (the current parser position) past the
/// specified symbol for each item in a set of items.
let goto symbol items (productions : Map<'Nonterminal, Symbol<'Nonterminal, 'Terminal>[][]>) : Items<_,_,_> =
    (Set.empty, items)
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
    // Return the closure of the item set.
    |> closure productions


let final (item : Item<_,_,_>) : bool =
    item.CurrentSymbol = None

let lhs (item : Item<_,_,_>) : 'Nonterminal =
    item.Nonterminal

let pop (item : Item<_,_,_>) : Item<_,_,_> =
    { item with
        Position = item.Position - 1<_>;
        Lookahead = item.CurrentSymbol; }

let first (item : Item<_,_,_>) : bool =
    item.Position = 0<_>

let string : string = "aaaa"
let productions : Map<'Nonterminal, Symbol<'Nonterminal, 'Terminal>[][]> = Map.empty

let findNonterminalsFromStartItems (items : Items<_,_,_>) : Set<'Nonterminal> =
    let mutable nonterminals : Set<'Nonterminal> = Set.empty
    Set.iter (fun x -> if first x then nonterminals <- nonterminals.Add x.Nonterminal) items
    nonterminals

let findFinalItems (items : Items<_,_,_>) position : MyParserState<_,_,_> =
    let mutable finalItems : MyParserState<_,_,_> = Set.empty
    Set.iter (fun x -> if final x then finalItems <- finalItems.Add (x, position)) items
    finalItems

let afterApplying (items : Items<_,_,_>) (symbol : Symbol<'Nonterminal, 'Terminal>) position : MyParserState<_,_,_> =
    state

let beforeApplying (items : Items<_,_,_>) position : MyParserState<_,_,_> =
    let closure = closure productions items
    let mutable result : MyParserState<_,_,_> = Set.union (afterApplying items (Symbol.Terminal string.[position]) (position + 1)) (findFinalItems items position)
    let firstNonterminals = findNonterminalsFromStartItems closure
    Set.iter (fun x -> result <- Set.union result (afterApplying items (Symbol.Nonterminal x) position)) firstNonterminals
    result


[<EntryPoint>]
let main argv = 
    0
