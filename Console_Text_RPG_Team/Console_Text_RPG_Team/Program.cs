namespace Console_Text_RPG_Team
{
    internal class Program
    {
        static void Main(string[] args)
        {
            JobSelect jobselect = new JobSelect();

            jobselect.Start();
            jobselect.Input();
            jobselect.sceneStatus.Start();
            jobselect.sceneStatus.Input();
		}
    }
}
