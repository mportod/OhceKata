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
            if (IsOnTheMorning(currentTime))
            {
                _display.Write($"¡Buenos días {userName}!");
            }
            if (IsOnTheAfternoon(currentTime))
            {
                _display.Write($"¡Buenas tardes {userName}!");
            }
            _display.Write($"¡Buenas noches {userName}!");
        }

        private static bool IsOnTheMorning(TimeOnly currentTime)
        {
            return currentTime >= new TimeOnly(06, 00) && currentTime < new TimeOnly(12, 00);
        }

        private static bool IsOnTheAfternoon(TimeOnly currentTime)
        {
            return currentTime >= new TimeOnly(12, 00) && currentTime < new TimeOnly(20, 00);
        }
    }
}
