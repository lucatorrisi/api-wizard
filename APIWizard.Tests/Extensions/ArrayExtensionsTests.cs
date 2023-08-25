using APIWizard.Extensions;
using NUnit.Framework;

namespace APIWizard.Tests.Extensions
{
    [TestFixture]
    public class ArrayExtensionsTests
    {
        [Test]
        public void Add_AddingItemToEmptyArray_ReturnsArrayWithNewItem()
        {
            int[] emptyArray = new int[0];
            int newItem = 42;

            var result = emptyArray.Add(newItem);

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(newItem, result[0]);
        }

        [Test]
        public void Add_AddingItemToNonEmptyArray_ReturnsArrayWithNewItem()
        {
            int[] originalArray = { 1, 2, 3 };
            int newItem = 4;

            var result = originalArray.Add(newItem);

            Assert.AreEqual(originalArray.Length + 1, result.Length);
            Assert.AreEqual(newItem, result[originalArray.Length]);
        }

        [Test]
        public void Add_AddingItemToNonEmptyArray_DoesNotModifyOriginalArray()
        {
            int[] originalArray = { 1, 2, 3 };
            int newItem = 4;

            var result = originalArray.Add(newItem);

            Assert.AreNotSame(originalArray, result);
            Assert.AreNotEqual(originalArray.Length, result.Length);
        }
    }
}
