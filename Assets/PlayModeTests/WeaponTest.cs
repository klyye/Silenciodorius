using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace PlayModeTests
{
    public class WeaponTest
    {
        [Test]
        public void WeaponTestSimplePasses()
        {
            // Use the Assert class to test conditions.
            
        }

        // A UnityTest behaves like a coroutine in PlayMode
        // and allows you to yield null to skip a frame in EditMode
        [UnityTest]
        public IEnumerator WeaponTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // yield to skip a frame
            yield return null;
        }
    }
}