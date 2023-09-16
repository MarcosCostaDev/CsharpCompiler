using Rinha.Interfaces;

namespace Rinha.Commands.Abstracts;

internal abstract class AbstractCommand : ICompilerCommand
{
    public virtual bool CanExecute() => true;

    public abstract void Execute();

}
