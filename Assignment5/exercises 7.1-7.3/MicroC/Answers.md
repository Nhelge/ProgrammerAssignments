### Exercise 7.1
The output from running `fromFile "CEx/ex01.c"`.
![CEx/ex01.c](image.png)
The above shows the abstract syntax tree. We can see a function called `main` which takes an integer `n`. Then there is some statements: `While`, `Print`, `Assign` and another `Print` statement. We see a `TypI` in the declaration of the function. There are multiple expressions, such as the the first print statement and the last print statement and the assignment of `n` at the end of the `while` loop.

### Exercise 7.2
The solutions can be found in the CEx-folder, and is named 'ex72x', where x is the part of exercise. 

#### Exercise 7.2 (iii)
See `CEx/ex72iii.c`. Running it with `run (fromFile "CEx/ex72iii.c") []` prints `1 4 2 0`.

`freq` must have at least `max+1` elements, because `histogram` writes to `freq[0]` up to `freq[max]`. If it is smaller, micro-C does no bounds checking, so the writes go past the end of the array and overwrite whatever is stored next to it in the store (e.g. other local variables). The program does not fail, it just gives wrong results.

### Exercise 7.3
The lexer (`CLex.fsl`) now recognises the keyword `for` as the token `FOR`, and the parser (`CPar.fsy`) has a new `for` rule in both `StmtM` and `StmtU`. No changes were needed in `Absyn.fs` or `Interp.fs`, because the parser turns

```c
for (e1; e2; e3) stmt
```

into the existing abstract syntax

```fsharp
Block [Stmt(Expr e1); Stmt(While(e2, Block [Stmt stmt; Stmt(Expr e3)]))]
```

The programs from 7.2 rewritten with for-loops are `CEx/ex73i.c`, `CEx/ex73ii.c` and `CEx/ex73iii.c`:

- `run (fromFile "CEx/ex73i.c") []` prints `37`
- `run (fromFile "CEx/ex73ii.c") [5]` prints `30`
- `run (fromFile "CEx/ex73iii.c") []` prints `1 4 2 0`
