/* Tashkinov Mikhail (c) 2013
   Bitmap Representation
*/

#include <stdio.h>

void BitmapRepresentation(int n)
{
  int i = 0, bitinint = sizeof(int) * 8;
  int temp[sizeof(int) * 8];
  
  if (n == 0)
  {
    printf("%d", 0);
  }
  else
  {
    for (i = 0; i < bitinint; ++i)
    {
      temp[i] = n & 1;
      n >>= 1;
    }
  
    i = bitinint - 1;
    while ((i >= 0)  && (temp[i] == 0))
    {
      --i;
    }
    while (i >= 0)
    {
      printf("%d", temp[i]);
      --i;
    }
  }
}

int main()
{
  int n;
  
  scanf("%d", &n);
  BitmapRepresentation(n);
  return 0;
}