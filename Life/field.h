/* Tashkinov Mikhail (c) 2013
*/

#ifndef FIELD_H
#define FIELD_H

#include <stdio.h>

typedef enum Cell
{
  DEAD = 0,
  ALIVE = 1
} Cell;

typedef struct World
{
  int hieght;
  int width;
  Cell ** field;
} World;

Cell ** Create(int);
void Del(Cell **, int);
World Scan(FILE *);

#endif