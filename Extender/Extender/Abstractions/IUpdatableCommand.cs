using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
namespace Extender.Abstractions
{
    public interface IUpdatableCommand:ICommand
    {
        void ChangeCanExecute();
    }
}
