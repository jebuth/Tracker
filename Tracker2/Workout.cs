using System;
using System.Collections.Generic;
namespace Tracker2
{
    public class Workout
    {
        private string Name { get; set; }
        private List<Set> Set_List;
        private byte Sets;
        
        public Workout(string Name, byte Sets)
        {
            this.Name = Name;
            this.Sets = Sets;

            Set_List = new List<Set>();
            for (int i = 0; i < Sets; i++){
                Set_List.Add(new Set());
            }
        }

        public List<Set> Get_Set_List(){
            return this.Set_List;
        }

        public string Get_Name(){
            return this.Name; 
        }
        public byte Get_Sets(){
            return this.Sets;
        }
    }
}
