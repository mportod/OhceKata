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
            if ((_clock.GetCurrentHour().Hour == 6 && _clock.GetCurrentHour().Minute == 0) ||
                (_clock.GetCurrentHour().Hour == 11 && _clock.GetCurrentHour().Minute == 59))
            {
                _display.Write($"¡Buenos días {userName}!");
            }
        }
    }
}
