/* Tashkinov Mikhail (c) 2013
   Is palindrom?
*/
#include <stdio.h>

int Length(char * string)
{
  int len = 0;
  
  while (string[len] != '\0')
  {
    ++len;
  }
  return len;
}

char ChangeReg(char c)
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

int isPalindrom(char * string)
{
  int left,right,isp = 1;
  
  right=Length(string) - 1;
  left = 0;
  while (isp && (left < right))
  {
    while ((left < right) && (string[left] == ' '))
	{
      ++left;
	}
	while ((left < right) && (string[right] == ' '))
	{
      --right;
	}
    if (left < right)
	{
      if (ChangeReg(string[left]) != ChangeReg(string[right]))
      {
        isp = 0;
      }
    }
    ++left;
    --right;
  }
  return isp;
}

int main()
{
  int i = 0;
  char string[256];
  char c;
  
  scanf("%c", &c);
  while (c != '\n')
  {
      string[i] = c;
      ++i;
      scanf("%c", &c);
  }
  string[i] = '\0';
  printf("%s", (isPalindrom(string)) ? "Yes" : "No");
  return 0;
}