/* Mikhail Tashkinov 2014
 * Hexic field
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexic
{
    enum Colour
    {
        RED,
        BLUE,
        GREEN,
        YELLOW,
        PURPLE,
        ORANGE,
        PINK,
        BROWN,
        GRAY,
        WHITE,
    }

    public enum GroupType
    {
        ONE, // В первой колонке 1 hex
        TWO, // В первой колонке 2 hex'a
    }

    public enum RotationType
    {
        LEFT, // Против часовой стрелки. 
        RIGHT, // По часовой стрелке. 
    }

    class Field
    {
        private Colour[,] field;
        private bool[,] curSeq;
        private int[] neededFields;
        private int m, n, k;
        private Random random;

        private const int COLORS_NUM = 7;

        public int getWidth()
        {
            return n;
        }

        public int getHeight()
        {
            return m;
        }

        public Field()
        {
            read();
            regen();
            prepare();
        }

        public int nextStep(GroupType groupType, RotationType rotationType, int x, int y)
        {
            int[,] group = getRotationGroup(groupType, x, y);
            rotate(rotationType, group);
            return prepare();
        }

        public int tryRotate(GroupType groupType, RotationType rotationType, int x, int y)
        {
            int[,] group;
            int points = 0;

            group = getRotationGroup(groupType, x, y);

            switch (rotationType)
            {
                case (RotationType.LEFT):
                    if (rotate(rotationType, group))
                    {
                        points = getPoints(group);
                        rotate(RotationType.RIGHT, group);
                    }
                    break;
                case (RotationType.RIGHT):
                    if (rotate(rotationType, group))
                    {
                        points = getPoints(group);
                        rotate(RotationType.LEFT, group);
                    }
                    break;
                default:
                    break;
            }
            return points;
        }

        private int[,] getRotationGroup(GroupType groupType, int x, int y)
        {
            int posForInc;
            int[] xs;
            int[] ys;

            switch (groupType)
            {
                case GroupType.ONE:
                    xs = new int[3] { x, x + 1, x + 1 };
                    ys = new int[3] { y, y - 1, y };
                    posForInc = 1;
                    break;
                case GroupType.TWO:
                    xs = new int[3] { x, x, x + 1 };
                    ys = new int[3] { y + 1, y, y };
                    posForInc = 2;
                    break;
                default:
                    xs = new int[3];
                    ys = new int[3];
                    posForInc = 3;
                    break;
            }

            if (x % 2 == 1)
            {
                for (int i = posForInc; i < 3; ++i)
                {
                    ++ys[i];
                }
            }
            return new int[2, 3] {{xs[0], xs[1], xs[2]}, {ys[0], ys[1], ys[2]}};
        }

        private int getPoints(int[,] group)
        {
            int sum = 0;
            for (int i = 0; i < 3; ++i)
            {
                clearCurSeq();
                sum += countPoints(countNeighborsWithSameColour(group[0, i], group[1, i]));
            }
            return sum;
        }

        private bool rotate(RotationType rotationType, int[,] group)
        {
            for (int i = 0; i < 3; ++i )
            {
                if (!verify(group[0, i], group[1, i]))
                {
                    return false;
                }
            }

            Colour cur = field[group[0, 0], group[1, 0]];

            switch (rotationType)
            {
                case RotationType.LEFT:
                    field[group[0, 0], group[1, 0]] = field[group[0, 2], group[1, 2]];
                    field[group[0, 2], group[1, 2]] = field[group[0, 1], group[1, 1]];
                    field[group[0, 1], group[1, 1]] = cur;
                    break;
                case RotationType.RIGHT:
                    field[group[0, 0], group[1, 0]] = field[group[0, 1], group[1, 1]];
                    field[group[0, 1], group[1, 1]] = field[group[0, 2], group[1, 2]];
                    field[group[0, 2], group[1, 2]] = cur;
                    break;
                default:
                    break;
            }
            return true;
        }

        private void regen()
        {
            for (int i = 0; i < n; ++i)
            {
                int j = neededFields[i];
                while (j != 0)
                {
                    field[i, m - j] = (Colour)random.Next(COLORS_NUM);
                    --j;
                }
            }
        }

        private void read()
        {
            m = Convert.ToInt32(Console.ReadLine());
            n = Convert.ToInt32(Console.ReadLine());
            k = Convert.ToInt32(Console.ReadLine());
            field = new Colour[n, m];
            neededFields = new int[n];
            for (int i = 0; i < n; ++i)
            {
                neededFields[i] = m;
            }
            random = new Random(k);
            curSeq = new bool[n, m];
        }

        private int prepare()
        {
            bool isRemoval;
            bool[,] forDelete;
            int points = 0;
            int hexSeq;

            do
            {
                forDelete = new bool[n, m];
                isRemoval = false;
                for (int i = 0; i < n; ++i)
                {
                    for (int j = 0; j < m; ++j)
                    {
                        clearCurSeq();
                        if (!forDelete[i, j])
                        {
                            hexSeq = countNeighborsWithSameColour(i, j);
                            points += countPoints(hexSeq);
                            if (hexSeq > 2)
                            {
                                isRemoval = true;
                                merge(forDelete, curSeq);
                            }
                        }
                    }
                }
                deleteHexs(forDelete);
                regen();
            }
            while (isRemoval);

            return points;
        }

        private void merge(bool[,] array1, bool[,] array2)
        {
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    array1[i, j] = array1[i, j] || array2[i, j];
                }
            }
        }

        private void deleteHexs(bool[,] marked)
        {
            for (int i = 0; i < n; ++i)
            {
                neededFields[i] = 0;
                int j = 0;
                while (j < m - neededFields[i])
                {
                    int seq = 0;
                    int seqStart;

                    while ((j < m - neededFields[i]) && (!marked[i, j]))
                    {
                        ++j;
                    }

                    seqStart = j;
                    while ((j < m - neededFields[i]) && (marked[i, j]))
                    {
                        ++seq;
                        ++j;
                    }

                    if (j != m - neededFields[i])
                    {
                        j = seqStart;
                    }

                    for (int l = seqStart; l < m - seq - neededFields[i]; ++l)
                    {
                        field[i, l] = field[i, l + seq];
                        marked[i, l] = marked[i, l + seq];
                    }
                    neededFields[i] += seq;
                }
            }
        }

        public int countNeighborsWithSameColour(int x, int y)
        {
            Colour curColour = field[x, y];
            bool isEvenColumn = x % 2 == 0;
            int count = 1;
            int[] xNeighbors;
            int[] yNeighbors;

            curSeq[x, y] = true;

            if (isEvenColumn)
            {
                xNeighbors = new int[6] { x, x, x - 1, x - 1, x + 1, x + 1 };
                yNeighbors = new int[6] { y - 1, y + 1, y, y - 1, y, y - 1 };
            }
            else
            {
                xNeighbors = new int[6] { x, x, x - 1, x - 1, x + 1, x + 1 };
                yNeighbors = new int[6] { y - 1, y + 1, y, y + 1, y, y + 1 };
            }

            for (int i = 0; i < 6; ++i)
            {
                if ((verify(xNeighbors[i], yNeighbors[i])) && (!curSeq[xNeighbors[i], yNeighbors[i]]) && (checkColour(curColour, xNeighbors[i], yNeighbors[i])))
                {
                    count += countNeighborsWithSameColour(xNeighbors[i], yNeighbors[i]);
                }
            }
            return count;
        }

        private bool verify(int x, int y)
        {
            return (x >= 0) && (x < n) && (y >= 0) && (y < m);
        }

        private bool checkColour(Colour colour, int x, int y)
        {
            bool res = field[x, y] == colour;
            return res;
        }

        private void clearCurSeq()
        {
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < m; ++j)
                {
                    curSeq[i, j] = false;
                }
                neededFields[i] = 0;
            }
        }

        public static int countPoints(int hexsNum)
        {
            int points = 0;

            for (int i = hexsNum - 2; i > 0; --i)
            {
                if (points == 0)
                {
                    points = 3;
                } else
                {
                    points = points * 2;
                }
            }

            return points;
        }
    }
}
