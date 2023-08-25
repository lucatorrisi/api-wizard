using NUnit.Framework;
using APIWizard.Constants;
using APIWizard.Extensions;

namespace APIWizard.Tests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void ContainsCurlyBraces_InputContainsCurlyBraces_ReturnsTrue()
        {
            // Arrange
            string input = "Hello {world}";

            // Act
            bool result = input.ContainsCurlyBraces();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ContainsCurlyBraces_InputDoesNotContainCurlyBraces_ReturnsFalse()
        {
            // Arrange
            string input = "Hello world";

            // Act
            bool result = input.ContainsCurlyBraces();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ContainsCurlyBraces_NullInput_ReturnsFalse()
        {
            // Arrange
            string input = null;

            // Act
            bool result = input.ContainsCurlyBraces();

            // Assert
            Assert.IsFalse(result);
        }
    }
}
