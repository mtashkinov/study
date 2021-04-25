/* Tashkinov Mikhail (c) 2013
   Print first n strings
*/

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define DEFAULT_STR_NUM 10

enum Error
{
  NO_ERRORS = 0,
  TOO_MANY_PARAMETRS = 1,
  TOO_LONG_ARGUMENT = 2,
  INVALID_ARGUMENTS = 3,
  NO_SUCH_FILE = 4
};

typedef enum Error Error;


Error Check_three_arg(int argc, char * argv[])
{
  char extraStr[255] = "";
  int n;
  
  if ((strlen(argv[1]) <= 50) && (strlen(argv[2]) <= 50) && (strlen(argv[3]) <= 50))
  {
    if (strcmp(argv[1], "-n") == 0)
    {
      if ((sscanf(argv[2], "%d%s", &n, &extraStr) == 1) && (strlen(extraStr) == 0))
      {
        return NO_ERRORS;
      }
      else
      {
        return INVALID_ARGUMENTS;
      }
    }
    else
    {
      if (strcmp(argv[2], "-n") == 0)  
      {
        if ((sscanf(argv[3], "%d%s", &n, &extraStr) == 1) && (strlen(extraStr) == 0))
        {
          return NO_ERRORS;
        }
        else
        {
          return INVALID_ARGUMENTS;
        }
      }
      else
      {
        return INVALID_ARGUMENTS;
      }
    }
  }
  else
  {
    return  TOO_LONG_ARGUMENT;
  }
}

Error Check_two_arg(int argc, char * argv[])
{
  char extraStr[255] = "";
  int n;

  if ((strlen(argv[1]) <= 50) && (strlen(argv[1]) <= 50))
  {
    if (strcmp(argv[1], "-n") == 0)
    {
      if ((sscanf(argv[2], "%d%s", &n, &extraStr) == 1) && (strlen(extraStr) == 0))
      {
        return NO_ERRORS;
      }
      else
      {
        return INVALID_ARGUMENTS;
      }
    }
    else
    {
      return INVALID_ARGUMENTS;
    }
  }
  else
  {
    return  TOO_LONG_ARGUMENT;
  }
}

Error Val_par(int argc, char * argv[])
{
  Error errCode = NO_ERRORS;
  if (argc > 4)
  {
    errCode = TOO_MANY_PARAMETRS;
  }
  else
  {
    if (argc == 3)
    {
      errCode = Check_two_arg(argc, argv);
    }
    else
    {
      if (argc == 4)
      {
        errCode = Check_three_arg(argc, argv);
      }
      else
      {
        if (argc == 2)
        {
          if (strlen(argv[1]) > 50)
          {
            errCode =  TOO_LONG_ARGUMENT;
          }
        }
      }
    }
  }
  return errCode;
}

int Parse_str_num(int argc, char * argv[])
{
  if (argc == 3)
  {
    return atoi(argv[2]);
  }
  else
  {
    if (argc == 4)
    {
      if (strcmp(argv[1], "-n") == 0)
      {
        return atoi(argv[2]);
      }
      else
      {
        if (strcmp(argv[2], "-n") == 0)
        {
          return atoi(argv[3]);
        }
      }
    }
  }
  return DEFAULT_STR_NUM;
}

char * Parse_file_name(int argc, char * argv[])
{
  if (argc == 2)
  {
    return argv[1];
  }
  else
  {
    if (argc == 4)
    {
      if (strcmp(argv[1], "-n") == 0)
      {
        return argv[3];
      }
      else
      {
        if (strcmp(argv[2], "-n") == 0)
        {
          return argv[1];
        }
      }
    }
  }
  return NULL;
}

void Print_error_message(Error errCode)
{
  const char * messages[] = 
  {"Too many parameters",
   "Too long argument",
   "Invalid arguments",
   "No such file in directory"
   };
  
  puts(messages[errCode - 1]);
}

Error Print(char * fileName, int strnum)
{
  FILE * input;
  int c, i = 0;
  
  if (fileName == NULL)
  {
    input = stdin;
  }
  else
  {     
    input = fopen(fileName, "r");
    if (input == NULL)
    {
      return NO_SUCH_FILE;
    }
  }
  
  do
  {
    c = getc(input);
    while ((c != '\n') && (c != EOF))
    {
      putchar(c);
      c = getc(input);
    }
	printf("\n");
    ++i;
  }
  while ((c != EOF) && (i < strnum));
  
  if (fileName != NULL)
  {
    fclose(input);
  }
  
  return NO_ERRORS;
}

int main(int argc, char * argv[])
{
  Error errCode = Val_par(argc, argv);
  
  if (errCode == NO_ERRORS)
  {
    char * fileName = Parse_file_name(argc, argv);
    int strNum = Parse_str_num(argc, argv);
    
    errCode = Print(fileName, strNum);
  }

  if(errCode != NO_ERRORS)
  {
    Print_error_message(errCode);
  }
  
  return errCode;
}