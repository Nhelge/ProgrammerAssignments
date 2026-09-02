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
  | If of expr * expr * expr (* exercise 1.1.iv *)

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
    | Prim("min", e1, e2) -> (* exercise 1.1.i *)
        match eval e1 env, eval e2 env with
        | x, y -> if x > y then y else x
    | Prim ("max", e1, e2) -> (* exercise 1.1.i *)
        match eval e1 env, eval e2 env with
        | x, y -> if x > y then x else y
    | Prim ("==", e1, e2) -> (* exercise 1.1.i *)
        match eval e1 env, eval e2 env with
        | x, y -> if x = y then 1 else 0
    | Prim _            -> failwith "unknown primitive";;

(* exercise 1.1.iii *)
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
    | If (e1, e2, e3) -> (* exercise 1.1.v *)
        if eval2 e1 env <> 0 then eval2 e2 env else eval2 e3 env

let test = If(Var "a",CstI 11, CstI 22)
let evaltest = eval2 test env

let e4 = Prim("min", Var "a", Var "c")
let e5 = Prim("==", Var "b", CstI 3)
let e6 = Prim("max", CstI 3, CstI 5)

(* exercise 1.1.ii evaluating abstract syntax*)
let e1v  = eval e4 env;; (* expected: 3*)
let e2v1 = eval e5 env;; (* expected: 0*)
let e6eval = eval e6 env (* expected: 5*)
(* 
outcommented source code from the repo
let e2v2 = eval e2 [("a", 314)];;
let e3v  = eval e3 env;; 
*)

module exercise1_2 =
    (* exercise 1.2.i *)
    type aexpr =
    | CstI of int
    | Var of string
    | Add of aexpr * aexpr
    | Sub of aexpr * aexpr
    | Mul of aexpr * aexpr

    (* exercise 1.2.ii *)
    let s1 = Sub(Var "v", Add(Var "w", Var "z")) (* v - (x + z) *)
    let s2  = Mul(CstI 2, Sub(Var "v", Add(Var "w",Var "z"))) (* 2 * (v - (w + z)) *)
    let s3 = Add(Add(Add(Var "x", Var "y"), Var "z"), Var "v") (* x + y + z + v *)

    (* exercise 1.2.iii *)
    let rec fmt a =
        match a with
        | CstI x -> string x
        | Var x -> x
        | Add (a1, a2) -> "(" + fmt a1 + " + " + fmt a2 + ")"
        | Sub (a1, a2) -> "(" + fmt a1 + " - " + fmt a2 + ")"
        | Mul (a1, a2) -> "(" + fmt a1 + " * " + fmt a2 + ")"
    
    (* exercise 1.2.iv *)
    let rec simplify a =
        match a with
        | CstI x -> CstI x
        | Var x -> Var x
        | Add (a1, a2) ->
            match simplify a1, simplify a2 with
            | CstI 0, s-> s
            | s, CstI 0 -> s
            | s1, s2 -> Add (s1, s2)
        | Sub (a1, a2) ->
            match simplify a1, simplify a2 with
            | s, CstI 0 -> s
            | s1, s2 -> Sub (s1, s2)
        | Mul (a1, a2) ->
            match simplify a1, simplify a2 with
            | s, CstI 0 -> CstI 0
            | CstI 0, s -> CstI 0
            | s, CstI 1 -> s
            | CstI 1, s -> s
            | s1, s2 -> Mul (s1, s2)
    
    let t1 = Mul(Add(CstI 1, CstI 0), Add(Var "x", CstI 0))

    (* exercise 1.2.v *)
    let rec deriv a v =
        match a with
        | CstI _ -> CstI 0
        | Var x -> if x = v then CstI 1 else CstI 0
        | Add (a1, a2) -> Add (deriv a1 v, deriv a2 v)
        | Sub (a1, a2) -> Sub (deriv a1 v, deriv a2 v)
        | Mul (a1, a2) -> Add (Mul(deriv a1 v, a2), Mul(a1, deriv a2 v))