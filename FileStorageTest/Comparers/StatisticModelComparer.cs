using FileStorage.BLL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace FileStorageTest.Comparers
{
    public class StatisticModelComparer : IEqualityComparer<StatisticModel>
    {
        public bool Equals([AllowNull] StatisticModel x, [AllowNull] StatisticModel y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.UserName == y.UserName
                && x.UsedSpace == y.UsedSpace
                && x.MaxSpace == y.MaxSpace
                && x.Files == y.Files;

        }

        public int GetHashCode([DisallowNull] StatisticModel obj)
        {
            return obj.GetHashCode();
        }
    }
}
