/* Tashkinov Mikhail (c) 2013
   Play in game "Life"
*/

#include <malloc.h>
#include <stdio.h>
#include <stdlib.h>
#include "play.h"
#include "field.h"

int Count_neighbors(World source, int i, int j)
{
  int neighborsNum = 0;
  
  if (source.field[(i - 1 + source.size) % source.size][(j - 1 + source.size) % source.size] == ALIVE)
  {
    ++neighborsNum;
  }
  if (source.field[(i - 1 + source.size) % source.size][j] == ALIVE)
  {
    ++neighborsNum;
  }
  if (source.field[(i - 1 + source.size) % source.size][(j + 1) % source.size] == ALIVE)
  {
    ++neighborsNum;
  }
  if (source.field[i][(j - 1 + source.size) % source.size] == ALIVE)
  {
    ++neighborsNum;
  }
  if (source.field[i][(j + 1) % source.size] == ALIVE)
  {
    ++neighborsNum;
  }
  if (source.field[(i + 1) % source.size][(j - 1 + source.size) % source.size] == ALIVE)
  {
    ++neighborsNum;
  }
  if (source.field[(i + 1) % source.size][j] == ALIVE)
  {
    ++neighborsNum;
  }
  if (source.field[(i + 1) % source.size][(j + 1) % source.size] == ALIVE)
  {
    ++neighborsNum;
  }
  
  return neighborsNum;
}

void Next_turn(World source, Cell ** next)
{
  int i, j;
  int neighborsNum;
  
  for (i = 0; i < source.size; ++i)
  {
    for (j = 0; j < source.size; ++j)
    {
      neighborsNum = Count_neighbors(source, i, j);
      if (source.field[i][j] == DEAD)
      {
        if (neighborsNum == 3)
        {
          next[i][j] = ALIVE;
        }
        else
        {
          next[i][j] = DEAD;
        }
      }
      else
      {
        if ((neighborsNum >= 2) && (neighborsNum <= 3))
        {
          next[i][j] = ALIVE;
        }
        else
        {
          next[i][j] = DEAD;
        }
      }
    }
  }
}

void Print_field(World source)
{
  int i, j;
  char c;

  system("cls");
  for (i = 0; i < source.size; ++i)
  {
    for (j = 0; j < source.size; ++j)
    {
      if (source.field[i][j] == ALIVE)
      {
        printf("#");
      }
      else
      {
        printf(".");
      }
    }
    printf("\n");
  }
  scanf("%c", &c);
}

int Is_stop(World source, Cell ** next)
{
  int isStop = 1;
  int i = 0, j;
  
  while ((isStop) && (i < source.size))
  {
    j = 0;
    while ((isStop) && (j < source.size))
	{
      isStop = (source.field[i][j] == next[i][j]);
      ++j;
	}
	++i;
  }
  
  if (isStop == 0)
  {
    isStop = 1;
    while ((isStop) && (i < source.size))
    {
      j = 0;
      while ((isStop) && (j < source.size))
	  {
        isStop = (source.field[i][j] == DEAD);
        ++j;
	  }
	  ++i;
    }
  }
  
  return (isStop);
}

void Play(World source)
{
  Cell ** next = Create(source.size);
  Cell ** temp;
  
  Print_field(source);
  do
  {
    Next_turn(source, next);

    temp = next;
    next = source.field;
    source.field = temp;

    Print_field(source);
  }
  while (Is_stop(source, next) != 1);
}