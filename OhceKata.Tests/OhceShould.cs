using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace OhceKata.Tests
{
    public class OhceShould
    {
        private IClock clock = default!;
        private IDisplay display = default!;
        private IUserInput userInput = default!;
        private IEnvironment environment = default!;
        private Ohce sut = default!;

        [SetUp]
        public void SetUp()
        {
            clock = Substitute.For<IClock>();
            display = Substitute.For<IDisplay>();
            userInput = Substitute.For<IUserInput>();
            environment = Substitute.For<IEnvironment>();
            sut = new Ohce(clock, display, userInput, environment);
        }

        [TestCase(06, 00)]
        [TestCase(08, 35)]
        [TestCase(11, 59)]
        public void should_greet_user_when_time_is_between_6_and_12(int hour, int minute)
        {
            var name = "Manuel";
            userInput.Read().Returns(name);
            clock.GetCurrentHour().Returns(new TimeOnly(hour, minute));

            sut.Run();

            display.Received().Write($"¡Buenos días {name}!");
        }

        [TestCase(12, 00)]
        [TestCase(17, 22)]
        [TestCase(19, 59)]
        public void should_greet_user_when_time_is_between_12_and_20(int hour, int minute)
        {
            var name = "Manuel";
            userInput.Read().Returns(name);
            clock.GetCurrentHour().Returns(new TimeOnly(hour, minute));

            sut.Run();

            display.Received().Write($"¡Buenas tardes {name}!");
        }
    }
}