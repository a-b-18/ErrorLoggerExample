
namespace Sample_Cs
{
    public class PersonReview
    {
        private readonly string _fileName;
        private readonly int _fileLine;
        public readonly bool FoundErrors = false;
        public readonly List<ErrorLog> Errors = new List<ErrorLog>();

        public PersonReview(string[] entries, string fileName, int fileLine)
        {
            // Assign logging variables
            _fileName = fileName;
            _fileLine = fileLine;
            
            // Assign check-specific variables
            string name = entries[0];
            int age = Convert.ToInt16(entries[1]);
            
            // Call checks to be done
            CheckName(name);
            CheckAge(age);
            
            // Check if errors were logged
            if (Errors.Count > 0) { FoundErrors = true; }
        }

        private void CheckName(string name)
        {
            if (name != "Tony") return;
            
            Errors.Add(new ErrorLog {ErrorMessage = $"file {_fileName} on line {_fileLine} has {name}, which is a bad name.", IsCritical = false});
        }

        private void CheckAge(int age)
        {
            if (age <= 30) return;
            
            Errors.Add(new ErrorLog {ErrorMessage = $"file {_fileName} on line {_fileLine} has {age}, which way too old.", IsCritical = true});
        }
        
    }

    public class ErrorLog
    {
        public string ErrorMessage;
        public bool IsCritical;
    }

    public static class Program
    {
        public static void Main()
        {
            string fileName = "theFileXXX//wat//";
            
            List<string[]> personList = new List<string[]>()
            {
                new string[] {"Alex", "29"},
                new string[] {"Tony", "11"},
                new string[] {"Fred", "31"},
                new string[] {"Bud", "1"},
            };

            for (var i = 0; i < personList.Count; i++)
            {
                var person = personList[i];
                
                var personReview = new PersonReview(person, fileName, i + 1);
                
                if (personReview.FoundErrors)
                {
                    foreach (var errorLog in personReview.Errors)
                    {
                        switch (errorLog.IsCritical)
                        {
                            case true:
                                Console.WriteLine("CRITICAL error: " + errorLog.ErrorMessage);
                                break;
                            case false:
                                Console.WriteLine("Non-critical error: " + errorLog.ErrorMessage);
                                break;
                        }
                    }
                }
            }
        }
    }
}