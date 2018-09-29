
type Position = FarLeft | Left | Centre | Right | FarRight
type Women = LadyWinslow | DoctorMarcolla | CountessContee | MadamNatsiou | BaronessFinch
type Colours = Purple | White | Red | Blue | Green
type Heirloom = PrizedRing | BirdPendant | Diamond | WarMedal | SnuffTin
type Drink = Beer | Whiskey | Rum | Absinthe | Wine
type HomeTown = Dunwall | Dobovka | Baleton | Fraeport | Karnaca

type Description = {
    position: Position
    woman: Women
    wearing: Colours
    from: HomeTown
    drinking: Drink
    owns: Heirloom
}

type Subject = Woman of Women | Place of Position | Owns of Heirloom | Drinking of Drink | Wearing of Colours | From of HomeTown
type FactRule = IsTrue of Subject * Subject | NotTrue of Subject * Subject
type PlaceMentRule = NextTo of Subject * Subject

let rules = [
    IsTrue (Woman MadamNatsiou, Wearing Purple)
    IsTrue (Woman CountessContee, Place FarLeft)
    NotTrue (Woman CountessContee, Wearing White)
    IsTrue (Place Left, Wearing White)
    NotTrue (Wearing Red, Place FarRight)
    NotTrue (Wearing Blue, Place FarLeft)
    IsTrue (Wearing Red, Drinking Beer)
    IsTrue (From Dunwall, Wearing Green)
    NotTrue (Owns BirdPendant, From Dunwall)
    IsTrue (Woman DoctorMarcolla, Owns PrizedRing)
    NotTrue (Woman DoctorMarcolla, From Dobovka)
    IsTrue (From Dobovka, Owns WarMedal)
    NotTrue (From Baleton, Owns SnuffTin)
    NotTrue (From Baleton, Drinking Whiskey)
    IsTrue (Woman LadyWinslow, Drinking Rum)
    IsTrue (From Fraeport, Drinking Absinthe)
    NotTrue (From Fraeport, Place Centre)
    IsTrue (Place Centre, Drinking Wine)
    IsTrue (Woman BaronessFinch, From Karnaca)
]

let placementRules = [
    NextTo (Wearing Red, Wearing Blue)
    NextTo (Owns BirdPendant, From Dunwall)
    NextTo (From Baleton, Owns SnuffTin)
    NextTo (From Baleton, Drinking Whiskey)
]

let allPositions = [FarLeft;Left;Centre;Right;FarRight]
let allWomen = [LadyWinslow;DoctorMarcolla;CountessContee;MadamNatsiou;BaronessFinch]
let allColours = [Purple;White;Red;Blue;Green]
let allHeirlooms = [PrizedRing;BirdPendant;Diamond;WarMedal;SnuffTin]
let allDrinks = [Beer;Whiskey;Rum;Absinthe;Wine]
let allHomes = [Dunwall;Dobovka;Baleton;Fraeport;Karnaca]

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
    Seq.forall (fun rule -> ruleDoesNotForbid rule description) rules

[<EntryPoint>]
let main argv =
    let valid = allPossibilities () |> Seq.filter notForbidden |> Seq.toList
    printfn "%i" <| List.length valid
    0