using System;
using System.Collections.Generic;
namespace Tracker2
{
    public class Routine
    {
        private string name;
        private List<Workout> workouts;

        public Routine(string name)
        {
            this.name = name;
            workouts = new List<Workout>();
        }

        public List<Workout> Get_Workout_List(){
            return this.workouts;
        }

        public void Add_Workout(string Workout_Name, byte sets){
            workouts.Add(new Workout(Workout_Name, sets));
        }

        public string Get_Routine_Name(){
            return name;
        }
    }
}
