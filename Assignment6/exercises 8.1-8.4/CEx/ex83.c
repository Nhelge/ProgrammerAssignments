// Exercise 8.3 -- test of preincrement ++e and predecrement --e

void main() {
  int i;
  int arr[4];
  i = 0;
  arr[0] = 10; arr[1] = 20; arr[2] = 30; arr[3] = 40;

  print ++i;        // 1
  print --i;        // 0
  print ++i;        // 1
  println;

  ++arr[++i];       // i becomes 2, then arr[2] becomes 31
  print i;          // 2
  print arr[2];     // 31
  println;

  --arr[--i];       // i becomes 1, then arr[1] becomes 19
  print i;          // 1
  print arr[0]; print arr[1]; print arr[2]; print arr[3];   // 10 19 31 40
  println;
}
