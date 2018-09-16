using System;

namespace OWRankTracker.Services
{
    internal class ProfileNameConflictException : Exception
    {
        public ProfileNameConflictException(string profileName) : base($"A profile with the name '{profileName}' already exists")
        {
        }
    }
}