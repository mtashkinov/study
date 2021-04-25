/* Tashkinov Mikhail (c) 2013
   Bubble sort
*/

#include <stdio.h>
#include <time.h>

FILE * input;
FILE * output;

void BubbleSort(int * mas, int size)
{
  int i, j, temp;
  
  for (i = size - 1; i > 0; --i)
  {
    for (j = 0; j < i; ++j)
    {
      if (mas[j] > mas[j + 1])
      {
        temp = mas[j];
        mas[j] = mas[j + 1];
        mas[j + 1] = temp;
      }
    }
  }
}

int main()
{
  int i, mas[50000], n;
  int clockBegin, clockEnd, time = 0;
  
  input = fopen("input.txt", "r");
  output = fopen("output.txt", "w");
  fscanf(input, "%d", &n);
  for (i = 0; i < n; ++i)
  {
    fscanf(input, "%d", &mas[i]);
  }
  clockBegin = clock();
  BubbleSort(mas, n);
  clockEnd = clock();
  time += clockEnd-clockBegin;
  for (i = 0; i < n; ++i)
  {
    fprintf(output, "%d ", mas[i]);
  }
  fprintf(output, "\n%d", time);
  fclose(input);
  fclose(output);
  return 0;
}