using Ivanti.Entities;
using System.Collections.Generic;

namespace Ivanti.Manager.Contracts
{
    public interface ITriangleManager
    {
        List<int[]> GetCoordinates(string input);
        TriangleResponse GetTriangle(List<int[]> coordinates);
    }
}
