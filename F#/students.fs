(* Tashkinov Mikhail (c) 2014
   OOP example
*)

module Students

open System

let defMood = 5
let defKnowledge = 2

type Human(name:string) =
    let mutable mood = defMood
    member this.Mood with get() = mood and set(x) = mood <- x
    member this.Name = name
    member this.ChangeMood(x:int) = if (mood + x >= 0) && (mood + x <= 10) then mood <- mood + x

type Teacher(name:string) =
    inherit Human(name)
    member this.Relax() =
        this.ChangeMood(1)
        printfn "Teacher %A relaxed and now his mood = %A" name this.Mood

    member this.Work() = 
        this.ChangeMood(-1)
        printfn "Teacher %A worked and now his mood = %A" name this.Mood


type Student(name:string) =
    inherit Human(name)
    let CopyInfo (x:Student) (y:Student) = 
        y.Mood <- x.Mood
        y.Knowledge <- x.Knowledge
    let mutable knowledge = defKnowledge
    member this.Knowledge with get() = knowledge and set(x) = knowledge <- x
    member this.ChangeKnowledge(x:int) = if knowledge + x >= 0 then knowledge <- knowledge + x
    member this.Study() = 
        if this.Mood > 0 then this.ChangeKnowledge(1)
                              this.ChangeMood(-1)
        printfn "Student %A studied and now his mood = %A, knowledge = %A" name this.Mood knowledge

    member this.Relax() = 
        this.ChangeMood(1)
        this.ChangeKnowledge(-1)
        printfn "Student %A relaxed and now his mood = %A, knowledge = %A" name this.Mood knowledge

    member this.TryPassExam(t:Teacher) = 
        printf "Student %A tried pass exam" name
        if this.Mood + t.Mood + 2 * knowledge > 20 then 
            this.ChangeMood(5)
            this.ChangeKnowledge(1)
            printfn " and passed! Now his mood = %A, knowledge = %A" this.Mood this.Knowledge
        else 
            this.ChangeMood(-3)
            printfn " and failed! Now his mood = %A, knowledge = %A" this.Mood this.Knowledge

    member this.Help(st:Student) = 
        if (st.Mood >= 5) && (this.Mood >= 5) then 
            st.ChangeMood(1)
            st.ChangeKnowledge(2)
            printfn "Student %A helped student %A. And now %A mood = %A, knowledge = %A" name st.Name st.Name this.Mood this.Knowledge

    member this.BecomeGood() =
        printfn "Student %A becomes a good student!" name 
        let r = new GoodStudent(name)
        CopyInfo this r
        r

    member this.BecomeBad() =
        printfn "Student %A becomes a bad student!" name 
        let r = new BadStudent(name)
        CopyInfo this r
        r

and Matriculant(name:string) =
    inherit Human(name)
    member this.GoToUniversity() = 
        printfn "Matrifucant %A becomes a student!" name 
        let x = this.Mood
        let y = new Student(name)
        y.Mood <- x
        y

and BadStudent(name:string) =
    inherit Student(name)
    member this.RelaxHard() = 
        this.ChangeMood(2)
        this.ChangeKnowledge(-3)
        printfn "Bad student %A relaxed hard and now his mood = %A, knowledge = %A" name this.Mood this.Knowledge

    member this.LeaveUniversity() =
        printfn "Bad student %A leaves the University" name
        let m = new Matriculant(name)
        let x = this.Mood
        m.Mood <- x
        m

and GoodStudent(name:string) =
    inherit Student(name)
    member this.StudyHard() = 
        if this.Mood > 0 then 
            this.ChangeMood(-2)
            this.ChangeKnowledge(3)
            printfn "Good student %A sdudied hard and now his mood = %A, knowledge = %A" name this.Mood this.Knowledge

let life =
    let mutable st1 = new Matriculant("Vasya")
    let st2 = new Matriculant("Petya")
    let t = new Teacher("Victor")

    let st1 = st1.GoToUniversity()
    let st2 = st2.GoToUniversity()

    st1.Study()
    st1.Study()
    st1.Study()
    let st1 = st1.BecomeGood()
    st1.StudyHard()

    st2.Relax()
    st2.Relax()
    st2.Relax()
    let st2 = st2.BecomeBad()
    st2.RelaxHard()

    t.Work()
    t.Work()
    t.Work()

    st1.TryPassExam(t)
    st2.TryPassExam(t)

    st1.Relax()
    st1.Relax()
    st1.Relax()
    st1.StudyHard()

    st2.Study()
    st2.Study()
    st2.Study()
    st2.RelaxHard()

    t.Relax()
    t.Relax()
    t.Relax()

    st1.TryPassExam(t)
    st2.TryPassExam(t)

    st2.Relax()
    st2.Relax()

    st1.Help(st2)
    st1.Help(st2)
    st2.RelaxHard()

    st2.TryPassExam(t)
    let st2 = st2.LeaveUniversity()
    ()
life