/* Tashkinov Mikhail (c) 2013
   Realization of functions: strlen,strcpy,strcat,strcmp
*/

#include <stdio.h>

int strlen(char * string)
{
  int length = 0;
  
  while (string[length] != '\0')
  {
    ++length;
  }
  return length;
}

void strcpy(char * destination, char * source)
{
  int i = 0;
  
  while (source[i] != '\0')
  {
    destination[i] = source[i];
    ++i;
  }
  destination[i] = '\0';
}

void strcat(char * destination, char * source)
{
  int i = strlen(destination), j = 0;
  
  while (source[j] != '\0')
  {
    destination[i] = source[j];
    ++i;
    ++j;
  }
  destination[i] = '\0';
}

int strcmp(char * s1, char * s2)
{
  int i = 0;
  
  while ((s1[i] != '\0') && (s2[i] != '\0') && (s1[i] == s2[i]))
  {
    ++i;
  }
  if ((s1[i] != '\0') && (s2[i] != '\0'))
  {
    return (s1[i] > s2[i]) ? 1 : -1;
  }
  else
  {
    if ((s1[i] == '\0') && (s2[i] == '\0'))
    {
      return 0;
    }
    else
    {
      return (s2[i] == '\0') ? 1 : -1;
    }
  }
}

int main()
{
  char s1[100], s2[100];
  
  scanf("%s%s", &s1, &s2);
  printf("%d", strcmp(s1, s2));
  return 0;
}