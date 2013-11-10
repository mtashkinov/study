/* Tashkinov Mikhail (c) 2013
   Counts the number repetitions of words and print it to stdout
*/

#include <stdio.h>
#include "count.h"

#define max(a, b) (a > b) ? a : b

enum Error
{
  OK = 0,
  INVALID_ARG_NUM = 1,
  NO_SUCH_FILE = 2
}err; 

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

int Find_max(Node * node)
{
  int max = node->count;
  
  if (node->left != NULL)
  {
    max = max(max, Find_max(node->left));
  }
  if (node->right != NULL)
  {
    max = max(max, Find_max(node->right));
  }
  
  return max;
}

void Print(Node * node, int maxSize)
{
  int spaceNum = maxSize - Number_size(node->count), i;
  
  for (i = 0; i < spaceNum; ++i)
  {
    printf("%c", ' ');
  }
  printf("%d %s\n", node->count, node->word);
  
  if (node->left != NULL)
  {
    Print(node->left, maxSize);
  }
  if (node->right != NULL)
  {
    Print(node->right, maxSize);
  }
}

FILE * Parse_file(int argc, char * argv[])
{
  FILE * input = NULL;
  if (argc == 2)
  {
    input = fopen(argv[1], "r");
    if (input == NULL)
    {
      err = NO_SUCH_FILE;
    }
    else
    {
      err = OK;
    }
  }
  else
  {
    err = INVALID_ARG_NUM;
  }
  
  return input;
}

void Print_error_message()
{
  const char * messages[] = 
  {"Invalid number of arguments",
   "No such file in directory"
  };
  
  puts(messages[err - 1]);
}

int main(int argc, char * argv[])
{
  FILE * input;
  Node * tree = NULL;
  int maxSize;
  char * word;
  
  input = Parse_file(argc, argv);
  
  if (err == OK)
  {
    word = Next_word(input);
    tree = Create(word);
    Count_words(input, tree);
    
    maxSize = Number_size(Find_max(tree));
    Print(tree, maxSize);
    Del(tree);
    fclose(input);
  }
  else
  {
    Print_error_message();
  }
  
  return 0;
}