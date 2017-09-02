using System;
using System.Collections.Generic;

namespace Tracker2
{
    public class DefaultRoutines
    {
        private List<Workouts_Table> WK = null;
        
        public DefaultRoutines()
        {
            WK = new List<Workouts_Table>();
            PopulateList();
        }

        public List<Workouts_Table> GetRoutineList(){
            return WK;
        }

        private void PopulateList()
        {
            WK.Add(new Workouts_Table
			{
				id = 1,
				routine_name = "gorilla chest",
				workout_name = "Bench",
				weight = "145, 145, 145, 145",
				reps = "12, 12, 12, 12",
				date = "8/9/017"
			});

			WK.Add(new Workouts_Table
			{
				routine_name = "gorilla chest",
				workout_name = "Bench",
				weight = "165, 165, 165, 165",
				reps = "12, 12, 10, 9",
				date = "8/9/017"
			});

			WK.Add(new Workouts_Table
			{
				routine_name = "gorilla chest",
				workout_name = "Bench",
				weight = "175, 175, 175, 175",
				reps = "12, 12, 10, 8",
				date = "8/9/017"
			});

			WK.Add(new Workouts_Table
			{
				routine_name = "gorilla chest",
				workout_name = "Bench",
				weight = "175, 175, 175, 175",
				reps = "12, 12, 11, 9",
				date = "8/10/017"
			});//8400

			WK.Add(new Workouts_Table
			{
				routine_name = "gorilla chest",
				workout_name = "Bench",
				weight = "185, 185, 185, 185",
				reps = "12, 12, 10, 10",
				date = "8/10/017"
			});

			WK.Add(new Workouts_Table
			{
				routine_name = "boulders",
				workout_name = "Arnold Press",
				weight = "100, 100, 100, 100",
				reps = "12, 12, 12, 12",
				date = "8/10/017"
			});
			WK.Add(new Workouts_Table
			{
				routine_name = "boulders",
				workout_name = "Overhead Press",
				weight = "100, 100, 100, 100",
				reps = "12, 12, 12, 12",
				date = "8/10/017"
			});
			WK.Add(new Workouts_Table
			{
				routine_name = "biceps",
				workout_name = "Curl",
				weight = "100, 100, 100, 100",
				reps = "12, 12, 12, 12",
				date = "8/10/017"
			});
			
            WK.Add(new Workouts_Table
			{
				routine_name = "biceps",
				workout_name = "Preacher Curl",
				weight = "100, 100, 100, 100",
				reps = "12, 12, 12, 12",
				date = "8/10/017"
			});
			
            WK.Add(new Workouts_Table
			{
				routine_name = "biceps",
				workout_name = "Hammer curl",
				weight = "100, 100, 100, 100",
				reps = "12, 12, 12, 12",
				date = "8/10/017"
			});
			
            WK.Add(new Workouts_Table
			{
				routine_name = "cobra back",
				workout_name = "lat pull",
				weight = "100, 100, 100, 100",
				reps = "12, 12, 12, 12",
				date = "8/10/017"
			});

			WK.Add(new Workouts_Table
			{
				routine_name = "legs",
				workout_name = "sumo",
				weight = "100, 100, 100, 100",
				reps = "12, 12, 12, 12",
				date = "8/10/017"
			});
        }
    }
}
