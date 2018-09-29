
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
    | NotTrue _ -> [r]
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

[<EntryPoint>]
let main _ =
    let validPeople = allPossibilities () |> Seq.filter notForbidden
    printfn "%i" <| Seq.length validPeople
    0
        