using System.Diagnostics.CodeAnalysis;

namespace Ivanti.Entities
{
    [ExcludeFromCodeCoverage]
    public class TriangleResponse
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
}
