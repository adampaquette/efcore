// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Microsoft.EntityFrameworkCore.TestModels.ComplexTypeModel;

#nullable enable

public class Customer
{
    public int Id { get; set; }
    public required CustomerName Name { get; set; }

    public required Address ShippingAddress { get; set; }
    public required Address BillingAddress { get; set; }
}

public record CustomerName
{
    public string Value { get; }

    public CustomerName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ValidationException("CustomerName can't be empty.");
        }

        value = Normalize(value);

        if (value.Length > 100)
        {
            throw new ValidationException("CustomerName exceeds the maximum length of 100 characters.");
        }

        Value = value;
    }

    public static CustomerName From(string value)
        => new(value);

    public string Normalize(string value)
        => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value).Trim();
}

public record Address
{
    public required string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public int ZipCode { get; set; }

    public required Country Country { get; set; }
}

public record Country
{
    public required string FullName { get; set; }
    public required string Code { get; set; }
}

// Regular entity type referencing Customer, which is also a regular entity type.
// Used to test complex types on nullable/required entity types.
public class CustomerGroup
{
    public int Id { get; set; }

    public required Customer RequiredCustomer { get; set; }
    public Customer? OptionalCustomer { get; set; }
}

public class ValuedCustomer
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public required AddressStruct ShippingAddress { get; set; }
    public required AddressStruct BillingAddress { get; set; }
}

public record struct AddressStruct
{
    public required string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public int ZipCode { get; set; }

    public required CountryStruct Country { get; set; }
}

public record struct CountryStruct
{
    public required string FullName { get; set; }
    public required string Code { get; set; }
}

// Regular entity type referencing ValuedCustomer, which is also a regular entity type.
// Used to test complex types on nullable/required entity types.
public class ValuedCustomerGroup
{
    public int Id { get; set; }

    public required ValuedCustomer RequiredCustomer { get; set; }
    public ValuedCustomer? OptionalCustomer { get; set; }
}
