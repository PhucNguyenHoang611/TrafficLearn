﻿namespace webapi.Services.PasswordHasher
{
    public interface IPasswordHasher
    {
        string Hash(string password);

        bool Verify(string passwordHash, string inputPassword);
    }
}