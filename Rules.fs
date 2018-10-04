module Rules

open Model

let rules = [
    IsTrue (Woman MadamNatsiou, Wearing Purple)
    IsTrue (Woman CountessContee, Place FarLeft)
    IsTrue (Place Left, Wearing White)
    LeftOf (Wearing Red, Wearing Blue)
    IsTrue (Wearing Red, Drinking Beer)
    IsTrue (From Dunwall, Wearing Green)
    NextTo (Owns BirdPendant, From Dunwall)

    IsTrue (Woman DoctorMarcolla, Owns Ring)
    NotTrue (Woman DoctorMarcolla, From Dabokva)
    IsTrue (From Dabokva, Owns WarMedal)
    NextTo (From Baleton, Owns SnuffTin)
    NextTo (From Baleton, Drinking Whiskey)
    IsTrue (Woman LadyWinslow, Drinking Rum)
    IsTrue (From Fraeport, Drinking Absinthe)
    IsTrue (Place Centre, Drinking Wine)
    IsTrue (Woman BaronessFinch, From Karnaca)    
]