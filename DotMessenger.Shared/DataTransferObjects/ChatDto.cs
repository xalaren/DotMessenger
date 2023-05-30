﻿namespace DotMessenger.Shared.DataTransferObjects
{
    public class ChatDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string CreatedAt { get; set; } = null!;
    }
}
