using AccessSchoolRegister.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessSchoolRegister.Services
{
    public interface IRegisterStateHandler
    {
        bool SaveState(SchoolRegister register, out string errorMessage);

        bool LoadState();
    }
}
