using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static Utility;

namespace Tests
{
    public class UtilityTest
    {
        private IDictionary<string, int> _dict;

        [Test]
        public void RandomEntryTest()
        {
            _dict.Add("I can be your hero", 8142001);
            Assert.AreEqual("I can be your hero", RandomEntry(_dict).Key);
            Assert.AreEqual(8142001, RandomEntry(_dict).Value);
        }

        [TearDown]
        public void Teardown()
        {
        }

        [SetUp]
        public void Setup()
        {
            _dict = new Dictionary<string, int>();
        }
    }
}