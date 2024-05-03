﻿using System.ComponentModel.DataAnnotations;

namespace AnimeStar.Models
{
    public class MpaaViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}
