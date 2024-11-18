using RotatingDonut;

namespace RotatingDonutTest
{
    [TestFixture]
    public class DonutRendererTests
    {
        [Test]
        public void InitializeBuffers_ShouldSetBuffersToDefaultValues()
        {

            var renderer = new DonutRenderer();

            renderer.InitializeBuffers();

            Assert.IsTrue(Array.TrueForAll(renderer.ZBuffer, z => z == 0));
            Assert.IsTrue(Array.TrueForAll(renderer.OutputBuffer, b => b == ' '));
        }

        [Test]
        public void RenderFrame_ShouldUpdateBuffers()
        {
            var renderer = new DonutRenderer();
            renderer.InitializeBuffers();

            renderer.RenderFrame();

            // Check that ZBuffer has values other than 0
            Assert.IsTrue(Array.Exists(renderer.ZBuffer, z => z != 0));

            // Check that OutputBuffer has characters other than ' '
            Assert.IsTrue(Array.Exists(renderer.OutputBuffer, b => b != ' '));
        }

        [Test]
        public void UpdateAngles_ShouldIncrementAngles()
        {
            var renderer = new DonutRenderer();
            float initialA = renderer.A;
            float initialB = renderer.B;

            renderer.UpdateAngles();

            Assert.That(renderer.A, Is.EqualTo(initialA + 0.04f));
            Assert.That(renderer.B, Is.EqualTo(initialB + 0.02f));
        }

        [Test]
        public void OutputBuffer_ShouldHaveExpectedLength()
        {
            var renderer = new DonutRenderer();
            Assert.That(renderer.OutputBuffer, Has.Length.EqualTo(1760));
        }

        [Test]
        public void ZBuffer_ShouldHaveExpectedLength()
        {
            var renderer = new DonutRenderer();
            Assert.That(renderer.ZBuffer, Has.Length.EqualTo(1760));
        }
    }
}
