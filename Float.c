/* Tashkinov Mikhail (c) 2013
   Float -> sign * mantis * exp
*/

#include <stdio.h>
#include <math.h>

void PrintUpLine(int sign, int * mantis, int exponent, int mSize)
{
  int spaceNumber;
  int i;
  
  printf("    %d  ", sign); 
  
  if ((mSize == 1) && mantis[0] == 0)
  {
    spaceNumber = mSize;
  }
  else
  {
    spaceNumber = mSize + 2;
  }

  for (i = 0; i < spaceNumber; ++i)
  {
    printf(" ");
  }
      
  if (exponent == 0)
  {
    printf("    %d\n", exponent - 126);
  }
  else
  {
    printf("    %d\n", exponent - 127);
  }
}

void PrintLowLine(int * mantis, int exponent, int mSize)
{
  int i;
  if (exponent == 0)
  { 
    printf("(-1) * 0.");
    for (i = 0; i < mSize; ++i)
    {
      printf("%d", mantis[i]);
    }
  }
  else
  {
    if ((mSize == 1) && mantis[0] == 0)
    {
      printf("(-1) * 1");
    }
    else
    {
      printf("(-1) * 1.");
      for (i = 0; i < mSize; ++i)
      {
        printf("%d", mantis[i]);
      }
    }
  }      
  printf(" * 2\n");
}

void PrintFloat(int sign, int * mantis, int exponent, int mSize)
{
  int i;
  
  if ((exponent == 0) && (mSize = 1) && (mantis[0] == 0))
  {
    printf("%c0\n", (sign == 0) ? '+' : '-');
  }
  else
  {
    if (exponent == 255)
    {
      if ((mSize = 1) && (mantis[0] == 0))
      {
        printf("%cInf\n", (sign == 0) ? '+' : '-');
      }
      else
      {
        printf("NaN\n");
      }
    }
    else
    {
      PrintUpLine(sign, mantis, exponent, mSize);
      PrintLowLine(mantis, exponent, mSize);
    }
  }
}

void BitwiseDecomposition(float f)
{
  int n =  *((int *) &f);
  int i, exponent = 0, sign, mSize;
  int bitinfloat = sizeof(float) * 8;
  int mantis[sizeof(float) * 8 - 9];

  sign = abs(((1 << (bitinfloat - 1)) & n) >> (bitinfloat - 1));
  // abs need because (1 << (bitinfloat - 1) = -2^31 and if n<0 and all this = -2^31/2^31 = -1
  
  exponent = ((255 << (bitinfloat - 9)) & n) >> (bitinfloat - 9);

  i = 0;
  while (((n & 1) == 0) && (i < bitinfloat - 10))
  {
    n >>= 1;
    ++i;
  }
  
  mSize = bitinfloat - 9 - i;
  
  while (i < bitinfloat - 9)
  {
    mantis[bitinfloat - 10 - i] = n & 1;
    n >>= 1;
    ++i;
  }

  PrintFloat(sign, mantis, exponent, mSize);
}

int main()
{
  float f = 0;
  
  scanf("%f", &f);
  BitwiseDecomposition(f);
  return 0;
}