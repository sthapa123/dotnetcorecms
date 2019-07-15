using dotnetcorepms.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnetcorepms.Interfaces
{
    public interface ICommon
    {
        List<PairModel> GetPairModel(string args, long id = 0);
        List<PairModel> GetPairModelWithDefault(string args, long id = 0);
    }
 }
