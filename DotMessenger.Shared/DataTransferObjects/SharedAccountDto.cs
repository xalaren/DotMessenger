﻿namespace DotMessenger.Shared.DataTransferObjects
{
    public class SharedAccountDto
    {
        public int Id { get; set; }
        public string Nickname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
