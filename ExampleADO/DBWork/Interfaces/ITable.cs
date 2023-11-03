using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleADO.DBWork
{
    public interface ITable
    {
        string Name { get; set; }
        int Num { get; set; }
    }
}
