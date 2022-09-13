using System;
using System.IO;
using NUnit.Framework;

namespace NSW.Testing.Internal
{
    [TestFixture]
    public class RandomHelperTests
    {
        [Test]
        public void ShouldGenerateRandoms()
        {
            RandomHelper.Reset();

            var int1 = RandomHelper.NextInt();
            var int2 = RandomHelper.NextInt();
            Assert.AreNotEqual(int1,int2);

            var double1 = RandomHelper.NextDouble();
            var double2 = RandomHelper.NextDouble();
            Assert.AreNotEqual(double1, double2);

            var string1 = RandomHelper.NextString();
            var string2 = RandomHelper.NextString();
            Assert.AreNotEqual(string1, string2);
            Assert.AreEqual(100, string1.Length);
            Assert.AreEqual(100, string2.Length);

            var guid1 = RandomHelper.NextGuid();
            var guid2 = RandomHelper.NextGuid();
            Assert.AreNotEqual(guid1, guid2);

            var bytes1 = RandomHelper.NextBytes();
            var bytes2 = RandomHelper.NextBytes();
            Assert.AreNotEqual(bytes1, bytes2);
            Assert.AreEqual(100, bytes1.Length);
            Assert.AreEqual(100, bytes1.Length);
        }

        [Test]
        public void ShouldRandomize()
        {
            RandomHelper.Reset();

            var testClass = new TestObject();
            testClass.Randomize();
            Assert.NotNull(testClass.Long);
            Assert.NotNull(testClass.Enum);
            Assert.NotNull(testClass.Bytes);
            Assert.NotNull(testClass.Stream);
            Assert.NotNull(testClass.Memory);
            Assert.Null(testClass.Test);

            testClass = new TestObject();
            testClass.Randomize(fillNullable: false);
            Assert.Null(testClass.Long);
            Assert.Null(testClass.Enum);
            Assert.Null(testClass.Test);
        }

        enum TestEnum
        {
            Test1,
            Test2,
            Test3,
            Test4,
            Test5,
            Test6
        }
        class TestObject
        {
            public byte Byte { get; set; }
            public short Short { get; set; }
            public int Int { get; set; }
            public ushort UShort { get; set; }
            public uint UInt { get; set; }
            public ulong ULong { get; set; }
            public bool Bool { get; set; }
            public float Float { get; set; }
            public double Double { get; set; }
            public long? Long { get; set; }
            public TestEnum? Enum { get; set; }
            public string? String { get; set; }
            public Guid Guid { get; set; }
            public DateTime DateTime { get; set; }
            public TimeSpan TimeSpan { get; set; }
            public byte[]? Bytes { get; set; }
            public Stream? Stream { get; set; }
            public MemoryStream? Memory { get; set; }
            public string? Test { get; }
        }
    }
}