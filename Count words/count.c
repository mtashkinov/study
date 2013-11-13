/* Tashkinov Mikhail (c) 2013
   Counts the number repetitions of words
*/

#include <stdio.h>
#include <string.h>
#include <malloc.h>
#include "count.h"
#include "list.h"

char Low_reg(char c)
{
  if ((c >= 'A') && (c <= 'Z'))
  {
    return c - 'A' + 'a';
  }
  else
  {
    return c;
  }
}

int Is_letter(char c)
{
  return ((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z'));
}

char * Next_word(FILE * input)
{
  char * word = (char *)malloc(MAX_WORD_SIZE);
  int i = 0;
  char c;
  
  c = fgetc(input);
  while ((!Is_letter(c)) && (feof(input) == 0))
  {
    c = fgetc(input);
  }

  while ((Is_letter(c) || (c == '-')) && (feof(input) == 0))
  {
    c = Low_reg(c);
    word[i] = c;
    ++i;
    c = fgetc(input);
  }
  word[i] = '\0';
  word = (char *)realloc(word, (strlen(word) + 1) * sizeof(char));
  
  return word;
}

void Count_words(FILE * input, Hashmap hashmap)
{
  int hash;
  char * word;
  Word * curWord;
  
  word = Next_word(input);
  while (strcmp(word, "") != 0)
  {
    hash = Count_hash(word);
    curWord = Is_exist(hashmap[hash], word);
    if (curWord != NULL)
    {
      ++curWord->count;
    }
    else
    {
      Add(hashmap[hash], word);
    }
    word = Next_word(input);
  }
}