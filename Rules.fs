module Rules

open Model

let rules = [
    IsTrue (Woman MadamNatsiou, Wearing Red)
    IsTrue (Woman BaronessFinch, Place FarLeft)
    IsTrue (Place Left, Wearing Purple)
    LeftOf (Wearing White, Wearing Green)
    IsTrue (Wearing White, Drinking Absinthe)
    IsTrue (From Dabokva, Wearing Blue)
    NextTo (Owns Ring, From Dabokva)

    IsTrue (Woman LadyWinslow, Owns WarMedal)
    NotTrue (Woman LadyWinslow, From Fraeport)
    IsTrue (From Fraeport, Owns BirdPendant)
    NextTo (From Dunwall, Owns Diamond)
    NextTo (From Dunwall, Drinking Rum)
    IsTrue (Woman CountessContee, Drinking Beer)
    IsTrue (From Karnaca, Drinking Wine)
    IsTrue (Place Centre, Drinking Whiskey)
    IsTrue (Woman DoctorMarcolla, From Baleton)    
]