using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskApp.Data.Models
{
    public class Member
    {
        [BsonElement("Name")]
        public string Member_Name { get; set; }
        public string Email { get; set; }
    }
}
