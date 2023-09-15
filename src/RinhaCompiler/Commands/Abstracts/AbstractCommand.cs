using RinhaCompiler.Interfaces;

namespace RinhaCompiler.Commands.Abstracts;

internal abstract class AbstractCommand : ICompilerCommand
{
    public virtual bool CanExecute() => true;

    public abstract Task ExecuteAsync(CancellationToken cancellationToken = default);

}
