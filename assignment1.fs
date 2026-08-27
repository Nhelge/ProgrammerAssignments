(* Programming language concepts for software developers, 2010-08-28 *)

(* Evaluating simple expressions with variables *)

module Intro2

(* Association lists map object language variables to their values *)

let env = [("a", 3); ("c", 78); ("baf", 666); ("b", 111)];;

let emptyenv = []; (* the empty environment *)

let rec lookup env x =
    match env with 
    | []        -> failwith (x + " not found")
    | (y, v)::r -> if x=y then v else lookup r x;;

let cvalue = lookup env "c";;


(* Object language expressions with variables *)

type expr = 
  | CstI of int
  | Var of string
  | Prim of string * expr * expr
  | If of expr * expr * expr

let e1 = CstI 17;;

let e2 = Prim("+", CstI 3, Var "a");;

let e3 = Prim("+", Prim("*", Var "b", CstI 9), Var "a");;


(* Evaluation within an environment *)

let rec eval e (env : (string * int) list) : int =
    match e with
    | CstI i            -> i
    | Var x             -> lookup env x 
    | Prim("+", e1, e2) -> eval e1 env + eval e2 env
    | Prim("*", e1, e2) -> eval e1 env * eval e2 env
    | Prim("-", e1, e2) -> eval e1 env - eval e2 env
    | Prim("min", e1, e2) -> 
        match eval e1 env, eval e2 env with
        | x, y -> if x > y then y else x
        | _ -> failwith "error"
    | Prim ("max", e1, e2) ->
        match eval e1 env, eval e2 env with
        | x, y -> if x > y then x else y
        | _ -> failwith "error"
    | Prim ("==", e1, e2) ->
        match eval e1 env, eval e2 env with
        | x, y -> if x = y then 1 else 0
        | _ -> failwith "error"
    | Prim _            -> failwith "unknown primitive";;

let rec eval2 e (env : (string * int) list) : int =
    match e with
    | CstI i -> i
    | Var x -> lookup env x
    | Prim (op, e1, e2) ->
        let i1 = eval2 e1 env
        let i2 = eval2 e2 env
        match op with
        | "+" -> i1 + i2
        | "-" -> i1 - i2
        | "*" -> i1 * i2
        | "min" -> if i1 > i2 then i2 else i1
        | "max" -> if i1 > i2 then i1 else i2
        | "==" -> if i1 = i2 then 1 else 0
        | _ -> failwith "unknown operator"
    | If (e1, e2, e3) ->
        if eval2 e1 env <> 0 then eval2 e2 env else eval2 e3 env

let test = If(Var "a",CstI 11, CstI 22)
let evaltest = eval2 test env

let e4 = Prim("min", Var "a", Var "c")
let e5 = Prim("==", Var "b", CstI 3)
let e6 = Prim("max", CstI 3, CstI 5)

let e1v  = eval e4 env;;
let e2v1 = eval e5 env;;
let e6eval = eval e6 env
(*let e2v2 = eval e2 [("a", 314)];;
let e3v  = eval e3 env;; *)
