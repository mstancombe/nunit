// ***********************************************************************
// Copyright (c) 2015 Charlie Poole, Rob Prouse
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using NUnit.Framework;

namespace NUnit.Tests
{
    [Parallelizable(ParallelScope.All)]
    public class SlowTests
    {
        const int DELAY = 1000;

        public class AAA
        {
            public string SampleState;

            [Test]
            public void Test1() {
                SampleState = "1";
                SlowTests.Delay();
                Assert.AreEqual("1", SampleState);
            }

            [Test]
            public void Test2() {
                SampleState = "2";
                SlowTests.Delay();
                Assert.AreEqual("2", SampleState);
            }

            [Test]
            public void Test3() {
                SampleState = "3";
                SlowTests.Delay();
                Assert.AreEqual("3", SampleState);
            }

        }

        //public class BBB
        //{
        //    [Test]
        //    public void Test1() { SlowTests.Delay(); }
        //    [Test]
        //    public void Test2() { SlowTests.Delay(); }
        //    [Test]
        //    public void Test3() { SlowTests.Delay(); }
        //}

        //public class CCC
        //{
        //    [Test]
        //    public void Test1() { SlowTests.Delay(); }
        //    [Test]
        //    public void Test2() { SlowTests.Delay(); }
        //    [Test]
        //    public void Test3() { SlowTests.Delay(); }
        //}

        private static void Delay()
        {
            System.Threading.Thread.Sleep(DELAY);
        }
    }
}
