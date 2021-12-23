using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ivanti.Manager.Contracts
{
    public interface ITriangleManager
    {
        List<int[]> GetCoordinates(string input);
        string GetTriangle(List<int[]> coordinates);
    }
}
