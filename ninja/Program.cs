using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Extensions.CommandLineUtils;

namespace Ninja
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var app = new CommandLineApplication
                {
                    Name = "Ninja-tooling",
                    FullName = "Ninja .NET Core Tooling Utility",
                    Description = "Console app that runs many utilities",
                };

                app.Name = "ninja";
                app.HelpOption("-?|-h|--help");

                app.OnExecute(() =>
                {
                    app.ShowHelp();
                    return 2;
                });

                app.Command("hide", (command) =>
                {

                    command.Description = "Instruct the ninja to hide in a specific location.";
                    command.HelpOption("-?|-h|--help");

                    var outFileArgument = command.Option("-o|--out",
                                           "Output Log File",
                                           CommandOptionType.SingleValue);

                    var locationArgument = command.Argument("[location]",
                                               "Where the ninja should hide.");

                    command.OnExecute(() =>
                    {
                        var location = locationArgument.Value != null
                          ? locationArgument.Value
                          : "in a trash can";

                        var displayMessage = "Ninja is hidden " + location;

                        if(outFileArgument.HasValue()) File.WriteAllText(outFileArgument.Value(), displayMessage);
                        Console.WriteLine(displayMessage);

                        return 0;
                    });

                });

                app.Command("attack", (command) =>
                {
                    command.Description = "Instruct the ninja to go and attack!";
                    command.HelpOption("-?|-h|--help");

                    var excludeOption = command.Option("-e|--exclude <exclusions>",
                                            "Things to exclude while attacking.",
                                            CommandOptionType.MultipleValue);

                    var screamOption = command.Option("-s|--scream",
                                           "Scream while attacking",
                                           CommandOptionType.NoValue);

                    command.OnExecute(() =>
                    {
                        var exclusions = excludeOption.Values;
                        var attacking = (new List<string>
                                {
                                "dragons",
                                "badguys",
                                "civilians",
                                "animals"
                                }).Where(x => !exclusions.Contains(x));

                        Console.Write("Ninja is attacking " + string.Join(", ", attacking));

                        if (screamOption.HasValue())
                        {
                            Console.Write(" while screaming");
                        }

                        Console.WriteLine();

                        return 0;

                    });
                });

                app.Execute(args);

            } catch (Exception ex)
            {
                Console.WriteLine($"Error: {Environment.NewLine}{ex.Message}.");
                return 1;
            }

            //default return
            return 2;
        }
    }
}
