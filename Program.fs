open World

let noConflict dx dy = 
    dx.position <> dy.position
    && dx.woman <> dy.woman
    && dx.wearing <> dy.wearing
    && dx.from <> dy.from
    && dx.drinking <> dy.drinking
    && dx.owns <> dy.owns


let allPositions = [FarLeft;Left;Centre;Right;FarRight]
let allWomen = [LadyWinslow;DoctorMarcolla;CountessContee;MadamNatsiou;BaronessFinch]
let allColours = [Purple;White;Red;Blue;Green]
let allHeirlooms = [PrizedRing;BirdPendant;Diamond;WarMedal;SnuffTin]
let allDrinks = [Beer;Whiskey;Rum;Absinthe;Wine]
let allHomes = [Dunwall;Dobovka;Baleton;Fraeport;Karnaca]

let inverseRules = rules |> List.collect (fun r ->
    let getInverses list except map fact =
        list |> List.except [except] |> List.map (fun o -> NotTrue (map o, fact))
    match r with
    | NotTrue _ -> []
    | IsTrue (Place o, fact) -> getInverses allPositions o (Place) fact
    | IsTrue (Woman o, fact) -> getInverses allWomen o (Woman) fact
    | IsTrue (Wearing o, fact) -> getInverses allColours o (Wearing) fact
    | IsTrue (Owns o, fact) -> getInverses allHeirlooms o (Owns) fact
    | IsTrue (Drinking o, fact) -> getInverses allDrinks o (Drinking) fact
    | IsTrue (From o, fact) -> getInverses allHomes o (From) fact)

let allRules = rules @ inverseRules

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

let rec distinctSet group validPeople =
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
            distinctSet newGroup validPeople
        | None -> None

let rec ruleApplies rule group =
    let pairs = group |> Seq.sortBy (fun o -> o.position) |> Seq.pairwise
    match rule with
    | LeftOf (subject, target) ->
        pairs |> Seq.exists (fun (left, right) -> applies subject left && applies target right)
    | RightOf (subject, target) ->
        pairs |> Seq.exists (fun (left, right) -> applies subject right && applies target left)
    | NextTo (subject1, subject2) ->
        ruleApplies (LeftOf (subject1, subject2)) group
        || ruleApplies (RightOf (subject1, subject2)) group

let notInvalid group =
    Seq.forall (fun rule -> ruleApplies rule group) neighbourRules

[<EntryPoint>]
let main _ =
    let validPeople = 
        allPossibilities () 
        |> Seq.filter notForbidden
    let validSets = 
        validPeople 
        |> Seq.map (fun p -> 
            distinctSet [p] validPeople) 
        |> Seq.choose id
        |> Seq.filter notInvalid
    printfn "%i" <| Seq.length validSets
    0        