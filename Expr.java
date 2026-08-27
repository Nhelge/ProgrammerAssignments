public abstract class Expr {
}

class CstI extends Expr {
    final int i;

    CstI (int i) {
        this.i = i;
    }

    @Override
    public String toString() {
        return Integer.toString(i);
    }
}

class Var extends Expr {
    final String name;

    Var (String name) {
        this.name = name;
    }

    @Override
    public String toString(){
        return name;
    }
}

abstract class Binop extends Expr {
    final Expr e1, e2;

    Binop (Expr e1, Expr e2) {
        this.e1 = e1;
        this.e2 = e2;
    }
}

class Add extends Binop {
    Add(Expr e1, Expr e2) {
        super(e1, e2);
    }

    @Override
    public String toString(){
        return "(" + e1.toString() + " + " + e2.toString() + ")";
    }
}

class Sub extends Binop {
    Sub(Expr e1, Expr e2) {
        super(e1, e2);
    }

    @Override
    public String toString() {
        return "(" + e1.toString() + " - " + e2.toString() + ")";
    }
}

class Mul extends Binop {
    Mul(Expr e1, Expr e2){
        super(e1, e2);
    }

    @Override
    public String toString(){
        return "(" + e1.toString() + " * " + e2.toString() + ")";
    }
}