#nullable enable

using System.CommandLine;

namespace VoyageAI.CLI.Commands;

internal static class ApiCommand
{
    public static Command Create()
    {
        var command = new Command("api", "Generated endpoint commands.");

                         command.Subcommands.Add(EndpointsApiGroupCommand.Create());
        return command;
    }
}