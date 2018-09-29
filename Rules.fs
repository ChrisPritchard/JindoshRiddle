module Rules

open Model

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
    IsTrue (Woman DoctorMarcolla, Owns Ring)
    NotTrue (Woman DoctorMarcolla, From Dabokva)
    IsTrue (From Dabokva, Owns WarMedal)
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