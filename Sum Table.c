/* Tashkinov Mikhail (c) 2013
   Print sum table
*/

#include <stdio.h>

#define MAXSIZE 50

int NumberSize(int number)
{
  int size = 0, n = number;
  
  while (n != 0)
  {
    n = n / 10;
    ++size;
  }
  
  return size;
}

void CalculateTable(int (* mas)[MAXSIZE], int n)
{
  int i, j;
  
  for (i = 0; i < n; ++i)
  {
    mas[i][0] = 1;
	mas[0][i] = 1;
  }
  
  for (i = 1; i < n; ++i)
  {
    for (j = 1; j < n; ++j)
    {
      mas[i][j] = mas[i-1][j]+mas[i][j-1];
    }
  }
}

void PrintEmptyString(int width, int n)
{
  int i, j;
  
  printf("%c", 186);
  
  for (i = 0; i < n; ++i)
  {
    for (j = 1; j <= width; ++j)
    {
      printf("%c", ' ');
    }
    if (i != n - 1)
    {
	  printf("%c", 179);
    }
  }
  
  printf("%c\n", 186);
}

void PrintBorder(int width, int n)
{
  int i, j;

  printf("%c", 199);
  
  for (i = 0; i < n; ++i)
  {
    for (j = 1; j <= width; ++j)
    {
      printf("%c", 196);
    }
    if (i != n-1)
    {
	  printf("%c", 197);
    }
  }
  
  printf("%c\n", 182);
}

void PrintFirstLine( int width, int n)
{
  int i, j;
  
  printf("%c", 201);
  
  for (i = 0; i < n; ++i)
  {
    for (j = 0; j < width; ++j)
    {
      printf("%c", 205);
    }
    if (i != n - 1)
    {
      printf("%c", 209);
    }
  }
  
  printf("%c\n", 187);
}

void PrintLine(int (* mas)[MAXSIZE], int width, int n, int line)
{
  int i, j, size, spaceLeft, spaceRight;
  
  PrintEmptyString(width, n);
  
  printf("%c", 186);
  
  for (i = 0; i < n; ++i)
  {
    size = NumberSize(mas[line][i]);
    spaceLeft = (width - size) / 2+(width - size) % 2;
    spaceRight = (width - size) / 2;
    
    for (j = 0; j < spaceLeft; ++j)
    {
      printf("%c", ' ');
    }
    
    printf("%d", mas[line][i]);
    for (j = 0; j < spaceRight; ++j)
    {
      printf("%c", ' ');
    }
    if (i != n - 1)
    {
      printf("%c", 179);
    }
  }
  
  printf("%c\n", 186);
  PrintEmptyString(width, n);
  
  if (line != n - 1)
  {
    PrintBorder(width, n);
  }
}

void PrintLastLine(int width, int n)
{
  int i, j;
  
  printf("%c", 200);
  
  for (i = 0; i < n; ++i)
  {
    for (j = 0; j < width; ++j)
    {
      printf("%c", 205);
    }
    if (i != n  -1)
    {
      printf("%c", 207);
    }
  }
  
  printf("%c\n", 188);
}


void DrawTable(int (* mas)[MAXSIZE], int n)
{
  int width, i;

  width = NumberSize(mas[n - 1][n - 1]) + 2;
  PrintFirstLine(width, n);
  
  for (i = 0; i < n; ++i)
  {
    PrintLine(mas, width, n, i);
  }
  
  PrintLastLine(width, n);
}

int main()
{
  int n;
  int mas[MAXSIZE][MAXSIZE];
  
  scanf("%d", &n);
  CalculateTable(mas, n);
  DrawTable(mas, n);
  return 0;
}