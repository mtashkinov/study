/* Tashkinov Mikhail (c) 2013
   Game "Life"
*/

#include <malloc.h>
#include "play.h"
#include "field.h"

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

int main(int argc, char ** argv)
{
  FILE * input;
  World source;
  
  input = Parse_file(argc, argv);
  
  if (err == OK)
  {
    source = Scan(input);
	source = Play(source);
    Del(source.field, source.hieght);
  }
  else
  {
    Print_error_message();
  }
  
  return 0;
}