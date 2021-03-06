﻿using NugetAnalyzer.DTOs.Models;
using NugetAnalyzer.Domain;
using System.Collections.Generic;

namespace NugetAnalyzer.DTOs.Converters
{
    public class UserConverter
    {
        public User ConvertDTOToUser(UserDTO profile)
        {
            return profile == null
                ? null
                : new User
                {
                    UserName = profile.UserName,
                    Email = profile.Email,
                    AvatarUrl = profile.AvatarUrl,
                    Id = profile.Id
                };
        }

        public UserDTO ConvertUserToDTO(User user)
        {
            return user == null
                ? null
                : new UserDTO
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    AvatarUrl = user.AvatarUrl,
                    Id = user.Id
                };
        }

        public User ConvertRegisterModelToUser(UserRegisterModel user)
        {
            if (user == null)
            {
                return null;
            }

            var newUser = new User
            {
                AvatarUrl = user.AvatarUrl,
                Email = user.Email,
                UserName = user.UserName,
                Profiles = new List<Profile>()
            };

            var profile = new Profile
            {
                AccessToken = user.AccessToken,
                ExternalId = user.ExternalId,
                SourceId = user.SourceId,
                Url = user.Url,
                User = newUser
            };

            newUser.Profiles.Add(profile);

            return newUser;
        }
    }
}
