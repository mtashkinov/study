/* Tashkinov Mikhail (c) 2013
   List implementation
*/

#include <malloc.h>
#include <stdlib.h>
#include "list.h"
#include <string.h>

Word * Is_exist(List * list, char * word)
{
  Word * curWord = list->first;
  
  while ((curWord != NULL) && (strcmp(curWord->word, word) != 0))
  {
    curWord = curWord->next;
  }
  
  return curWord;
}

void Add(List * list, char * word)
{
  Word * e = (Word *)malloc(sizeof(Word));
  
  e->count = 1;
  e->word = word;
  e->next = list->first;
  list->first = e;
}

void Del(List * list)
{
  Word * e;
  Word * ne;
  
  e = list->first;
  if (e != NULL)
  {
    ne = e->next;

    while (ne != NULL)
    {
      e = ne;
      ne = e->next;
      free(e->word);
      free(e);
    }
    e = list->first;
    free(e->word);
    free(e);
  }
    free(list); 
}