void arrsum(int n, int arr[], int *sump) {
  int i;
  int sum;
  sum = 0;
  for (i = 0; i < n; i = i + 1)
    sum = sum + arr[i];
  *sump = sum;
}

void main() {
  int arr[4];
  arr[0] = 7;
  arr[1] = 13;
  arr[2] = 9;
  arr[3] = 8;

  int sum;
  arrsum(4, arr, &sum);
  print sum;
}
