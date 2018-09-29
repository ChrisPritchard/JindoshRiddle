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

let neighbourRules = [
    LeftOf (Wearing Red, Wearing Blue)
    NextTo (Owns BirdPendant, From Dunwall)
    NextTo (From Baleton, Owns SnuffTin)
    NextTo (From Baleton, Drinking Whiskey)
]