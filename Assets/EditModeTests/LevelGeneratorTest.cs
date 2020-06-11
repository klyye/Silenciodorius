using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class LevelGeneratorTest
    {
        private LevelGenerator _levelGen;
        private Transform _generatedMap;
        
        // A Test behaves as an ordinary method
        [Test]
        public void LevelGeneratorTestSimplePasses()
        {
            // Use the Assert class to test conditions
            Assert.NotNull(_generatedMap);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator LevelGeneratorTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        [TearDown]
        public void Teardown()
        {
            
        }
        
        [SetUp]
        public void Setup()
        {
            _levelGen = new LevelGenerator();
            _generatedMap = _levelGen.GenerateLevel();
        }
    }
}
