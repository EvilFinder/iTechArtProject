﻿using System;

namespace iTechArt.SurveysSite.DomainModel
{
    public class User
    {
        public const int MinUserNameLength = 5;
        public const int MaxUserNameLength = 20;
        public const int MinEmailLength = 10;
        public const int MaxEmailLength = 35;
        public const int MinPasswordLength = 8;
        public const int MaxPasswordLength = 20;


        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string NormalizedUserName { get; set; }

        public Role Role { get; set; }

        public DateTime DateOfRegistration { get; set; }
    }
}