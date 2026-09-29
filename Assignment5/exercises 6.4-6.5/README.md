### Exercise 6.4
i)
![first proof tree](image-1.png)
The type of `f` needs to be polymorphic as it does not constraint its parameter at all. `f f` can't be typed if `f` is forced to have a single type as it would create a self referential equation.  
ii)
![second proof tree](image.png)
`f` is monomorphic in this case because every single use of `f` is consistent with one type, `int -> int`. It can also be seen in the tree as once everything is resolved there are no free variables left.