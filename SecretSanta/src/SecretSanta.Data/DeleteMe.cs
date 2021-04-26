using System.Collections.Generic;

namespace SecretSanta.Data
{
    public static class DeleteMe
    {
        public static List<User> Users { get; } = new()
        {
            new User() {Id = 1, FirstName = "LeBron", LastName = "James"},
            new User() {Id = 2, FirstName = "Kobe", LastName = "Bryant"},
            new User() {Id = 3, FirstName = "Michael", LastName = "Jordan"}
        };
    }
}