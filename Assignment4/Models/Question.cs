namespace Assignment4.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public int CorrectAnswerIndex { get; set; }

        // Helper method to get options as a list
        public List<string> GetOptions()
        {
            return new List<string> { Option1, Option2, Option3, Option4 };
        }
    }
}