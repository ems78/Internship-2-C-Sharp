using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvjetskoPrvenstvo
{
    public sealed class stringEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string? a, string? b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (ReferenceEquals(a, null)) return false;
            if (ReferenceEquals(b, null)) return false;
            if (a.GetType() != b.GetType()) return false;
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }
        public int GetHashCode(string a)
        {
            return (a != null ? a.GetHashCode() : 0);
        }
    }
}
