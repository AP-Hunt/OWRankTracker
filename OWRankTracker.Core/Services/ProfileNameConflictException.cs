using System;

namespace OWRankTracker.Core.Services
{
    public class ProfileNameConflictException : Exception
    {
        public ProfileNameConflictException(string message) : base($"Profile with name '${message}' already exists")
        {
        }
    }
}