void histogram(int n, int ns[], int max, int freq[]) {
  int i;
  for (i = 0; i <= max; i = i + 1)
    freq[i] = 0;
  for (i = 0; i < n; i = i + 1)
    freq[ns[i]] = freq[ns[i]] + 1;
}

void main() {
  int arr[7];
  arr[0] = 1;
  arr[1] = 2;
  arr[2] = 1;
  arr[3] = 1;
  arr[4] = 1;
  arr[5] = 2;
  arr[6] = 0;

  int freq[4];
  histogram(7, arr, 3, freq);

  int i;
  for (i = 0; i < 4; i = i + 1)
    print freq[i];
}
