(* Tashkinov Mikhail 2014
   Regular Expression for expression
*)

module RegExpr

let isEmail s =
    let reg = "^[a-zA-Z_]([.]?[\w\-\d]){2,30}[a-zA-Z\d][@]([\w\d]{2,}[\.])+(([a-zA-Z]{2,4}|marketing|sales|support|abuse|security|postmaster|hostmaster|usenet|webmaster|museum))$"
    let x = new System.Text.RegularExpressions.Regex(reg)
    x.IsMatch(s)