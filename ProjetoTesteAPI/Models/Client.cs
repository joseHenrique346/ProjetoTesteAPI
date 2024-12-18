﻿using System.Text.Json.Serialization;

namespace ProjetoTesteAPI.Models
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Phone { get; set; }

        public string Role { get; set; }

        [JsonIgnore]
        public List<Order>? Order { get; set; }

        public string PasswordHash { get; set; }
    }
}