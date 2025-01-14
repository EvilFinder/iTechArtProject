﻿using System;
using System.ComponentModel.DataAnnotations;
using iTechArt.SurveysSite.DomainModel;

namespace iTechArt.SurveysSite.WebApp.ViewModels
{
    public class SurveyViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter survey name")]
        [MaxLength(Survey.NameMaxLength, ErrorMessage = "The title is too big")]
        [MinLength(Survey.NameMinLength, ErrorMessage = "The title is too small")]
        [Display(Name = "New survey name: ")]
        public string Title { get; set; }

        public DateTime ChangeDate { get; set; }
    }
}