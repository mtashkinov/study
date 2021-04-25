/* Tashkinov Mikhail (c) 2013
   n is prime?
*/
#include <stdio.h>
#include <math.h>

int isPrime(int n)
{
  int count = 0;
  int i = 1;
  
  for (i; i <= sqrt((float)n); ++i)
  {
    if (n % i == 0)
	  {
	    ++count;
    }
  }
  return (count == 1) && (n != 1);
}

int main()
{
  int n;
  
  scanf("%d", &n);
  printf("%s", isPrime(n) ? "Yes" : "No");
  return 0;
}