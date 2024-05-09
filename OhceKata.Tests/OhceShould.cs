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

        [Test]
        public void should_greet_user_at_12()
        {
            var name = "Manuel";
            userInput.Read().Returns(name);
            clock.GetCurrentHour().Returns(new TimeOnly(12, 00));

            sut.Run();

            display.Received().Write($"¡Buenas tardes {name}!");
        }

        [Test]
        public void should_greet_user_at_19_and_59()
        {
            var name = "Manuel";
            userInput.Read().Returns(name);
            clock.GetCurrentHour().Returns(new TimeOnly(19, 59));

            sut.Run();

            display.Received().Write($"¡Buenas tardes {name}!");
        }
    }
}