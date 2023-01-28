using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.DAL.Interfaces
{
    public interface IDatabaseInitializer
    {
        void Initialize(string passwordForAdmin = default(string));
    }
}
