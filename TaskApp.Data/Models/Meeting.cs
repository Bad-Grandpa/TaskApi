using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskApp.Data.Models
{
    public class Meeting
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Meeting_Name { get; set; }

        public DateTime Meeting_Date { get; set; }

        public List<Member> Attendees { get; set; }
    }
}
