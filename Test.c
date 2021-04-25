/* Tashkinon Mikhail (c) 2013
   Here I test some features
*/

#include <stdio.h>
#include <time.h>

int main()
{
  long int i;
  int mas[1000000000];
  
  for (i = 0; i < 100000; ++i)
  {
    mas[(i*i+1000000)%1000000000] = 0;
  }
  printf("%d", clock())
  return 0;
}