using RinhaCompiler.Interfaces;

namespace RinhaCompiler.Commands.Manager;

internal class CommandManager
{
    private Stack<ICompilerCommand> _commands = new();

    public void Invoke(ICompilerCommand command)
    {
        if (command.CanExecute())
        {
            _commands.Push(command);
            _ = Task.Run(async () =>
             {
                 await command.ExecuteAsync();
             });
        }
    }
}
