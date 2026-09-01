# Assignments

## Assignment 1
### 1.1
**i**
Added the `max, min, ==` operators to the `eval` function. First match on the operator, then recursively match on each `expr` in order to compare them. Either return the `max, min` or `1` or `0` for the equal operator.

**ii**
We wrote three expressions using the abstract syntax and used our `eval` function to evaluate them. The expressions are derived from the `env` variable defined at the top of the file. All three expressions produce the expected output.

**iii**
Rewrote the original `eval` function to match on `Prim` first and from here match on a specific operator. The new function is called `eval2`.

**iv**
Extended our `expr` type to handle `If`statements composed of `expr * expr * expr`.

**v**
Extended our `eval2` function to handle the new `If` operator.

### 1.2
**i**
Created a new module in order to introduce the new type `aexpr` which also contained `Var` and `CstI`. Also added the operators `Add, Sub, Mul` to the type.

**ii**
Wrote representation of the three expressions:
`v - (x + z)`, `2 * (v - (w + z))`, `x + y + z + v`.

**iii**
Wrote the `fmt` function which formats the expressions as strings using recursion and pattern matching.

**iv**
Wrote `simplify` which simplifies expressions where possible e.g. `1 * 0 = 0` or `1 + 0 = 1` using recursion and nested pattern matching.

**v**
Wrote `deriv` which is a function used to find the derivative of an expression, also using recursion and pattern matching.

### 1.4
**i**
Designed a class hierarchy in Java to represent the arithemtic expressions we just worked with in the earlier exercises in F#. Created abstract classes `Expr` and `Binop` and created the `CstI, Var, Add, Sub` and `Mul` as we know from earlier which all either inherits from `Expr` or `Binop`.

**ii**
Created three more abstract expressions and had them printed in our `Test` class at the bottom of the file.

**iii**
Added an abstract function to our `Expr` class with the method signature `int eval(HashMap<String,Integer> env)` in order to evaluate arithmetic operations. Each `Expr` recursively calls their `eval` function in order to evaluate the expression.

**iv**
Added the abstract function `simplify` to our `Expr` class which does the same as the `simplify` function in exercise `1.2.iv`.

### 2.1
The `expr` type is extended so that `let` takes a list of `string * expr` bindings. `eval` was revised by using a fold, so we can walk through the list of bindings one at a time, and evaluate the right-hand side `erhs` accumulated so far, and adding the result `(x, xval)` to `accEnv`.

### 2.2
`freevars` is revised to handle the updated `expr` language. For each `(x, erhs)` binding we subtract the variable names form the binding that came before it. The free variables of `ebody` are found by subtracting all bound names from its free variables.

### 2.3
`tcomp` is revised to handle the updated `expr` language. `TLet` can only support a single binding, therefore we compile a list of bindings into nested `TLet`s. This is done by going through the bindings, compiling each right hand side built up so far, and then adding its name to that list, before going to the next binding. At last `ebody` is compiled using the final list of names. 

## Assignment 2
