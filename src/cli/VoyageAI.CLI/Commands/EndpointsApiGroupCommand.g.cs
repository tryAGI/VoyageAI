#nullable enable

using System.CommandLine;

namespace VoyageAI.CLI.Commands;

internal static partial class EndpointsApiGroupCommand
{
    static partial void CustomizeCommand(ref Command command);

    public static Command Create()
    {
        var command = new Command(@"endpoints", @"Endpoints endpoint commands.");
                         command.Subcommands.Add(EndpointsEmbeddingsApiCommandApiCommand.Create());
                         command.Subcommands.Add(EndpointsMultimodalEmbeddingsApiCommandApiCommand.Create());
                         command.Subcommands.Add(EndpointsRerankerApiCommandApiCommand.Create());
        CustomizeCommand(ref command);
        return command;
    }
}