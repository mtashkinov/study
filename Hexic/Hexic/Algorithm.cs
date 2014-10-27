/* Mikhail Tashkinov 2014
 * Greedy algothm for Hexic
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hexic;

namespace Hexic
{
    class Algorithm
    {
        struct Result
        {
            public int value;
            public int x;
            public int y;
            public RotationType rotationType;
            public GroupType groupType;
        }

        public long play(Field field)
        {
            long points = 0;
            int newPoints = 0;

            do
            {
                newPoints = nextStep(field);
                points += newPoints;
            } while (newPoints != 0);

            return points;
        }

        int nextStep(Field field)
        {
            int n = field.getWidth();
            int m = field.getHeight();
            int points;
            Result max;
            max.value = 0;
            max.x = 0;
            max.y = 0;
            max.rotationType = RotationType.LEFT;
            max.groupType = GroupType.ONE;

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < m; ++j)
                {
                    max = tryRotations(field, i, j, max);
                }
            }
            points = field.nextStep(max.groupType, max.rotationType, max.x, max.y);

            return points;
        }

        Result tryRotations(Field field, int i, int j, Result curMax)
        {
            Result max;
            Result res;
            max.x = i;
            max.y = j;
            res.x = i;
            res.y = j;

            max.value = field.tryRotate(GroupType.ONE, RotationType.LEFT, i, j);
            max.rotationType = RotationType.LEFT;
            max.groupType = GroupType.ONE;

            res.value = field.tryRotate(GroupType.TWO, RotationType.LEFT, i, j);
            res.rotationType = RotationType.LEFT;
            res.groupType = GroupType.TWO;
            max = (max.value > res.value) ? max : res;

            res.value = field.tryRotate(GroupType.ONE, RotationType.RIGHT, i, j);
            res.rotationType = RotationType.RIGHT;
            res.groupType = GroupType.TWO;
            max = (max.value > res.value) ? max : res;

            res.value = field.tryRotate(GroupType.ONE, RotationType.RIGHT, i, j);
            res.rotationType = RotationType.RIGHT;
            res.groupType = GroupType.ONE;
            max = (max.value > res.value) ? max : res;

            return (max.value > curMax.value) ? max : curMax;
        }
    }
}
