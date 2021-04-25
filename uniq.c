/* Tashkinov Mikhail (c) 2013
   Removing duplicate lines
*/

#include <stdio.h>
#include <malloc.h>
#include <stdlib.h>
#include <string.h>

#define MEM_BLOCK_SIZE 256

enum Error
{
  NO_ERRORS = 0,
  TOO_MANY_PARAMETRS = 1,
  NO_SUCH_FILE = 2,
  TOO_LONG_FILE_NAME = 3
} errCode;

typedef enum Error Error;

char * Read_line(FILE * input)
{
  int memBlocksNum = 0, eoln = 0;
  char * fullStr = (char*)malloc(MEM_BLOCK_SIZE);
  char currentStr[MEM_BLOCK_SIZE];
  
  fullStr[0] = '\0';
  while ((eoln == 0) && (fgets(currentStr, MEM_BLOCK_SIZE, input) != NULL))
  {
    ++memBlocksNum;
      
    if (fullStr[0] != '\0')
    {
      fullStr = (char*)realloc(fullStr, memBlocksNum * MEM_BLOCK_SIZE);
    }

    if (currentStr[strlen(currentStr) - 1] == '\n')
    {
      eoln = 1;
      currentStr[strlen(currentStr) - 1] = '\0';
    }
    strcat(fullStr, currentStr);
  }
  
  fullStr = (char*)realloc(fullStr, strlen(fullStr) + 1);

  return fullStr;
}

void Print(FILE * input)
{
  char * prevline = "";
  char * currline;
  
  currline = Read_line(input);
  
  while (feof(input) == 0)
  {
    if (strcmp(prevline, currline) != 0)
    {
      puts(currline);
    }
    if (prevline != "")
    {
      free(prevline);
    }
    prevline = currline;
    currline = Read_line(input);
  }
  
  if (strcmp(prevline, currline) != 0)
  {
    puts(currline);
  }
  
  if (input != stdin)
  {
    fclose(input);
  }
}

void Validate_file_name(int argc, char * argv[])
{
  if (argc > 2)
  {
    errCode = TOO_MANY_PARAMETRS;
  }
  else if ((argc != 1) && (strlen(argv[1]) > 50))
  {
    errCode = TOO_LONG_FILE_NAME;
  }
}

void Print_error_message(Error errCode)
{
  const char * messages[] =
  {"Too many parameters",
   "No such file in directory",
   "Too long file name"
  };
  
  puts(messages[errCode - 1]);
}

FILE * Parse_file_name(int argc, char * argv[])
{
  FILE * input;
  
  if (argc == 2)
  {
    input = fopen(argv[1], "r");
    if (input != NULL)
    {
      return input;
    }
    else
    {
      errCode = NO_SUCH_FILE;
    }
  }
  else
  {
    return stdin;
  }
}

int main(int argc, char * argv[])
{
  FILE * input;
  
  errCode = NO_ERRORS;
  Validate_file_name(argc, argv);
  
  if (errCode == NO_ERRORS)
  {
    input = Parse_file_name(argc, argv);
  }
  if (errCode == NO_ERRORS)
  {
    Print(input);
  }
  else
  {
    Print_error_message(errCode);
  }
  
  return errCode;
}