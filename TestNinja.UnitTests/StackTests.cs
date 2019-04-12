using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class StackTests
    {
        private Stack<int?> _genericStack;

        [SetUp]
        public void SetUp()
        {
            _genericStack = new Stack<int?>();
        }

        [Test]
        public void Count_EmptyStack_ReturnZero()
        {
            Assert.That(_genericStack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Count_WhenCalled_ReturnTheNumberOfObjectsInTheStack()
        {
            _genericStack.Push(1);
            Assert.That(_genericStack.Count, Is.EqualTo(1));

            _genericStack.Push(-1);
            Assert.That(_genericStack.Count, Is.EqualTo(2));
        }

        [Test]
        public void Push_ArgIsNull_ThrowArgumentNullException()
        {
            Assert.That(() => _genericStack.Push(null), Throws.Exception.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Push_WhenCalled_StackContainsNewlyAddedObject()
        {
            _genericStack.Push(1);
            var topOfStack = _genericStack.Peek();
            Assert.That(topOfStack, Is.EqualTo(1));
            Assert.That(_genericStack.Count, Is.EqualTo(1));

            _genericStack.Push(-1);
            topOfStack = _genericStack.Peek();
            Assert.That(topOfStack, Is.EqualTo(-1));
            Assert.That(_genericStack.Count, Is.EqualTo(2));
        }

        [Test]
        public void Pop_EmptyStack_ThrowsArgumentNullException()
        {
            Assert.That(() => _genericStack.Pop(), Throws.Exception.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Pop_StackHasObjects_PopTheStack()
        {
            _genericStack.Push(1);
            _genericStack.Push(-1);

            var result = _genericStack.Pop();
            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        public void Pop_StackHasObjects_RemoveTopOfTheStackObject()
        {
            _genericStack.Push(1);
            _genericStack.Push(-1);

            _genericStack.Pop();
            Assert.That(_genericStack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Peek_EmptyStack_ThrowsInvalidOperationException()
        {
            Assert.That(() => _genericStack.Peek(), Throws.Exception.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Peek_StackHasObjects_ReturnObjectOnTopOfStack()
        {
            _genericStack.Push(1);
            _genericStack.Push(-1);

            var topOfStack = _genericStack.Peek();

            Assert.That(topOfStack, Is.EqualTo(-1));
        }

        [Test]
        public void Peek_StackHasObjects_DoesNotRemoveTheObjectOnTopOfStack()
        {
            _genericStack.Push(1);
            _genericStack.Push(-1);

            _genericStack.Peek();

            Assert.That(_genericStack.Count, Is.EqualTo(2));
        }
    }
}
