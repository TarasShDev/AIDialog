using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIDialog.Entities;

namespace AIDialog
{
    class AISpeaker
    {
        DataProvider dataProvider = new DataProvider();
        Random random = new Random(DateTime.Now.Millisecond);
        List<string> userWords;
        string userPhrase;
        string bestAnswer = "";

        List<QuestionsWithCat> questions;
        List<AnswerWithCat> answers;
        List<string> words;
        List<Country> countries;
        List<string> abusiveWords;
        List<string> goodAnswers;
        List<string> badAnswers;

        public void StartCommunication()
        {
            InitVariables();

            Console.WriteLine("Hi, I'm your virtual chatterbox, tell me something.");

            while (true)
            {
                userPhrase = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(userPhrase) || userPhrase.Length < 2)
                {
                    TryOneMoreTime();
                }
                else
                {
                    if (ContainsAbusiveWords())
                    {
                        SayBye();
                        break;
                    }

                    SetUserWords();
                    if (!IsValidInput())
                        TryOneMoreTime();
                    else
                    {
                        if (userPhrase.EndsWith("?"))
                        {
                            bool successfulOccur = WriteAnswer();
                            Console.WriteLine(successfulOccur ? bestAnswer : GetRandomElem(goodAnswers));
                        }
                        else
                        {
                            Console.WriteLine(GetRandomElem(goodAnswers));
                            Console.WriteLine(GenerateQuestion());
                        }
                    }
                }               
            }

            Console.ReadKey();
        }

        private void SetUserWords()
        {
            string[] userWordsArray = userPhrase
                .ToLower()
                .Replace("!", " ")
                .Replace("?", " ")
                .Replace(", ", " ")
                .Replace(",", " ")
                .Replace(".", " ")
                .Trim()
                .Split(' ');
            userWords = new List<string>(userWordsArray);
        }

        private bool WriteAnswer()
        {
            string lowerCasePhrase = userPhrase.ToLower();
            if (lowerCasePhrase.Contains("capital"))
                return FindCapital(lowerCasePhrase);
            int mostCount = 0;
            int currentCount = 0;
            foreach(var answer in answers)
            {
                currentCount = 0;
                foreach(string word in answer.Words)
                {
                    if (lowerCasePhrase.Contains(word))
                        currentCount++;
                }

                if (currentCount > mostCount)
                {
                    mostCount = currentCount;
                    bestAnswer = answer.Answer;
                }
            }
            return mostCount>0;
        }

        private bool FindCapital(string lowerCasePhrase)
        {
            var res = userWords.Intersect(countries.Select(x => x.country.ToLower()));
            if (res.Any())
            {
                Country country = countries.First(x => x.country.ToLower() == res.First());
                bestAnswer = $"The capital of {country.country} is {country.capital}.";
                return true;
            }
            bestAnswer = "Can't understand your question.";
            return true;
        }

        private bool IsValidInput(double allowedPartOfUnknownWords = 0.33)
        {
            int totalWordsCount = userWords.Count;
            int unknownWordsCount = 0;

            foreach (string word in userWords)
                if (!words.Contains(word))
                    ++unknownWordsCount;

            double partOfUnknownWords = ((double)unknownWordsCount) / totalWordsCount;
            return allowedPartOfUnknownWords >= partOfUnknownWords;
        }

        private void TryOneMoreTime()
        {
            Console.WriteLine("Let's try one more time.");
        }

        private string GenerateQuestion()
        {
            var qColl = questions[random.Next(questions.Count)].Questions;
            return qColl[random.Next(qColl.Count)];
        }

        private bool ContainsAbusiveWords(List<string> userWords)
        {
            if (userWords.Intersect(abusiveWords).Any())
                return true;
            return false;
        }

        private string GetRandomElem(IList<string> collection)
        {
            return collection[random.Next(collection.Count)];
        }

        private bool ContainsAbusiveWords()
        {
            foreach(string word in abusiveWords)
            if (userPhrase.Contains(word))
                return true;
            return false;
        }

        private void SayBye()
        {
            string answer = badAnswers[random.Next(badAnswers.Count)];
            Console.WriteLine(answer);
        }

        private void InitVariables()
        {
            questions = dataProvider.GetQuestions();
            answers = dataProvider.GetAnswers();
            abusiveWords = dataProvider.GetAbusiveWords();
            words = dataProvider.GetWords();
            countries = dataProvider.GetCountries();
            goodAnswers = dataProvider.GetGoodAnswers();
            badAnswers = dataProvider.GetBadAnswers();

            answers.ForEach(ans => words.AddRange(ans.Words));
        }
    }
}
