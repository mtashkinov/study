/* Tashkinov Mikhail (c) 2013
   Counts the number repetitions of words and print it to stdout
*/

#include <stdio.h>
#include "count.h"
#include "list.h"
#include "hashmap.h"

enum Error
{
  OK = 0,
  INVALID_ARG_NUM = 1,
  NO_SUCH_FILE = 2
}err; 

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
  Hashmap hashmap = Create();
  
  input = Parse_file(argc, argv);
  
  if (err == OK)
  {
    Count_words(input, hashmap);
    Print_stat(hashmap);
    Print(hashmap);
    Del_hashmap(hashmap);
    fclose(input);
  }
  else
  {
    Print_error_message();
  }
  
  return 0;
}