namespace RinhaCompiler.Interfaces;

internal interface ICompilerCommand
{
    Task ExecuteAsync(CancellationToken cancellationToken = default);
    bool CanExecute();
}
