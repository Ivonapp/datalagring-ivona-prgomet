using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Domain.Entities.ValueObjects;

public sealed record Email
{

    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email is required.", nameof(value));

        var trimmed = value.Trim();

        if (!trimmed.Contains('@') || trimmed.StartsWith('@') || trimmed.EndsWith('@'))
            throw new ArgumentException("Invalid Email format.", nameof(value));

        Value = trimmed.ToLowerInvariant();
    }
    public override string ToString() => Value;
}