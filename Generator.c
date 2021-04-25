/* Tashkinov Mikhail (c) 2013
   Array generator
*/

#include <stdio.h>
#include <stdlib.h>

FILE * f1;

int main()
{
  int i;
  
  f1 = fopen("input.txt", "w");
  fprintf(f1, "%d\n", 200000);
  for (i = 0; i <= 200000; ++i)
  {
    fprintf(f1, "%d ", rand());
  }
  return 0;
  fclose(f1);
}