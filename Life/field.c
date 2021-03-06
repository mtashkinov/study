/* Tashkinov Mikhail (c) 2013
   Implementation of Create and Del
*/

#include <windows.h>
#include "field.h"

#define SLEEP_TIME 250

Cell ** Create(int hieght, int width)
{
  int i;
  
  Cell ** field = (Cell **)malloc(hieght * sizeof(Cell *));
  
  for (i = 0; i < hieght; ++i)
  {
    field[i] = (Cell *)malloc(width * sizeof(Cell));
  }

  return field;
}

void Del(Cell ** field, int hieght)
{
  int i;
  
  for (i = 0; i < hieght; ++i)
  {
    free(field[i]);
  }
  free(field);
}

World Scan(FILE * input)
{
  World source;
  char c;
  int i, j;

  fscanf(input, "%d%d%c", &source.hieght, &source.width, &c);
  source.field = Create(source.hieght, source.width);
  
  for (i = 0; i < source.hieght; ++i)
  {
    for (j = 0; j < source.width; ++j)
    {
      fscanf(input, "%c", &c);
      if (c == '#')
      {
        source.field[i][j] = ALIVE;
      }
      else
      {
        source.field[i][j] = DEAD;
      }
    }
    fscanf(input, "%c", &c);
  }
  
  fclose(input);
  
  return source;
}

void Print_field(World source, Cell ** prev)
{
  int i, j;
  char c;

  for (i = 0; i < source.hieght; ++i)
  {
    for (j = 0; j < source.width; ++j)
    {
      if (source.field[i][j] != prev[i][j])
      {
        SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), (COORD) {j , i});
        if (source.field[i][j] == ALIVE)
        {
          printf("#");
        }
        else
        {
          printf(" ");
        }
      }
    }
    printf("\n");
  }
  Sleep((DWORD)SLEEP_TIME);
}