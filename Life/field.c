/* Tashkinov Mikhail (c) 2013
   Implementation of Create and Del
*/

#include "field.h"

Cell ** Create(int size)
{
  int i;
  
  Cell ** field = (Cell **)malloc(size * sizeof(Cell *));
  
  for (i = 0; i < size; ++i)
  {
    field[i] = (Cell *)malloc(size * sizeof(Cell));
  }

  return field;
}

void Del(Cell ** field,int size)
{
  int i;
  
  for (i = 0; i < size; ++i)
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
  
  fscanf(input, "%d%c", &source.size, &c);
  source.field = Create(source.size);
  
  for (i = 0; i < source.size; ++i)
  {
    for (j = 0; j < source.size; ++j)
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
  
  return source;
}