using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Assignment4.Models
{
    public static class DbInitializer
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<QuizContext>();
                context.Database.EnsureCreated();

                // Look for any questions
                if (context.Questions.Any())
                {
                    return; // DB has been seeded
                }

                var questions = new Question[]
                {
                    new Question { Text = "What does C# primarily run on?", Option1 = "JVM", Option2 = ".NET Framework", Option3 = "Python Interpreter", Option4 = "Ruby on Rails", CorrectAnswerIndex = 1 },
                    new Question { Text = "Which of the following is a value type in C#?", Option1 = "string", Option2 = "class", Option3 = "int", Option4 = "object", CorrectAnswerIndex = 2 },
                    new Question { Text = "Which C# keyword is used to define a constant?", Option1 = "static", Option2 = "readonly", Option3 = "const", Option4 = "final", CorrectAnswerIndex = 2 },
                    new Question { Text = "What access modifier makes a method available only within its class?", Option1 = "public", Option2 = "private", Option3 = "protected", Option4 = "internal", CorrectAnswerIndex = 1 },
                    new Question { Text = "What does LINQ stand for?", Option1 = "Language Integrated Normal Query", Option2 = "Language Integrated Nested Query", Option3 = "Language Integrated Network Query", Option4 = "Language Integrated Query", CorrectAnswerIndex = 3 },
                    new Question { Text = "Which collection type in C# allows you to access elements by key?", Option1 = "List<T>", Option2 = "Dictionary<K,V>", Option3 = "Array", Option4 = "Queue<T>", CorrectAnswerIndex = 1 },
                    new Question { Text = "What is the correct way to handle exceptions in C#?", Option1 = "try/catch", Option2 = "try/except", Option3 = "try/finally", Option4 = "if/else", CorrectAnswerIndex = 0 },
                    new Question { Text = "Which namespace is required to work with files in C#?", Option1 = "System.File", Option2 = "System.IO", Option3 = "System.FileStream", Option4 = "System.Data", CorrectAnswerIndex = 1 },
                    new Question { Text = "What does the 'var' keyword do in C#?", Option1 = "Creates a variable that can change type", Option2 = "Creates a variable with explicit type", Option3 = "Creates a variable with compiler-inferred type", Option4 = "Creates a variable-length array", CorrectAnswerIndex = 2 },
                    new Question { Text = "Which method is automatically called when an object is created?", Option1 = "Finalize()", Option2 = "Initialize()", Option3 = "Constructor", Option4 = "Main()", CorrectAnswerIndex = 2 },
                    new Question { Text = "What is the difference between \"ref\" and \"out\" parameters in C#?", Option1 = "They are the same", Option2 = "ref requires the variable to be initialized before passing", Option3 = "out requires the variable to be initialized before passing", Option4 = "ref is for input, out is for output only", CorrectAnswerIndex = 1 },
                    new Question { Text = "Which of the following is NOT a C# access modifier?", Option1 = "protected internal", Option2 = "private protected", Option3 = "public protected", Option4 = "internal", CorrectAnswerIndex = 2 },
                    new Question { Text = "Which C# feature allows a class to use the functionality of another class?", Option1 = "Encapsulation", Option2 = "Polymorphism", Option3 = "Inheritance", Option4 = "Abstraction", CorrectAnswerIndex = 2 },
                    new Question { Text = "What is the purpose of the 'using' statement in C#?", Option1 = "To import namespaces", Option2 = "To ensure proper resource disposal", Option3 = "To include external files", Option4 = "To define custom methods", CorrectAnswerIndex = 1 },
                    new Question { Text = "What does the 'async' keyword do in C#?", Option1 = "Makes a method run faster", Option2 = "Makes a method run on a separate thread", Option3 = "Marks a method for asynchronous operations", Option4 = "Creates multiple instances of a method", CorrectAnswerIndex = 2 },
                    new Question { Text = "Which .NET collection automatically sorts its elements?", Option1 = "List<T>", Option2 = "SortedList<K,V>", Option3 = "Dictionary<K,V>", Option4 = "Queue<T>", CorrectAnswerIndex = 1 },
                    new Question { Text = "What is a delegate in C#?", Option1 = "A type that references a method", Option2 = "A special class variable", Option3 = "A database connection", Option4 = "A type of exception", CorrectAnswerIndex = 0 },
                    new Question { Text = "Which of the following is NOT a valid C# loop?", Option1 = "for", Option2 = "foreach", Option3 = "loop", Option4 = "while", CorrectAnswerIndex = 2 },
                    new Question { Text = "What is the purpose of 'sealed' keyword in C#?", Option1 = "To encrypt a class", Option2 = "To prevent a class from being inherited", Option3 = "To make a class thread-safe", Option4 = "To make a class immutable", CorrectAnswerIndex = 1 },
                    new Question { Text = "What does the 'virtual' keyword allow in C#?", Option1 = "Method overloading", Option2 = "Method overriding in derived classes", Option3 = "Method hiding", Option4 = "Method encryption", CorrectAnswerIndex = 1 }
                };

                context.Questions.AddRange(questions);
                context.SaveChanges();
            }
        }
    }
}