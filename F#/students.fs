module Students

open System

let defMood = 5
let defKnowledge = 2

type Human(name:string) =
    let mutable mood = defMood
    member this.Mood = mood
    member this.Name = name
    member this.SetMood(x:int) = mood <- x
    member this.ChangeMood(x:int) = if (mood + x >= 0) && (mood + x <= 10) then mood <- mood + x

type Teacher(name:string) =
    inherit Human(name)
    member this.Relax() = this.ChangeMood(1)
    member this.Work() = this.ChangeMood(-1)

type Student(name:string) =
    inherit Human(name)
    let mutable knowledge = defKnowledge
    member this.Knowledge = knowledge
    member this.SetKnowledge(x:int) = knowledge <- x
    member this.ChangeKnowledge(x:int) = if knowledge + x >= 0 then knowledge <- knowledge + x
    member this.Study() = if this.Mood > 0 then this.ChangeKnowledge(1)
                                                this.ChangeMood(-1)
    member this.Relax() = this.ChangeMood(1)
                          this.ChangeKnowledge(-1)
    member this.TryPassExam(t:Teacher) = if this.Mood + t.Mood + 2 * knowledge > 20 then this.ChangeMood(5)
                                                                                         this.ChangeKnowledge(1)
                                         else this.ChangeMood(-3)
    member this.Help(st:Student) = if (st.Mood >= 5) && (this.Mood >= 5) then st.ChangeMood(1)
                                                                              st.ChangeKnowledge(2)

type Matriculant(name:string) =
    inherit Human(name)
    member this.GoToUniversity() = let x = this.Mood
                                   let y = new Student(name)
                                   y.SetMood(x)
                                   y

type BadStudent(name:string) =
    inherit Student(name)
    member this.RelaxHard() = this.ChangeMood(2)
                              this.ChangeKnowledge(-3)
    member this.LeaveUniversity() = let m = new Matriculant(name)
                                    let x = this.Mood
                                    m.SetMood(x)
                                    m

type GoodStudent(name:string) =
    inherit Student(name)
    member this.StudyHard() = if this.Mood > 0 then this.ChangeMood(-2)
                                                    this.ChangeKnowledge(3)

// :?> don't work
let CopyInfo (x:Student) (y:Student) = y.SetMood(x.Mood)
                                       y.SetKnowledge(x.Knowledge)

let m1 = new Matriculant("Vasya")
let m2 = new Matriculant("Petya")
let t = new Teacher("Victor")

let s1 = m1.GoToUniversity()
let s2 = m2.GoToUniversity()

s1.Study()
s1.Study()
s1.Study()
let st1 = new GoodStudent(s1.Name)
CopyInfo s1 st1
st1.StudyHard()

s2.Relax()
s2.Relax()
s2.Relax()
let st2 = new BadStudent(s2.Name)
CopyInfo s2 st2
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
let m = st2.LeaveUniversity()