namespace MedvedFS

open System

[<assembly: CLSCompliant(true)>] 
do()
type MedvedFSharp() =
    inherit MedvedVBasic.MedvedVB()
    override x.MeetMedved() =
        printfn "Preved from F#"
        base.MeetMedved()
