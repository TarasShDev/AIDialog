using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIDialog.Entities;
using Newtonsoft.Json;

namespace AIDialog
{
    class DataProvider
    {
        public List<QuestionsWithCat> GetQuestions(string path = Configs.questionsPath)
        {
            string jsonFile = File.ReadAllText(path);
            List<QuestionsWithCat> questions = JsonConvert.DeserializeObject<List<QuestionsWithCat>>(jsonFile);
            return questions;
        }

        public List<AnswerWithCat> GetAnswers(string path = Configs.answersPath)
        {
            string jsonFile = File.ReadAllText(path);
            List<AnswerWithCat> answers = JsonConvert.DeserializeObject<List<AnswerWithCat>>(jsonFile);
            return answers;
        }

        public List<string> GetWords(string path = Configs.wordsPath)
        {
            List<string> words = new List<string>(File.ReadAllLines(path));
            return words;
        }

        public List<Country> GetCountries(string path = Configs.countiesPath)
        {
            string jsonFile = File.ReadAllText(path);
            List<Country> answers = JsonConvert.DeserializeObject<List<Country>>(jsonFile);
            return answers;
        }

        public List<string> GetAbusiveWords(string path = Configs.abusiveWordsPath)
        {
            List<string> words = new List<string>(File.ReadAllLines(path));
            return words;
        }

        public List<string> GetGoodAnswers(string path = Configs.goodAnswersPath)
        {
            List<string> words = new List<string>(File.ReadAllLines(path));
            return words;
        }

        public List<string> GetBadAnswers(string path = Configs.badAnswersPath)
        {
            List<string> words = new List<string>(File.ReadAllLines(path));
            return words;
        }
    }
}
