using System;
using Xamarin.Forms;
namespace Tracker2
{
    public class Workout_Cell_Odd : ViewCell
    {
        //private ViewCell View_Cell1;
        private StackLayout Cell_Wrapper;
        //private StackLayout Inner_Wrapper;
        private Label Workout_Name;
        private Entry Workout_Name_Entry;
        /*
        */
        public Workout_Cell_Odd()
        {
            
            //View_Cell1 = new ViewCell();
            Cell_Wrapper = new StackLayout();
            //Inner_Wrapper = new StackLayout();
            Workout_Name = new Label();
            Workout_Name_Entry = new Entry();

            Cell_Wrapper.Orientation = StackOrientation.Horizontal;
            Cell_Wrapper.Padding = new Thickness(12, 0, 12, 0);
            Workout_Name.Text = "Workout Name:";
            Workout_Name.VerticalOptions = LayoutOptions.Center;
            Workout_Name_Entry.Placeholder = "(eg Deadlift)";
            //Workout_Name_Entry.Text = "yeah";
            Workout_Name_Entry.HorizontalOptions = LayoutOptions.FillAndExpand;
            Workout_Name_Entry.VerticalOptions = LayoutOptions.Center;
            Workout_Name_Entry.Keyboard = Keyboard.Text;
            Cell_Wrapper.Children.Add(Workout_Name);
            Cell_Wrapper.Children.Add(Workout_Name_Entry);

            this.View = Cell_Wrapper;
        }

        public override string ToString()
        {
            return string.Format("[Workout_Cell_Odd]");
        }

        public string Get_Info()
        {
            return this.Workout_Name_Entry.Text.ToString();
        }

        public Workout_Cell_Odd Get(){
            return this;
        }
    }
}
