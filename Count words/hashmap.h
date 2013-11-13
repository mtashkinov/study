/* Tashkinov Mikhail (c) 2013
*/

#ifndef HASHMAP_H
#define HASHMAP_H

#define MAX_HASH 5000

#include "list.h"

typedef List ** Hashmap;

Hashmap Create();
int Count_hash(char *);
int Find_max(Hashmap);
void Print(Hashmap);
void Print_stat(Hashmap);
void Del_hashmap(Hashmap);

#endif