/* Tashkinov Mikhail (c) 2013
*/

#ifndef COUNT_H
#define COUNT_H

#include "tree.h"

#define MAX_WORD_SIZE 15

char * Next_word(FILE *);
void Count_words(FILE *, Node *);

#endif