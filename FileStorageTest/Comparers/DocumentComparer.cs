using FileStorage.DAL.Models;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace FileStorageTest.Comparers
{
    public class DocumentComparer : IEqualityComparer<Document>
    {
        public bool Equals([AllowNull] Document x, [AllowNull] Document y)
        {
            if (x == null || y == null)
            {
                return false;
            }
            if(x == y)
            {
                return true;
            }
            return x.Id == y.Id &&
                   x.Name == y.Name &&
                   x.Size == y.Size &&
                   x.UserId == y.UserId &&
                   x.ContentType == y.ContentType;
        }

        public int GetHashCode([DisallowNull] Document obj)
        {
            return obj.GetHashCode();
        }
    }
}
