using System.Collections.Generic;
using NUnit.Framework;
using static Utility;

namespace Tests
{
    public class UtilityTest
    {
        private IDictionary<string, int> _dict;
        private string[] _words;

        [Test]
        public void RandomEntryTest()
        {
            Assert.AreEqual("I can be your hero", RandomEntry(_dict).Key);
            Assert.AreEqual(8142001, RandomEntry(_dict).Value);
        }

        [Test]
        public void RandomElementPredTest()
        {
            Assert.AreEqual("I", RandomElement(_words, s => s.Length < 2));
            Assert.AreEqual("can", RandomElement(_words, s => s[0] == 'c'));
            Assert.AreEqual(null, RandomElement(_words, s => s.EndsWith("Ooh!")));
        }

        [TearDown]
        public void Teardown()
        {
        }

        [SetUp]
        public void Setup()
        {
            _dict = new Dictionary<string, int> {{"I can be your hero", 8142001}};
            _words = new[] {"I", "can", "be", "your", "hero"};
        }
    }
}