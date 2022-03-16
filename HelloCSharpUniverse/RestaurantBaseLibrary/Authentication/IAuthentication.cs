using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BaseLibrary
{
    public interface IAuthentication
    {
        bool CheckUser(IUser user);
        bool GranAccess(IUser user);
        bool ReportIllegalAccessAttempt();
    }
}
