module Collections

open Model

let allPositions = [FarLeft;Left;Centre;Right;FarRight]
let allWomen = [LadyWinslow;DoctorMarcolla;CountessContee;MadamNatsiou;BaronessFinch]
let allColours = [Purple;White;Red;Blue;Green]
let allHeirlooms = [Ring;BirdPendant;Diamond;WarMedal;SnuffTin]
let allDrinks = [Beer;Whiskey;Rum;Absinthe;Wine]
let allHomes = [Dunwall;Dabokva;Baleton;Fraeport;Karnaca]

let allPossibilities () = 
    [
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
    ]

let noConflict dx dy = 
    dx.position <> dy.position
    && dx.woman <> dy.woman
    && dx.wearing <> dy.wearing
    && dx.from <> dy.from
    && dx.drinking <> dy.drinking
    && dx.owns <> dy.owns

let rec distinctGroups group people =
    [
        if Set.count group = 5 then 
            yield group
        else 
            yield! 
                people 
                    |> List.filter (fun p -> Seq.forall (noConflict p) group)
                    |> List.collect (fun p -> 
                        distinctGroups (Set.add p group) people)
    ]