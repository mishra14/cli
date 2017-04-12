// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.DotNet.Cli;
using Microsoft.DotNet.Cli.CommandLine;
using Microsoft.DotNet.Cli.Utils;
using Microsoft.DotNet.Tools.MSBuild;
using Microsoft.DotNet.Tools.NuGet;

namespace Microsoft.DotNet.Tools.List.PackageReferences
{
    internal class ListPackageReferencesCommand : CommandBase
    {
        private readonly string _fileOrDirectory;
        private readonly AppliedOption _appliedCommand;

        public ListPackageReferencesCommand(
            AppliedOption appliedCommand,
            ParseResult parseResult) : base(parseResult)
        {
            if (appliedCommand == null)
            {
                throw new ArgumentNullException(nameof(appliedCommand));
            }

            _fileOrDirectory = appliedCommand.Arguments.Single();
            _appliedCommand = appliedCommand;
        }

        public override int Execute()
        {
            var projectFilePath = string.Empty;

            if (!File.Exists(_fileOrDirectory))
            {
                projectFilePath = MsbuildProject.GetProjectFileFromDirectory(_fileOrDirectory).FullName;
            }
            else
            {
                projectFilePath = _fileOrDirectory;
            }

            var result = NuGetCommand.Run(TransformArgs(projectFilePath));

            return result;
        }

        private string[] TransformArgs(string projectFilePath)
        {
            var args = new List<string>
            {
                "package",
                "list",
                "--project",
                projectFilePath
            };

            args.AddRange(_appliedCommand
                .OptionValuesToBeForwarded()
                .SelectMany(a => a.Split(' ')));

            return args.ToArray();
        }
    }
}