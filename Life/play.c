/* Tashkinov Mikhail (c) 2013
   Play in game "Life"
*/

#include <malloc.h>
#include <stdio.h>
#include <stdlib.h>
#include "play.h"

int Count_neighbors(World source, int i, int j)
{
  int neighborsNum = 0;
  
  if (source.field[(i - 1 + source.hieght) % source.hieght][(j - 1 + source.width) % source.width] == ALIVE)
  {
    ++neighborsNum;
  }
  if (source.field[(i - 1 + source.hieght) % source.hieght][j] == ALIVE)
  {
    ++neighborsNum;
  }
  if (source.field[(i - 1 + source.hieght) % source.hieght][(j + 1) % source.width] == ALIVE)
  {
    ++neighborsNum;
  }
  if (source.field[i][(j - 1 + source.width) % source.width] == ALIVE)
  {
    ++neighborsNum;
  }
  if (source.field[i][(j + 1) % source.width] == ALIVE)
  {
    ++neighborsNum;
  }
  if (source.field[(i + 1) % source.hieght][(j - 1 + source.width) % source.width] == ALIVE)
  {
    ++neighborsNum;
  }
  if (source.field[(i + 1) % source.hieght][j] == ALIVE)
  {
    ++neighborsNum;
  }
  if (source.field[(i + 1) % source.hieght][(j + 1) % source.width] == ALIVE)
  {
    ++neighborsNum;
  }
  
  return neighborsNum;
}

void Next_turn(World source, Cell ** next)
{
  int i, j;
  int neighborsNum;
  
  for (i = 0; i < source.hieght; ++i)
  {
    for (j = 0; j < source.width; ++j)
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

int Is_stop(World source, Cell ** next)
{
  int isStop = 1;
  int i = 0, j;
  
  while ((isStop) && (i < source.hieght))
  {
    j = 0;
    while ((isStop) && (j < source.width))
    {
      isStop = (source.field[i][j] == next[i][j]);
      ++j;
    }
    ++i;
  }
  
  if (isStop == 0)
  {
    isStop = 1;
    while ((isStop) && (i < source.hieght))
    {
      j = 0;
      while ((isStop) && (j < source.width))
      {
        isStop = (source.field[i][j] == DEAD);
        ++j;
      }
      ++i;
    }
  }
  
  return (isStop);
}
    
World Play(World source)
{
  Cell ** next = Create(source.hieght, source.width);
  Cell ** temp;
  
  Print_field(source, next);
  do
  {
    Next_turn(source, next);

    temp = next;
    next = source.field;
    source.field = temp;
	Print_field(source, next);
  }
  while (Is_stop(source, next) != 1);
  
  Del(next, source.hieght);

  return source;
}