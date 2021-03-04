using System.Collections.Generic;
using System.Threading.Tasks;

namespace FansyApp
{
  public interface IObData
  {
    Task<List<decimal>> GetDURest(int TreatyID);
  }
}