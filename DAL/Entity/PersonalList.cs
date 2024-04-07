﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public enum State
    {
        Запланировано,
        Смотрю,
        Просмотрено,
        Отложено,
        Брошено
    }
    public class PersonalList
    {
        public int Id { get; set; }
        public int AnimeId { get; set; }
        public Anime Anime { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public State State { get; set; }
    }
}
