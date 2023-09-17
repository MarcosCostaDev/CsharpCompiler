using Rinha.Commands.Abstracts;

namespace Rinha.Commands
{
    internal class ParserCommand : AbstractCommand
    {
        private string _rinhaFile;
        private string _outputDirOptionValue;

        public ParserCommand(string rinhaFile, string outputDirOptionValue)
        {
            _rinhaFile = rinhaFile;
            _outputDirOptionValue = string.IsNullOrEmpty(outputDirOptionValue) ? Path.Combine(Directory.GetCurrentDirectory(), "Outputs") : outputDirOptionValue;
        }

        public override bool CanExecute()
        {
            var exist = File.Exists(_rinhaFile);
            if (!exist)
            {
                Console.WriteLine($"The file {_rinhaFile} don't exist");
            }

            if (!Directory.Exists(_outputDirOptionValue))
                 Directory.CreateDirectory(_outputDirOptionValue);


            return exist;
        }
        public override void Execute()
        {

            if (!Directory.Exists(_outputDirOptionValue))
                Directory.CreateDirectory(_outputDirOptionValue);

            var outputFile = Path.GetFileNameWithoutExtension(_rinhaFile);
            outputFile = string.Concat(outputFile, ".json");
        }
    }
}
