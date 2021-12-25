using Ivanti.Constants;
using Ivanti.Entities;
using Ivanti.Manager.Contracts;
using Ivanti.Utility;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Ivanti.Manager
{
    public class TriangleService : ITriangleService
    {
        private readonly int _triangleSideLength;
        public TriangleService(IConfiguration configuration)
        {
            _triangleSideLength = configuration.GetValue<int>(Configuration.LengthOfTriangleSide);
            if (_triangleSideLength == 0)
            {
                throw new ArgumentException("Length of triangle side must be greater than 0");
            }
        }
        private bool IsLowerTriangle(int triangleNumber)
        {
            if (triangleNumber % 2 == 0)
            {
                return false;
            }

            return true;
        }
        private bool IsValidRowNumber(char row)
        {
            if (row < 'A' || row > 'F')
            {
                return false;
            }

            return true;
        }
        private bool IsValidTriangleNumber(int triangleNumber)
        {
            if (triangleNumber <= 0 || triangleNumber > 12)
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
            string block = input.Substring(1);
            int triangleNumber = Convert.ToInt32(block);

            if (!IsValidRowNumber(row) || !IsValidTriangleNumber(triangleNumber))
            {
                return result;
            }

            int rowNumber = row - 'A';
            int colNumber = (triangleNumber - 1) / 2;

            int r1 = rowNumber * _triangleSideLength;
            int r2 = rowNumber * _triangleSideLength + _triangleSideLength;

            int c1 = colNumber * _triangleSideLength;
            int c2 = colNumber * _triangleSideLength + _triangleSideLength;

            result.Add(new int[] { c1, r1 });
            result.Add(new int[] { c2, r2 });

            if (IsLowerTriangle(triangleNumber))
            {
                result.Add(new int[] { c1, r2 });
            }
            else
            {
                result.Add(new int[] { c2, r1 });
            }

            return result;
        }
        public TriangleResponse GetTriangle(List<int[]> coordinates)
        {
            var response = new TriangleResponse { 
                IsValid = false,
                Value = Messages.CheckCoordinatesMessage
            };

            if (coordinates.Count < 3 || !TriangleUtility.IsValidRighAngledTriangle(coordinates))
            {
                return response;
            }

            string triangle = string.Empty;

            int maxX = 0;
            int maxY = 0;
            int minX = Int32.MaxValue;
            int minY = Int32.MaxValue;

            var frequency = new Dictionary<int, int>();

            for (int i = 0; i < 3; i++)
            {
                maxX = Math.Max(maxX, coordinates[i][0]);
                maxY = Math.Max(maxY, coordinates[i][1]);

                minX = Math.Min(minX, coordinates[i][0]);
                minY = Math.Min(minY, coordinates[i][1]);

                if (!frequency.ContainsKey(coordinates[i][0]))
                {
                    frequency.Add(coordinates[i][0], 0);
                }
                frequency[coordinates[i][0]] = frequency[coordinates[i][0]] + 1;
            }

            char row = Convert.ToChar('A' + minY / _triangleSideLength);

            if (!IsValidRowNumber(row))
            {
                return response;
            }

            int col = minX / _triangleSideLength;
            int triangleNum = 0;

            // If two points having same minimum value of x coordinate then the triangle points upwards else downwards.
            if (frequency[minX] == 2)
            {
                triangleNum = col * 2 + 1;
            }
            else
            {
                triangleNum = col * 2 + 2;
            }

            if (!IsValidTriangleNumber(triangleNum))
            {
                return response;
            }

            response.IsValid = true;
            response.Value = $"{row}{triangleNum}";
            return response;
        }
    }
}
