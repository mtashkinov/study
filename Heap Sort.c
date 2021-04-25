/* Tashkinov Mikhail (c) 2013
   Heap Sort
*/

#include <stdio.h>
#include <time.h>

FILE * input;
FILE * output;

void swap(int * first, int * second)
{
  int temp = * first;
  * first = * second;
  * second = temp;
}

void SiftDown(int * heap, int number, int size)
{
  int i = number;
  int childN, nMax = i, value = heap[i], isHeap = 0;
  
  while (!isHeap)
  {
    childN = 2 * i + 1;
    if ((childN < size) && (heap[childN] > value))
    {
      nMax = childN;
    }
    ++childN;
    if (((childN < size) && (heap[childN])) > heap[nMax])
    {
      nMax = childN;
    }
    if (nMax == i)
    {
      isHeap = 1;
    }
    if (!isHeap)
    {
      swap(&heap[i], &heap[nMax]);
      i = nMax;
    }
  }
}

void HeapSort(int * heap, int size)
{
  int i, n = size, firstElement;
  
  for (i = size / 2 - 1; i >= 0; --i)
  {
    SiftDown(heap, i, size);
  }
  while (n > 1)
  {
    --n;
    swap(&heap[0], &heap[n]);
    SiftDown(heap, 0, n);
  }
  
}

int main()
{
  int heap[200000];
  int n, i, timeBegin, timeEnd, time = 0;
  
  input = fopen("input.txt", "r");
  output = fopen("output.txt", "w");
  fscanf(input, "%d", &n);
  for (i = 0; i < n; ++i)
  {
     fscanf(input, "%d", &heap[i]);
  }
  timeBegin = clock();
  HeapSort(heap, n);
  timeEnd = clock();
  time += timeEnd-timeBegin;
  for (i = 0; i < n; ++i)
  {
    fprintf(output, "%d ", heap[i]);
  }
  fprintf(output, "\n%d", time);
  fclose(input);
  fclose(output);
  return 0;
}