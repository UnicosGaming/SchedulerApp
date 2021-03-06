﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerApp.Models
{
    public class TeamSchedule: Schedule
    {
        private string _opponent;
        public string Opponent
        {
            get => _opponent;
            set => SetProperty(ref _opponent, value);
        }

        public TeamSchedule() : base() { }
        public TeamSchedule(string id) : base(id) { }
    }
}
