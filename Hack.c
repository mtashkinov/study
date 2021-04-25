/* Tashkinov Mikhail (c) 2013
   Shows the insecurity of function gets
*/

#include <stdio.h>
#include <string.h>
#include <malloc.h>

void Get_string(char * string)
{
  gets(string);
}

int main()
{
  char string[10];
  int n = 0;
  
  Get_string(string);
  if (n == 0)
  {
    printf("NO");
  }
  else
  {
    printf("YES");
  }
  return 0;
}