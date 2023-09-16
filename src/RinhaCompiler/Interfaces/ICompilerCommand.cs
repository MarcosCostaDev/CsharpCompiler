namespace Rinha.Interfaces;

internal interface ICompilerCommand
{
    void Execute();
    bool CanExecute();
}
