open World

let noConflict dx dy = 
    dx.position <> dy.position
    && dx.woman <> dy.woman
    && dx.wearing <> dy.wearing
    && dx.from <> dy.from
    && dx.drinking <> dy.drinking
    && dx.owns <> dy.owns

let allPossibilities () = 
    seq {
        yield!
            [0..4] |> Seq.collect (fun p ->
            [0..4] |> Seq.collect (fun w -> 
            [0..4] |> Seq.collect (fun c ->
            [0..4] |> Seq.collect (fun f ->
            [0..4] |> Seq.collect (fun d ->
            [0..4] |> Seq.map (fun o ->
            {
                position = allPositions.[p]
                woman = allWomen.[w]
                wearing = allColours.[c]
                from = allHomes.[f]
                drinking = allDrinks.[d]
                owns = allHeirlooms.[o]
            }))))))
    }

let applies subject description =
    match subject with
    | Woman w -> description.woman = w
    | Place p -> description.position = p
    | Owns o -> description.owns = o
    | Drinking d -> description.drinking = d
    | Wearing c -> description.wearing = c
    | From h -> description.from = h

let ruleDoesNotForbid rule description = 
    match rule with
    | IsTrue (subject, fact) -> not (applies subject description) || applies fact description
    | NotTrue (subject, fact) -> not (applies subject description) || not (applies fact description)

let notForbidden description =
    Seq.forall (fun rule -> ruleDoesNotForbid rule description) allRules

let rec addToGroup group validPeople =
    if Seq.length group = 5 then Some group
    else
        let valid = validPeople |> Seq.filter (fun p -> 
            Seq.forall (noConflict p) group) |> Seq.tryHead
        match valid with
        | Some p -> 
            let newGroup = 
                seq {
                    yield! group
                    yield p
                }
            addToGroup newGroup validPeople
        | None -> None

[<EntryPoint>]
let main _ =
    let validPeople = allPossibilities () |> Seq.filter notForbidden
    let validSets = 
        validPeople 
        |> Seq.map (fun p -> 
            addToGroup [p] validPeople) 
        |> Seq.choose id
    printfn "%i" <| Seq.length validSets
    0        