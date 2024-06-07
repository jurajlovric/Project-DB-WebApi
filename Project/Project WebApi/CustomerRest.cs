﻿namespace Project.WebApi
{
    public class CustomerRest
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
    }
}
