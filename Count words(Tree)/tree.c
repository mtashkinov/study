/* Tashkinov Mikhail (c) 2013
   Tree implementation
*/

#include <malloc.h>
#include <stdlib.h>
#include "tree.h"
#include <string.h>

Node * Is_exist(Node * node, char * word)
{
  if (strcmp(word, node->word) == 0)
  {
    return node;
  }
  else if (strcmp(node->word, word) > 0)
  {
    if (node->left != NULL)
    {
      return Is_exist(node->left, word);
    }
    else
    {
      return NULL;
    }
  }
  else if (node->right != NULL)
  {
    return Is_exist(node->right, word);
  }
  else
  {
    return NULL;
  }
}

Node * Create(char * word)
{
  Node * e = (Node *)malloc(sizeof(Node));
  
  e->count = 1;
  e->word = word;
  e->left = NULL;
  e->right = NULL;
  
  return e;
}

void Add(Node * tree, char * word)
{
  Node * e = tree;
  Node * ne;
  
  if (strcmp(e->word, word) > 0)
  {
    ne = e->left;
  }
  else
  {
    ne = e->right;
  }
  while (ne != NULL)
  {
    e = ne;
    if (strcmp(ne->word, word) > 0)
    {
      ne = ne->left;
    }
    else
    {
      ne = ne->right;
    }
  }
  
  ne = (Node *)malloc(sizeof(Node));
  ne->count = 1;
  ne->word = word;
  ne->left = NULL;
  ne->right = NULL;
  
  if (strcmp(e->word, word) > 0)
  {
    e->left = ne;
  }
  else
  {
    e->right = ne;
  }
}

void Del(Node * node)
{
  if (node->left != NULL)
  {
    Del(node->left);
  }
  if (node->right != NULL)
  {
    Del(node->right);
  }
  free(node->word);
  free(node);
}