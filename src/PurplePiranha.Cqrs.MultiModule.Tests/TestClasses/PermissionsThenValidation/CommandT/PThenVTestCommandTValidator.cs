﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Extra.Tests.TestClasses.PermissionsThenValidation.CommandT;

public class PThenVTestCommandTValidator : AbstractValidator<PThenVTestCommandT>
{
    public PThenVTestCommandTValidator()
    {
        // Ensure that validator isn't called before permission checker
        RuleFor(c => c).Must(x =>
        {
            Assert.That(x.SpecialNumber, Is.Not.EqualTo(100));
            return true;
        });

        RuleFor(c => c.SpecialNumber).NotEqual(200);
    }
}
