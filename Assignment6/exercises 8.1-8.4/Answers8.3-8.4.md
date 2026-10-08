# Exercises 8.3 and 8.4

## 8.3

Absyn.fs: added PreInc of access and PreDec of access to expr (the abstract syntax from exercise 7.4).

CLex.fsl: added the tokens "++" (PREINC) and "--" (PREDEC), placed before '+' and '-'.

CPar.fsy: declared PREINC and PREDEC with the same precedence as NOT and AMP, and added the rules
PREINC Access -> PreInc $2 and PREDEC Access -> PreDec $2 to ExprNotAccess.

Comp.fs: added two cases to cExpr:

```
| PreInc acc -> cAccess acc varEnv funEnv @ [DUP; LDI; CSTI 1; ADD; STI]
| PreDec acc -> cAccess acc varEnv funEnv @ [DUP; LDI; CSTI 1; SUB; STI]
```

The address is computed once and duplicated, so e is only evaluated one time. DUP copies the address,
LDI loads the old value, CSTI 1 and ADD/SUB compute the new value, and STI stores it and leaves the
new value on the stack as the result of the expression.

Test program CEx/ex83.c (includes ++arr[++i] and --arr[--i]):

```
dotnet build parse.fsproj
javac Machine.java
dotnet run --project microcc.fsproj CEx/ex83.c
java Machine CEx/ex83.out
```

Output:

```
1 0 1
2 31
1 10 19 31 40
```

++arr[++i] increments i once (0 -> 1 -> 2) and only arr[2] changes (30 -> 31). After --arr[--i], i is 1
and only arr[1] changes (20 -> 19). So i and the array elements have the right values afterwards.

## 8.4

ex8.c, symbolic bytecode (i is the local at offset 0):

```
        LDARGS 0; CALL (0, L1); STOP
L1:     INCSP 1                                           // int i;
        GETBP; CSTI 0; ADD; CSTI 20000000; STI; INCSP -1  // i = 20000000;
        GOTO L3
L2:     GETBP; CSTI 0; ADD; GETBP; CSTI 0; ADD; LDI;
        CSTI 1; SUB; STI; INCSP -1                        // i = i - 1;
        INCSP 0                                           // end of block
L3:     GETBP; CSTI 0; ADD; LDI                           // i
        IFNZRO L2                                         // while (i)
        INCSP -1; RET -1
```

The handwritten prog1:

```
        CSTI 20000000
        GOTO L2
L1:     CSTI 1; SUB
L2:     DUP; IFNZRO L1
        STOP
```

prog1 keeps i on top of the stack, so each iteration is only 4 instructions (CSTI 1, SUB, DUP, IFNZRO).
The compiled ex8 keeps i in the stack frame, so every use of i must compute its address
(GETBP; CSTI 0; ADD) and then load (LDI) or store (STI). The assignment's value is also thrown away
(INCSP -1) and the block adds a useless INCSP 0. That gives 17 instructions per iteration instead of 4.
When we ran them, ex8 took about 0.45 s and prog1 about 0.12 s.

ex13.c, symbolic bytecode (n is at offset 0, y at offset 1):

```
        LDARGS 1; CALL (1, L1); STOP
L1:     INCSP 1                                                // int y;
        GETBP; CSTI 1; ADD; CSTI 1889; STI; INCSP -1           // y = 1889;
        GOTO L3
L2:     GETBP; CSTI 1; ADD; GETBP; CSTI 1; ADD; LDI;
        CSTI 1; ADD; STI; INCSP -1                             // y = y + 1;
        GETBP; CSTI 1; ADD; LDI; CSTI 4; MOD; CSTI 0; EQ       // y % 4 == 0
        IFZERO L7                                              // && : false -> L7
        GETBP; CSTI 1; ADD; LDI; CSTI 100; MOD; CSTI 0; EQ; NOT  // y % 100 != 0
        IFNZRO L9                                              // || : true -> L9
        GETBP; CSTI 1; ADD; LDI; CSTI 400; MOD; CSTI 0; EQ     // y % 400 == 0
        GOTO L8
L9:     CSTI 1                                                 // || is true
L8:     GOTO L6
L7:     CSTI 0                                                 // && is false
L6:     IFZERO L4                                              // if (...)
        GETBP; CSTI 1; ADD; LDI; PRINTI; INCSP -1              // print y;
        GOTO L5
L4:     INCSP 0                                                // empty else branch
L5:     INCSP 0                                                // end of while body
L3:     GETBP; CSTI 1; ADD; LDI; GETBP; CSTI 0; ADD; LDI; LT   // y < n
        IFNZRO L2                                              // while (y < n)
        INCSP -1; RET 0
```

The while loop works like in ex8: jump to the test at L3, and jump back to L2 while the condition is true.

The && and || are compiled as ordinary expressions, so they first push a value (CSTI 0 or CSTI 1), and
then the if tests that value again with IFZERO L4. This gives jumps to jumps. For example, when
y % 100 != 0 is true, the code jumps to L9, pushes 1, jumps to L8, jumps again to L6, and then IFZERO
tests the 1 it just pushed, when it could have jumped straight to the print. When y % 4 == 0 is false,
it jumps to L7, pushes 0, and IFZERO then jumps to L4, when it could have jumped to L4 directly. There
are also useless INCSP 0 instructions from the empty else branch and the block.
