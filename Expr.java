import java.util.HashMap;

// exercise 1.4

public abstract class Expr {
    abstract int eval(HashMap<String,Integer> env);
    abstract Expr simplify();
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

    int eval(HashMap<String,Integer> env){
        return i;
    }

    Expr simplify(){
        return new CstI(i);
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

    int eval(HashMap<String,Integer> env){
        return env.get(name);
    }

    Expr simplify(){
        return new Var(name);
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

    int eval(HashMap<String,Integer> env){
        return e1.eval(env) + e2.eval(env);
    }

    Expr simplify(){
        Expr v = e1.simplify();
        Expr h = e2.simplify();

        if (v instanceof CstI x && x.i == 0){
            return h;
        } else if (h instanceof CstI y && y.i == 0){
            return v;
        } else {
            return new Add(v,h);
        }
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

    int eval(HashMap<String,Integer> env){
        return e1.eval(env) - e2.eval(env);
    }

    Expr simplify(){
        Expr v = e1.simplify();
        Expr h = e2.simplify();

        if (h instanceof CstI y && y.i == 0){
            return v;
        } else {
            return new Sub(v,h);
        }
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

    int eval(HashMap<String,Integer> env){
        return e1.eval(env) * e2.eval(env);
    }

    Expr simplify(){
        Expr v = e1.simplify();
        Expr h = e2.simplify();

        if (v instanceof CstI x && x.i == 0){
            return new CstI(0);
        } else if (v instanceof CstI x && x.i == 1){
            return h;
        } else if (h instanceof CstI y && y.i == 0){
            return new CstI(0);
        } else if (h instanceof CstI y && y.i == 1){
            return v;
        } else {
            return new Mul(v,h);
        }
    }
}

class Test{
    public static void main (String [] args){
        Expr e1 = new Add(new CstI(5), new CstI(8));
        Expr e2 = new Mul(new CstI(6), new Var("b"));
        Expr e3 = new Sub(new CstI(9), new Mul(new Var("a"), new CstI(3)));

        Expr e4 = new Mul(new Add(new CstI(1), new CstI(0)), new Add(new Var("x"), new CstI(0)));

        System.out.println(e1);
        System.out.println(e2);
        System.out.println(e3);
        System.out.println(e4.simplify());
    }
}