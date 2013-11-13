/* Tashkinov Mikhail (c) 2013
*/

#ifndef LIST_H
#define LIST_H

typedef struct Word
{
  char * word;
  int count;
  struct Word * next;
} Word;

typedef struct List
{
  Word * first;
} List;

Word * Is_exist(List *, char *);
void Add(List *, char *);
void Del(List *);

#endif