using System;
using System.Collections.Generic;
using System.Text;

namespace TaskApp.Data.Models
{
    public class TaskAppDbSettings : ITaskAppDbSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface ITaskAppDbSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
