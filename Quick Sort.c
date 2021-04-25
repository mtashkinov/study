/* Tashkinov Mikhail (c) 2013
   Quick sort
*/

#include <stdio.h>
#include <time.h>

FILE * input;
FILE * output;

void QuickSort(int * mas, int first, int last)
{
  int x = mas[(first+last) / 2];
  int left = first, right = last;
  int temp;
  
  while (left < right)
  {
    while (mas[left] < x)
    {
      ++left;
    }
    while (mas[right] > x)
    {
      --right;
    }
    if (left <= right)
    {
      temp = mas[right];
      mas[right] = mas[left];
      mas[left] = temp;
      ++left;
      --right;
    }
  }
  if (first < right)
  {
    QuickSort(mas, first, right);
  }
  if (last > left)
  {
    QuickSort(mas, left, last);
  }
}

int main()
{
  int n, i, timeBegin, timeEnd, time = 0;
  int mas[200000];
  
  input = fopen("input.txt", "r");
  output = fopen("output.txt", "w");
  fscanf(input, "%d", &n);
  for (i = 0; i < n; ++i)
  {
    fscanf(input, "%d", &mas[i]);
  }
  timeBegin = clock();
  QuickSort(mas, 0, n - 1);
  timeEnd = clock();
  time += timeEnd - timeBegin;
  for (i = 0; i < n; ++i)
  {
    fprintf(output, "%d ", mas[i]);
  }
  fprintf(output, "\n%d", time);
  fclose(input);
  fclose(output);
  return 0;
}