using System;
using System.Collections.Generic;
using System.Text;

namespace AccessSchoolRegister.Model
{
    public interface IRegisterStateHandler
    {
        void SaveState();

        void LoadState();
    }
}
