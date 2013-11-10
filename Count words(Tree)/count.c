/* Tashkinov Mikhail (c) 2013
   Counts the number repetitions of words
*/

#include <stdio.h>
#include <string.h>
#include <malloc.h>
#include "count.h"

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
  word = realloc(word, (strlen(word) + 1) * sizeof(char));
  
  return word;
}

void Count_words(FILE * input, Node * tree)
{
  char * word;
  Node * curWord;
  
  word = Next_word(input);
  while (strcmp(word, "") != 0)
  {
    curWord = Is_exist(tree, word);
    if (curWord != NULL)
    {
      ++curWord->count;
    }
    else
    {
      Add(tree, word);
    }
    word = Next_word(input);
  }
}