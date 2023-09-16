using Rinha.Interfaces;

namespace Rinha.Commands.Manager;

internal class CommandManager
{
    private Stack<ICompilerCommand> _commands = new();

    public void Invoke(ICompilerCommand command)
    {
        if (command.CanExecute())
        {
            _commands.Push(command);

            command.Execute();
        }
    }
}
