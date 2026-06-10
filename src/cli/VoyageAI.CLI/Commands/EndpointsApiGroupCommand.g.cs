#nullable enable

using System.CommandLine;

namespace VoyageAI.CLI.Commands;

internal static class EndpointsApiGroupCommand
{
    public static Command Create()
    {
        var command = new Command(@"endpoints", @"Endpoints endpoint commands.");
                         command.Subcommands.Add(EndpointsEmbeddingsApiCommandApiCommand.Create());
                         command.Subcommands.Add(EndpointsMultimodalEmbeddingsApiCommandApiCommand.Create());
                         command.Subcommands.Add(EndpointsRerankerApiCommandApiCommand.Create());
        return command;
    }
}