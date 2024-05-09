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

        [Test]
        public void should_greet_user_at_six()
        {
            var name = "Manuel";
            userInput.Read().Returns(name);
            clock.GetCurrentHour().Returns(new TimeOnly(06, 00));

            sut.Run();

            display.Received().Write($"¡Buenos días {name}!");
        }

        [Test]
        public void should_greet_user_at_11_and_59()
        {
            var name = "Manuel";
            userInput.Read().Returns(name);

            clock.GetCurrentHour().Returns(new TimeOnly(11, 59));

            sut.Run();

            display.Received().Write($"¡Buenos días {name}!");
        }
    }
}