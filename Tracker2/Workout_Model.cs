using System;
using SQLite;
namespace Tracker2
{
    public class Workout_Model
    {
        [PrimaryKey]
        public string name { get; set; }

        //[MaxLength(500)]
        public string routine { get; set; }

        //[MaxLength(500)]
        public string weight { get; set; }

        //[MaxLength(500)]
        public string reps { get; set; }
    }

}
