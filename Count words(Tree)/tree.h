/* Tashkinov Mikhail (c) 2013
*/

#ifndef TREE_H
#define TREE_H

typedef struct Node
{
  char * word;
  int count;
  struct Node * left;
  struct Node * right;
} Node;

Node * Create();
Node * Is_exist(Node *, char *);
Node * Create(char * word);
void Add(Node *, char *);
void Del(Node *);

#endif