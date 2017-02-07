using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Domain.Interfaces
{
  public interface iTestPersist<T>
  {
    T Oid { get; set; }
  }
}
 