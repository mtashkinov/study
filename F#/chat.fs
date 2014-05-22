(* Tashkinov Mikhail (c) 2014
   Chat with bot
*)

open System.Windows.Forms
open System


let height = 110
let width = 500
let btHeight = 25
let btWidth = 80
let delt = 10
let btHDelt = 45
let btWDelt = 25

let noKeyWord = 
    [
     "Я еще не очень разбираюсь в этом вопросе."
     "Вы удивляете меня своей способностью мыслить."
     "Можно поинтересоваться, откуда у Вас такие сведения?"
     "Мне кажется, Вы что-то от меня скрываете."
     "Я думаю, многие разделяют эту точку зрения."
     "Я не могу общаться с людьми, которые пытаются меня на чем-то подловить."
    ]
let keyWords =
    [
     "тебя зовут?"
     "дела?"
     "умеешь?"
     "знаешь?"
     "можешь?"
     "любишь?"
     "понимаешь?"
     "?"
     "Привет"
     "Здравствуй"
    ]
let phrases =
    [
     [
      "Меня никак не зовут."
      "Мне без разницы как вы будете меня звать."
     ]
     [
      "У меня всё хорошо, а у вас?"
      "Отлично. А ваши?"
     ]
     [
      "Я почти ничего не умею."
      "Мои возможности очень ограничены."
     ]
     [
      "К сожалению, я пока очень мало знаю..."
      "Мой кругозор очень ограничен."
     ]
     [
      "У меня очень скудные возможности."
     ]
     [
      "У меня нет ярко выраженных предпочтений."
     ]
     [
      "Я пока понимаю далеко не все, потому что у меня маленькая база."
      "Я пока понимаю очень не многое."
     ]
     [
      "У меня недостаточно знаний, чтобы ответить на Ваш вопрос."
      "Извините,  не могу ответить на Ваш вопрос."
     ]
     [
      "Привет."
      "Очень приятно Вас видеть."
     ]
     [
      "Здравствуйте."
     ]
    ]

let rec isKeyWord (l:string list) (s:string) =
    match l with
    | [] -> false
    | hd :: tl -> if (s.IndexOf(hd)) >= 0 then true
                  else isKeyWord tl s

let rec getEl l x  =
    if x = 1 then match l with
                  | hd :: _ -> hd
                  | [] -> failwith "Empty list"
    else match l with
         | _ :: tl -> getEl tl (x - 1)
         | [] -> failwith "No such element in the list"

let rec getKeyWord (s:string) (l:string list) x =
    match l with
    | [] -> -1
    | hd :: tl -> if (s.IndexOf(hd)) >= 0 then x
                  else getKeyWord s tl (x + 1)

let answerBot s =
    let rand = new Random()
    let n = getKeyWord s keyWords 1
    if n > 0 then let l = getEl phrases n
                  getEl l (rand.Next(1, l.Length + 1))
    else getEl noKeyWord (rand.Next(1, (noKeyWord.Length + 1)))

let answer s =
    let frm = new Form()
    do frm.Size <- new System.Drawing.Size(width, height)
    let tb = new Label(Text = answerBot s, Left = delt)
    do tb.Width <- width - btWDelt - delt
    let btOk = new Button(Text = "Ok", Top = height - btHDelt - btHeight, Left = width / 2 - btWidth / 2)
    btOk.Click.Add (fun _ -> frm.Close())
    btOk.KeyDown.Add(fun x -> if x.KeyCode = Keys.Enter then frm.Close())
    frm.Controls.AddRange [| tb; btOk; |]
    frm.Show()

let send =
    let frm = new Form()
    do frm.Size <- new System.Drawing.Size(width, height)
    let tb = new TextBox(Top = delt, Left = delt)
    do tb.Width <- width - btWDelt - delt
    let btSend = new Button(Text = "Send", Top = height - btHDelt - btHeight, Left = width - btWDelt - btWidth)
    do btSend.Size <- new System.Drawing.Size(btWidth, btHeight)
    let btClose = new Button(Text = "Close", Top = height - btHDelt - btHeight, Left = delt)
    let answ() =
        let s = tb.Text
        tb.Clear()
        answer s
    let close() =
        frm.Close()
        Application.Exit()
    btClose.Click.Add (fun _ -> close())
    btSend.Click.Add (fun _ -> answ())
    tb.KeyDown.Add(fun x -> if x.KeyCode = Keys.Escape then close())
    tb.KeyDown.Add(fun x -> if x.KeyCode = Keys.Enter then answ())
    frm.Controls.AddRange [| tb; btClose; btSend |]
    frm.Show()

[<EntryPoint>]
send
Application.Run()