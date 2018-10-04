module Model

type Position = FarLeft | Left | Centre | Right | FarRight
type Women = LadyWinslow | DoctorMarcolla | CountessContee | MadamNatsiou | BaronessFinch
type Colours = Purple | White | Red | Blue | Green
type Heirloom = Ring | BirdPendant | Diamond | WarMedal | SnuffTin
type Drink = Beer | Whiskey | Rum | Absinthe | Wine
type HomeTown = Dunwall | Dabokva | Baleton | Fraeport | Karnaca

type Description = {
    position: Position
    woman: Women
    wearing: Colours
    from: HomeTown
    drinking: Drink
    owns: Heirloom
}

type Testable = Person of Description | Group of seq<Description>

type Subject = 
    | Woman of Women | Place of Position | Owns of Heirloom 
    | Drinking of Drink | Wearing of Colours | From of HomeTown
type Rule = 
    | IsTrue of Subject * Subject | NotTrue of Subject * Subject 
    | NextTo of Subject * Subject | LeftOf of Subject * Subject | RightOf of Subject * Subject