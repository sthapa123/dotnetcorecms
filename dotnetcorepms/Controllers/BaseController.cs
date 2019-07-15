using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetcorepms.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcorepms.Controllers
{
    public class BaseController : Controller
    {
        public enum TOSTRMSGTYPE
        {
            SUCCESS,
            ERROR,
            WARNING,
            OTHER
        };

        public enum ROLE
        {
            SUPER_USER = 1
        }

        public PairModel GetToStrJsonData(TOSTRMSGTYPE MsgType, string msg)
        {
            switch (MsgType)
            {
                case TOSTRMSGTYPE.SUCCESS:
                    return new PairModel { Key = "Success", Value = msg };
                case TOSTRMSGTYPE.WARNING:
                    return new PairModel { Key = "Warning", Value = msg };
                case TOSTRMSGTYPE.ERROR:
                    return new PairModel { Key = "Error", Value = msg };
                default:
                    return new PairModel { Key = "Info", Value = msg };
            }
        }
    }
}