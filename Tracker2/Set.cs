using System;
namespace Tracker2
{
    public class Set
    {
        private byte Count;
        private byte Reps;
        private float Weight;
        private float Total_Weight;

        public Set()
        {
            Count = 0;
            Reps = 0;
            Weight = 0;
            Total_Weight = 0;
        }

        public void Set_Count(byte Count)
        {
            this.Count = Count;
        }

        public byte Get_Count()
        {
            return this.Count;
        }

		public void Set_Reps(byte Reps)
		{
			this.Reps = Reps;
		}

		public byte Get_Reps()
		{
			return this.Reps;
		}

		public void Set_Weight(float Weight)
		{
            this.Weight = Weight;
		}

		public float Get_Weight()
		{
			return this.Weight;
		}

		public void Set_Total_Weight(float Total_Weight)
		{
			this.Total_Weight = Total_Weight;
		}

		public float Get_Total_Weight()
		{
			return this.Total_Weight;
		}
    }
}
