/* Tashkinov Mikhail (c) 2013
   a^n
*/
#include <stdio.h>

int Power(int a, int n)
{
  int Pow;
  
  if (n == 0)
  {
    Pow = 1;
  }
  else
  {
    Pow = Power(a, n/2);
    Pow *= Pow;
    if (n%2 == 1)
  	{
	    Pow *= a;
	  }
  }
  return(Pow);
}

int main()
{
  int a;
  int n;
  
  scanf("%d%d", &a, &n);
  printf("%d", Power(a, n));
  return 0;
}