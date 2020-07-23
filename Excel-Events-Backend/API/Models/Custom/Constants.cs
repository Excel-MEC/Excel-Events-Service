namespace API.Models.Custom
{
    public static class Constants
    {
        public static string[] Category = {
            "non_tech",
            "computer_science",
            "general_tech"
        };
        public static string[] EventType =  {
            "general",
            "competition",
            "workshop",
            "talk",
            "conference"
        };
        public static string[] EventStatus = {
            "yet_to_start",
            "started",
            "finished"
        };

        public static string[] EventRounds = {
            "NA","prelims","mains","final"
        };
    }
}