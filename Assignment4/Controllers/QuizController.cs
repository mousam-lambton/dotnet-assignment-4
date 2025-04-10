using Assignment4.Helpers;
using Assignment4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment4.Controllers
{
    public class QuizController : Controller
    {
        private const string SessionKeyQuestions = "Questions";
        private const string SessionKeyAnswers = "Answers";
        private readonly QuizContext _context;

        public QuizController(QuizContext context)
        {
            _context = context;
        }

        // shuffle for randomizing the list
        private List<Question> ShuffleQuestions(List<Question> questions)
        {
            Random rng = new Random();
            int n = questions.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Question value = questions[k];
                questions[k] = questions[n];
                questions[n] = value;
            }
            return questions;
        }

        public IActionResult Start()
        {
            HttpContext.Session.Clear();

            // Fetch all questions from database
            var allQuestions = _context.Questions.ToList();

            // Shuffle the questions list randomly
            var randomQuestions = ShuffleQuestions(allQuestions).Take(10).ToList();

            // Store the randomized questions in the session
            HttpContext.Session.SetObject(SessionKeyQuestions, randomQuestions);

            // Reset answers for a new session
            HttpContext.Session.SetObject(SessionKeyAnswers, new Dictionary<int, int?>());

            return RedirectToAction("Question", new { index = 0 });  // Start quiz from first question
        }

        // Displays a single question based on index
        public IActionResult Question(int index)
        {
            var questions = HttpContext.Session.GetObject<List<Question>>(SessionKeyQuestions);
            var answers = HttpContext.Session.GetObject<Dictionary<int, int?>>(SessionKeyAnswers);

            if (questions == null || answers == null)
                return RedirectToAction("Start");

            // Pass index and selected answer (if any) to the view
            ViewBag.Index = index;
            ViewBag.SelectedOption = answers.ContainsKey(index) ? answers[index] : null;

            return View(questions[index]);  // Return the current question to the view
        }

        // Handles the selected answer submission
        [HttpPost]
        public IActionResult Answer(int index, int selectedOption)
        {
            var answers = HttpContext.Session.GetObject<Dictionary<int, int?>>(SessionKeyAnswers);

            if (answers != null)
            {
                answers[index] = selectedOption;  // Store selected answer
                HttpContext.Session.SetObject(SessionKeyAnswers, answers);
            }

            // If it's the last question (index 9), show the result
            if (index == 9)
            {
                return RedirectToAction("Result");
            }

            // Move to the next question
            return RedirectToAction("Question", new { index = index + 1 });
        }

        // Displays the result of the quiz and saves to database
        public IActionResult Result()
        {
            var questions = HttpContext.Session.GetObject<List<Question>>(SessionKeyQuestions);
            var answers = HttpContext.Session.GetObject<Dictionary<int, int?>>(SessionKeyAnswers);

            // If session data is missing (quiz not started), redirect to start
            if (questions == null || answers == null) return RedirectToAction("Start");

            // Calculate score by comparing answers to the correct answer index
            int score = questions.Select((q, i) => answers.ContainsKey(i) && answers[i] == q.CorrectAnswerIndex ? 1 : 0).Sum();

            // Save the result to database
            var quizResult = new QuizResult
            {
                Score = score,
                CompletedAt = DateTime.Now
            };

            _context.QuizResults.Add(quizResult);
            _context.SaveChanges();

            // Display score and result message
            ViewBag.Score = score;
            ViewBag.Message = score >= 8 ? "You have successfully passed the test." : "Unfortunately, you did not pass the test. Please try again later!";

            return View();  // Show result to the user
        }
    }
}