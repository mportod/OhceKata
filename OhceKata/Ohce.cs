using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace OhceKata
{
    public class Ohce
    {
        private string userName = "";
        private readonly IDisplay _display;
        private readonly IUserInput _userInput;
        private readonly IClock _clock;
        private readonly IEnvironment _environment;

        public Ohce(IClock clock, IDisplay display, IUserInput userInput, IEnvironment environment)
        {
            this._display = display;
            this._userInput = userInput;
            this._clock = clock;
            this._environment = environment;
        }

        public void Run()
        {
            userName = _userInput.Read();
            var currentTime = _clock.GetCurrentHour();
            var greeting = GetGreetingMessage(currentTime);
            _display.Write(greeting);
        }

        public string Reverse(string word)
        {
            if (IsStopCommand(word))
            {
                _display.Write($"Adios {userName}!");
                _environment.Exit();
            }

            if (ContainsManyWords(word))
            {
                _display.Write("Error. Debes introducir una sola palabra");
            }

            var reversedWord = new string(word.ToCharArray().Reverse().ToArray());
            if (word.Equals(reversedWord))
            {
                _display.Write("¡Bonita palabra!");
            }
            return reversedWord;
        }


        private string GetGreetingMessage(TimeOnly currentTime)
        {
            string greeting;
            if (IsOnTheMorning(currentTime))
            {
                greeting = $"¡Buenos días {userName}!";
            }
            else if (IsOnTheAfternoon(currentTime))
            {
                greeting = $"¡Buenas tardes {userName}!";
            }
            else
            {
                greeting = $"¡Buenas noches {userName}!";
            }
            return greeting;
        }

        private static bool IsOnTheMorning(TimeOnly currentTime)
        {
            return currentTime >= new TimeOnly(06, 00) && currentTime < new TimeOnly(12, 00);
        }

        private static bool IsOnTheAfternoon(TimeOnly currentTime)
        {
            return currentTime >= new TimeOnly(12, 00) && currentTime < new TimeOnly(20, 00);
        }
       
        private static bool ContainsManyWords(string word)
        {
            return word.Split(' ').Length > 0;
        }

        private static bool IsStopCommand(string word)
        {
            return word == "Stop!";
        }

    }
}
