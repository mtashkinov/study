(* Tashkinov Mikhail (c) 2014
   Chat with bot
*)
module Chat
    open System.Windows.Forms
    open System
    open Bot


    let height = 110
    let width = 500
    let btHeight = 25
    let btWidth = 80
    let delt = 10
    let btHDelt = 45
    let btWDelt = 25

    let mutable history = ""

    let showHist() =
        let frm = new Form()
        do frm.Size <- new System.Drawing.Size(width, 2 * height)
        let tb = new RichTextBox(Text = history, Left = delt, Top = delt, Height = 2 * height - btWidth - delt)
        do tb.Width <- width - btWDelt - delt
        let btOk = new Button(Text = "Ok", Top = 2 * height - btHDelt - btHeight, Left = width / 2 - btWidth / 2)
        btOk.Click.Add (fun _ -> frm.Close())
        tb.KeyDown.Add(fun _ -> frm.Close())
        frm.Controls.AddRange [| tb; btOk; |]
        frm.Show()

    let answer s =
        let frm = new Form()
        do frm.Size <- new System.Drawing.Size(width, height)
        let text = answerBot s
        history <- history + "Bot:\n  " + text + "\n"
        let tb = new Label(Text = text, Left = delt, Top = delt)
        do tb.Width <- width - btWDelt - delt
        let btOk = new Button(Text = "Ok", Top = height - btHDelt - btHeight, Left = width / 2 - btWidth / 2)
        btOk.Click.Add (fun _ -> frm.Close())
        btOk.KeyDown.Add(fun _ -> frm.Close())
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
        let btHistory = new Button(Text = "History", Top = height - btHDelt - btHeight, Left = (width - btWidth) / 2)
        let answ() =
            let s = tb.Text
            history <- history + "You:\n  " + s + "\n"
            tb.Clear()
            answer s
        let close() =
            frm.Close()
            Application.Exit()
        btClose.Click.Add (fun _ -> close())
        btSend.Click.Add (fun _ -> answ())
        btHistory.Click.Add (fun _ -> showHist())
        tb.KeyDown.Add(fun x -> if x.KeyCode = Keys.Escape then close())
        btHistory.KeyDown.Add(fun x -> if x.KeyCode = Keys.Escape then close())
        tb.KeyDown.Add(fun x -> if x.KeyCode = Keys.Enter then answ())
        frm.Controls.AddRange [| tb; btClose; btSend; btHistory |]
        frm.Show()

    [<EntryPoint>]
    send
    Application.Run()