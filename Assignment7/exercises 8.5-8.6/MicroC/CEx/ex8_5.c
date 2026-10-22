// micro-C example for exercise 8.5: conditional expressions e1 ? e2 : e3

void main(int n) {
  int x;
  int y;
  print (n > 0 ? 1 : 0);
  print (n < 0 ? -1 : n == 0 ? 0 : 1);
  x = n == 5 ? 100 : 200;
  print x;
  y = 0;
  x = n > 0 ? (y = 1) : (y = 2);
  print y;
  println;
}
