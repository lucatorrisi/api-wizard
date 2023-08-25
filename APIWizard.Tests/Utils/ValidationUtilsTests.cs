using APIWizard.Constants;
using APIWizard.Exceptions;
using APIWizard.Utils;
using NUnit.Framework;

namespace APIWizard.Tests.Utils
{
    [TestFixture]
    public class ValidationUtilsTests
    {
        [Test]
        public void ArgumentNotNull_ValidArgument_DoesNotThrow()
        {
            object validArgument = new object();

            Assert.DoesNotThrow(() => ValidationUtils.ArgumentNotNull(validArgument, "validArgument"));
        }

        [Test]
        public void ArgumentNotNull_NullArgument_ThrowsArgumentNullException()
        {
            object nullArgument = null;

            Assert.Throws<ArgumentNullException>(() => ValidationUtils.ArgumentNotNull(nullArgument, "nullArgument"));
        }

        [Test]
        public void ArgumentNotNullOrEmpty_ValidArgument_DoesNotThrow()
        {
            string validArgument = "valid";

            Assert.DoesNotThrow(() => ValidationUtils.ArgumentNotNullOrEmpty(validArgument, "validArgument"));
        }

        [Test]
        public void ArgumentNotNullOrEmpty_NullArgument_ThrowsArgumentNullException()
        {
            string nullArgument = null;

            Assert.Throws<ArgumentNullException>(() => ValidationUtils.ArgumentNotNullOrEmpty(nullArgument, "nullArgument"));
        }

        [Test]
        public void ArgumentNotNullOrEmpty_EmptyArgument_ThrowsArgumentException()
        {
            string emptyArgument = string.Empty;

            Assert.Throws<ArgumentException>(() => ValidationUtils.ArgumentNotNullOrEmpty(emptyArgument, "emptyArgument"));
        }

        [Test]
        public void ParameterNotNull_ValidParameter_DoesNotThrow()
        {
            object validParameter = new object();

            Assert.DoesNotThrow(() => ValidationUtils.ParameterNotNull(validParameter, "validParameter"));
        }

        [Test]
        public void ParameterNotNull_NullParameter_ThrowsAPIClientException()
        {
            object nullParameter = null;

            Assert.Throws<APIClientException>(() => ValidationUtils.ParameterNotNull(nullParameter, "nullParameter"));
        }

        [Test]
        public void ParameterNotNullOrEmpty_ValidParameter_DoesNotThrow()
        {
            string validParameter = "valid";

            Assert.DoesNotThrow(() => ValidationUtils.ParameterNotNullOrEmpty(validParameter, "validParameter"));
        }

        [Test]
        public void ParameterNotNullOrEmpty_NullParameter_ThrowsAPIClientException()
        {
            string nullParameter = null;

            Assert.Throws<APIClientException>(() => ValidationUtils.ParameterNotNullOrEmpty(nullParameter, "nullParameter"));
        }

        [Test]
        public void ParameterNotNullOrEmpty_EmptyParameter_ThrowsAPIClientException()
        {
            string emptyParameter = string.Empty;

            Assert.Throws<APIClientException>(() => ValidationUtils.ParameterNotNullOrEmpty(emptyParameter, "emptyParameter"));
        }
    }
}
