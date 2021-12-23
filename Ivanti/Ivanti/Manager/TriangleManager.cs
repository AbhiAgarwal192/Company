using Ivanti.Constants;
using Ivanti.Manager.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Ivanti.Manager
{
    public class TriangleManager : ITriangleManager
    {
        private readonly int _triangleSideLength;
        public TriangleManager(IConfiguration configuration)
        {
            _triangleSideLength = configuration.GetValue<int>(Configuration.LengthOfTriangleSide);
        }
        private bool IsLowerTriangle(int triangleNumber)
        {
            if (triangleNumber % 2 == 0)
            {
                return false;
            }

            return true;
        }
        public List<int[]> GetCoordinates(string input)
        {
            var result = new List<int[]>();
            if (input.Length < 2)
            {
                return result;
            }

            char row = input[0];

            if (row < 'A' || row > 'F')
            {
                return result;
            }

            string block = input.Substring(1);

            int triangleNumber = Convert.ToInt32(block);

            if (triangleNumber <= 0 || triangleNumber > 12)
            {
                return result;
            }

            int rowNumber = row - 'A';
            int colNumber = (triangleNumber - 1) / 2;

            int r1 = rowNumber * _triangleSideLength;
            int r2 = rowNumber * _triangleSideLength + _triangleSideLength;

            int c1 = colNumber * _triangleSideLength;
            int c2 = colNumber * _triangleSideLength + _triangleSideLength;

            int[] coordinate1 = { c1, r1 };
            int[] coordinate2 = { c2, r2 };

            result.Add(coordinate1);
            result.Add(coordinate2);

            if (IsLowerTriangle(triangleNumber))
            {
                int[] coordinate3 = { c1, r2 };
                result.Add(coordinate3);
            }
            else
            {
                int[] coordinate3 = { c2, r1 };
                result.Add(coordinate3);
            }

            return result;
        }
        public string GetTriangle(List<int[]> coordinates)
        {
            string triangle = string.Empty;

            if (coordinates.Count < 3)
            {
                return triangle;
            }

            int maxX = 0;
            int maxY = 0;
            int minX = Int32.MaxValue;
            int minY = Int32.MaxValue;

            var freq = new Dictionary<int, int>();

            for (int i = 0; i < 3; i++)
            {
                maxX = Math.Max(maxX, coordinates[i][0]);
                maxY = Math.Max(maxY, coordinates[i][1]);

                minX = Math.Min(minX, coordinates[i][0]);
                minY = Math.Min(minY, coordinates[i][1]);

                if (!freq.ContainsKey(coordinates[i][0]))
                {
                    freq.Add(coordinates[i][0], 0);
                }
                freq[coordinates[i][0]] = freq[coordinates[i][0]] + 1;
            }

            char row = Convert.ToChar('A' + minY / _triangleSideLength);

            int col = minX / _triangleSideLength;

            if (freq[minX] == 2)
            {
                triangle = $"{row}{col * 2 + 1}";
            }
            else
            {
                triangle = $"{row}{col * 2 + 2}";
            }

            return triangle;
        }
    }
}
