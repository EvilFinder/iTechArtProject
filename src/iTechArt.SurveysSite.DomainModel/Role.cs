﻿using System.Collections.Generic;

namespace iTechArt.SurveysSite.DomainModel
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}