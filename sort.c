/* Tashkinov Mikhail (c) 2013
   Sort lines in file
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

int Scan(FILE * input, char ** text)
{
  char * currline;
  int memBlocksNum = 1, lineNum = 0;

  currline = Read_line(input);
  
  while (feof(input) == 0)
  {
    ++lineNum;
    if (lineNum > memBlocksNum * MEM_BLOCK_SIZE)
    {
      text = (char**)realloc(text, memBlocksNum * MEM_BLOCK_SIZE);
    }
    text[lineNum - 1] = currline;
    currline = Read_line(input);
  }
  
  ++lineNum;
  text[lineNum - 1] = currline;
  
  if (input != stdin)
  {
    fclose(input);
  }
  
  return lineNum;
}

void Print(char ** text, int lineNum)
{
  int i;
  for (i = 0; i < lineNum; ++i)
  {
    puts(text[i]);
  }
}

void QuickSort(char ** mas, int first, int last)
{
  char * x = mas[(first+last) / 2];
  int left = first, right = last;
  char * temp;
  
  while (left < right)
  {
    while (strcmp(mas[left], x) == -1)
    {
      ++left;
    }
    while (strcmp(mas[right], x) == 1)
    {
      --right;
    }
    if (left <= right)
    {
      temp = mas[right];
      mas[right] = mas[left];
      mas[left] = temp;
      ++left;
      --right;
    }
  }
  if (first < right)
  {
    QuickSort(mas, first, right);
  }
  if (last > left)
  {
    QuickSort(mas, left, last);
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
  char ** text;
  int lineNum = 0;
  int i;

  errCode = NO_ERRORS;
  Validate_file_name(argc, argv);
  
  if (errCode == NO_ERRORS)
  {
    text = (char**)malloc(MEM_BLOCK_SIZE);
    input = Parse_file_name(argc, argv);
  }
  if (errCode == NO_ERRORS)
  {
    lineNum = Scan(input, text);
    QuickSort(text, 0, lineNum - 1);
    Print(text, lineNum);
  
    for (i = 0; i < lineNum; ++i)
    {
      free(text[i]);
    }
    free(text);
  }  
  else
  {
    Print_error_message(errCode);
  }
  
  return errCode;
}