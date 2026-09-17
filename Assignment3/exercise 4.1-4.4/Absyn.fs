(* Fun/Absyn.fs * Abstract syntax for micro-ML, a functional language *)

module Absyn

(* Exercise 4.3: Modified the abstract syntax in Absyn.fs to permit a list of parameter names in Letfun*)

type expr = 
  | CstI of int
  | CstB of bool
  | Var of string
  | Let of string * expr * expr
  | Prim of string * expr * expr
  | If of expr * expr * expr
  | Letfun of string * string list * expr * expr    (* (f, x, fBody, letBody) *)
  | Call of expr * expr list
