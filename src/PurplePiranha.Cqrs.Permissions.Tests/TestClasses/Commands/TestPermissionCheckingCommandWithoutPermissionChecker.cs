﻿using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Permissions.Decorators;

namespace PurplePiranha.Cqrs.Permissions.Tests.TestClasses.Commands;

public record TestPermissionCheckingCommandWithoutPermissionChecker(int A) : ICommand, IPermissionRequired
{
}
