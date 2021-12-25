using System;
using System.Collections.Generic;

namespace Ivanti.Utility
{
    public class TriangleUtility
    {
        /// <summary>
        /// Check if the coordinates form a valid right angled triangle using pythagoras theorem.
        /// </summary>
        /// <param name="coordinates">List of coordinates</param>
        /// <returns>bool</returns>
        public static bool IsValidRighAngledTriangle(List<int[]> coordinates)
        {
            int x1 = coordinates[0][0];
            int y1 = coordinates[0][1]; 
            int x2 = coordinates[1][0]; 
            int y2 = coordinates[1][1]; 
            int x3 = coordinates[2][0]; 
            int y3 = coordinates[2][1];

            int A = (int)(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));

            int B = (int)(Math.Pow((x3 - x2), 2) + Math.Pow((y3 - y2), 2));

            int C = (int)(Math.Pow((x3 - x1), 2) + Math.Pow((y3 - y1), 2));
            
            if ((A > 0 && B > 0 && C > 0) &&
                (A == (B + C) || B == (A + C) ||
                 C == (A + B)))
                return true;

            return false;
        }
    }
}
