using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApp.Data.Models;

namespace TaskApp.Services
{
    //should be in contrl
    public class MeetingNotFoundException : Exception 
    {
           public MeetingNotFoundException() { }
    }
    public class MeetingFullException : Exception
    {
        public MeetingFullException() { }
    }
    public class MeetingService
    {
        private readonly IMongoCollection<Meeting> _meetings;

        public MeetingService(ITaskAppDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _meetings = database.GetCollection<Meeting>(settings.CollectionName);
        }

        public List<Meeting> Get() =>
            _meetings.Find(meeting => true).ToList();

        public Meeting Get(string id) =>
            _meetings.Find<Meeting>(meeting => meeting.Id == id).FirstOrDefault();

        public Meeting Create(Meeting meeting)
        {
            _meetings.InsertOne(meeting);
            return meeting;
        }

        //public void Update(string id, Meeting meetingIn) =>
        //    _meetings.ReplaceOne(meeting => meeting.Id == id, meetingIn);

        public void Update(string id, Member attendee) {
            var meeting = _meetings.Find<Meeting>(meeting => meeting.Id == id).FirstOrDefault();
            if(meeting == null)
            {
                throw new MeetingNotFoundException();
            }
            if(meeting.Attendees.Count >= 24)
            {
                throw new MeetingFullException();
            }
            var filter = Builders<Meeting>.Filter.Where(meeting => meeting.Id == id && meeting.Attendees.Count <= 24);
            var update = Builders<Meeting>.Update.Push("Attendees", attendee);
            _meetings.FindOneAndUpdate(filter, update);
        }

        public void Remove(Meeting meetingIn) =>
            _meetings.DeleteOne(meeting => meeting.Id == meetingIn.Id);

        public void Remove(string id) =>
            _meetings.DeleteOne(meeting => meeting.Id == id);
    }
}
