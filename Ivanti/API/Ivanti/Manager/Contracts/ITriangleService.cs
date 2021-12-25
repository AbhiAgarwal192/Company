using Ivanti.Entities;
using System.Collections.Generic;

namespace Ivanti.Manager.Contracts
{
    public interface ITriangleService
    {
        List<int[]> GetCoordinates(string input);
        TriangleResponse GetTriangle(List<int[]> coordinates);
    }
}
