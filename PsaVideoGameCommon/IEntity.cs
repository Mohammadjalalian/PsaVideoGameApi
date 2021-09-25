using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsaVideoGameCommon
{
  public interface IEntity
  {
     int Id { get; set; }
     DateTime CreationTime { get; set; }
     DateTime  ModificationTime { get; set; }
  }
}
