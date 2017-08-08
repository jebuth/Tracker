using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tracker2
{
	public class Routines
	{
		// Dummy routines
		private Routine Gorilla_Chest;
		private Routine Boulders;
		private Routine Legs;
		private ObservableCollection<Routine> Routine_List;

		public Routines()
		{
			Gorilla_Chest = new Routine("Gorilla Chest");
			Gorilla_Chest.Add_Workout("Bench Press", 4);
			Gorilla_Chest.Add_Workout("Incline", 4);
			Gorilla_Chest.Add_Workout("Decline", 4);
			Gorilla_Chest.Add_Workout("Fly", 4);

			Boulders = new Routine("Boulders");
			Boulders.Add_Workout("Shrugs", 4);
			Boulders.Add_Workout("Arnold Press", 4);

			Legs = new Routine("Legs");
			Legs.Add_Workout("Squat", 4);
			Legs.Add_Workout("Calf Raise", 4);

			Routine_List = new ObservableCollection<Routine>();
			Routine_List.Add(Gorilla_Chest);
			Routine_List.Add(Boulders);
			Routine_List.Add(Legs);
		}

		public void Add_Routine(string Routine_Name, List<Workout> Workouts)
		{

		}

		public ObservableCollection<Routine> Get_Routine_List()
		{
			return Routine_List;
		}
	}
}
