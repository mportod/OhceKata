using FluentAssertions;
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

        [TestCase(20, 00)]
        [TestCase(03, 05)]
        [TestCase(05, 59)]
        public void should_greet_user_when_time_is_between_20_and_6(int hour, int minute)
        {
            var name = "Manuel";
            userInput.Read().Returns(name);
            clock.GetCurrentHour().Returns(new TimeOnly(hour, minute));

            sut.Run();

            display.Received().Write($"¡Buenas noches {name}!");
        }

        [Test]
        public void should_reverse_when_is_no_palindrome_word()
        {
            var word = "coche";

            var result = sut.Reverse(word);

            result.Should().Be("ehcoc");
        }

        [Test]
        public void should_reverse_when_word_is_not_a_valid_stop_command()
        {
            var word = "stop!";

            var result = sut.Reverse(word);

            result.Should().Be("!pots");
        }
    }
}