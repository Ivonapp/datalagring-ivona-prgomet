using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Domain.Entities.ValueObjects;

public sealed record PhoneNumber
{

    public string Value { get; }

    public PhoneNumber(string value)
    {


        // KOLLAR SÅ DET INTE ÄR TOMT
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Phone number is required.", nameof(value));


        // TAR BORT MELLANRUM OCH BINDESTRECK:
        var normalized = value.Trim()
            .Replace(" ", "")
            .Replace("-", "");

        Value = normalized;
    }

    public override string ToString() => Value;

    }





