module World

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
type NeighbourRule = NextTo of Subject * Subject | LeftOf of Subject * Subject | RightOf of Subject * Subject

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

let neighbourRules = [
    LeftOf (Wearing Red, Wearing Blue)
    RightOf (Wearing Blue, Wearing Red)
    NextTo (Owns BirdPendant, From Dunwall)
    NextTo (From Baleton, Drinking Whiskey)
]