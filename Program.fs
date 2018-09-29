
type Women = LadyWinslow | DoctorMarcolla | CountessContee | MadamNatsiou | BaronessFinch
type Position = FarLeft | Left | Centre | Right | FarRight
type Heirloom = PrizedRing | BirdPendant | Diamond | WarMedal | SnuffTin
type Drink = Beer | Whiskey | Rum | Absinthe | Wine
type Colours = Purple | White | Red | Blue | Green
type HomeTown = Dunwall | Dobovka | Baleton | Fraeport | Karnaca

type Subject = Woman of Women | Place of Position | Owns of Heirloom | Drinking of Drink | Wearing of Colours | From of HomeTown
type Rules = IsTrue of Subject * Subject | NotTrue of Subject * Subject | NextTo of Subject * Subject

let rules = [
    IsTrue (Woman MadamNatsiou, Wearing Purple)
    IsTrue (Woman CountessContee, Place FarLeft)
    IsTrue (Place Left, Wearing White)
    NextTo (Wearing Red, Wearing Blue)
    NotTrue (Wearing Red, Place FarRight)
    NotTrue (Wearing Blue, Place FarLeft)
    IsTrue (Wearing Red, Drinking Beer)
    IsTrue (HomeTown Dunwall, Wearing Green)
    NotTrue (Owns BirdPendant, HomeTown Dunwall)
    IsTrue (Woman DoctorMarcolla, Owns PrizedRing)
    NotTrue (Womam DoctorMarcolla, HomeTown Dobovka)
    IsTrue (HomeTown Dobovka, Owns WarMedal)
    NotTrue (HomeTown Baleton, Owns SnuffTin)
    NotTrue (HomeTown Baleton, Drinking Whiskey)
    IsTrue (Woman LadyWinslow, Drinking Rum)
    IsTrue (HomeTown Fraeport, Drinking Absinthe)
    NotTrue (HomeTown Fraeport, Position Centre)
    IsTrue (Position Centre, Drinking Wine)
    IsTrue (BaronessFinch, HomeTown Karnaca)
]

let allWomen = [LadyWinslow;DoctorMarcolla;CountessContee;MadamNatsiou;BaronessFinch]
let allPositions = [FarLeft;Left;Centre;Right;FarRight]
let allHeirlooms = [PrizedRing;BirdPendant;Diamond;WarMedal;SnuffTin]
let allDrinks = [Beer;Whiskey;Rum;Absinthe;Wine]
let allColours = [Purple;White;Red;Blue;Green]
let allHomes = [Dunwall;Dobovka;Baleton;Fraeport;Karnaca]

[<EntryPoint>]
let main argv =
    
    0