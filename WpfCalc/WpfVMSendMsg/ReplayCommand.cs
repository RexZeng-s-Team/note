using System;
using System.Windows.Input;

namespace WpfVMSendMsg
{
    public class ReplayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private Action? _execute;
        private Func<bool>? _canexecute;

        public ReplayCommand(Action? execute, Func<bool>? canexecute = null)
        {
            _execute = execute;
            _canexecute = canexecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (_canexecute == null)
            {
                return true;
            }
            return _canexecute();
        }

        public void Execute(object? parameter)
        {
            _execute!();
        }
    }
    public class ReplayCommand<T> : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private Action<T>? _execute;
        private Func<T, bool>? _canexecute;

        public ReplayCommand(Action<T>? execute, Func<T, bool>? canexecute = null)
        {
            _execute = execute;
            _canexecute = canexecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (_canexecute == null)
            {
                return true;
            }
            return _canexecute((T)parameter!);
        }

        public void Execute(object? parameter)
        {
            _execute((T)parameter!);
        }
    }
}
