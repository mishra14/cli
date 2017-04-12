// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using Microsoft.DotNet.Cli.CommandLine;
using Newtonsoft.Json.Linq;
using LocalizableStrings = Microsoft.DotNet.Tools.List.PackageReference.LocalizableStrings;

namespace Microsoft.DotNet.Cli
{
    internal static class ListPackageParser
    {
        public static Command ListPackage()
        {
            return Create.Command(
                "package",
                LocalizableStrings.AppFullName,
                CommonOptions.HelpOption(),
                Create.Option("--id",
                              LocalizableStrings.CmdPackageDescription,
                              Accept.ExactlyOneArgument()
                                    .With(name: LocalizableStrings.CmdPackageDescription)
                                    .ForwardAsSingle(o => $"--id {o.Arguments.Single()}")),
                Create.Option("-f|--framework",
                              LocalizableStrings.CmdFrameworkDescription,
                              Accept.ExactlyOneArgument()
                                    .With(name: LocalizableStrings.CmdFramework)
                                    .ForwardAsSingle(o => $"--framework {o.Arguments.Single()}")));
        }
    }
}
