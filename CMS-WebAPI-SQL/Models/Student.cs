﻿namespace CMS_WebAPI_SQL.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Status { get; set; }
    }
}
