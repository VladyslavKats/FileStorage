﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.DAL.Models
{
    public class Document
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public long Size { get; set; }

        public string Path { get; set; }

        public string? UserId { get; set; }

        public User  User { get; set; }
    }
}