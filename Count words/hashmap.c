/* Tashkinov Mikhail (c) 2013
   Hashmap implementation
*/

#include <malloc.h>
#include <stdio.h>
#include "hashmap.h"

#define HASH_BASE 3

Hashmap Create()
{
  Hashmap hashmap = (Hashmap)malloc(sizeof(List *) * MAX_HASH);
  int i;
  
  for (i = 0; i < MAX_HASH; ++i)
  {
    hashmap[i] = (List *)malloc(sizeof(List));
    hashmap[i]->first = NULL;
  }
  return hashmap;
}

int Count_hash(char * word)
{
  int p = HASH_BASE;
  int q = MAX_HASH;
  int i = 0, hash = 0;
  
  while (word[i] != '\0')
  {
    hash = (hash * p + word[i]) % q;
    ++i;
  }
  
  return hash;
}

int Find_max(Hashmap hashmap)
{
  int max = -1, i;
  Word * e;
  
  for (i = 0; i < MAX_HASH; ++i)
  {
    e = hashmap[i]->first;
    while (e != NULL)
    {
      if (e->count > max)
      {
        max = e->count;
      }
      e = e->next;
    }
  } 
  
  return max;
}

int Number_size(int number)
{
  int size = 0, n = number;
  
  while (n != 0)
  {
    n = n / 10;
    ++size;
  }
  if (size == 0)
  {
    ++size;
  }
  
  return size;
}

void Print(Hashmap hashmap)
{
  int maxSize = Number_size(Find_max(hashmap));
  int spaceNum, i, j;
  Word * e;
  
  for (i = 0; i < MAX_HASH; ++i)
  {
    e = hashmap[i]->first;
    while (e != NULL)
    {
      spaceNum = maxSize - Number_size(e->count);
      for (j = 0; j < spaceNum; ++j)
      {
        printf("%c", ' ');
      }
      printf("%d %s\n", e->count, e->word);
      e = e->next;
    }
  } 
}

void Print_stat(Hashmap hashmap)
{
  int i, curnum = 0, max, min, wordsnum = 0, empty = 0;
  Word * e;
  
  e = hashmap[0]->first;
  while (e != NULL)
  {
    ++curnum;
    e = e->next;
  }
  min = max = curnum;
  wordsnum += curnum;
  if (curnum == 0)
  {
    ++empty;
  }
  
  for (i = 1; i < MAX_HASH; ++i)
  {
    curnum = 0;
    e = hashmap[i]->first;
    while (e != NULL)
    {
      ++curnum;
      e = e->next;
    }
    
    wordsnum += curnum;
    if (curnum > max)
    {
      max = curnum;
    }
    if (curnum < min)
    {
      min = curnum;
    }
    if (curnum == 0)
    {
      ++empty;
    }
  }
  printf("#stat max=%d; med=%0.3f; min=%d; empty hash=%d\n", max, (float)wordsnum / (float)MAX_HASH, min, empty);
}

void Del_hashmap(Hashmap hashmap)
{
  int i;
  
  for (i = 0; i < MAX_HASH; ++i)
  {
    Del(hashmap[i]);
  }
  free(hashmap);
}