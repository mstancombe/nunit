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
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class AAA
    {
        const int DELAY = 1000;
        public string SampleState;

        public static string[] GetTestCases() 
            => new string[] { "1", "2", "3", "4", "5", "6" };

        [TestCaseSource(nameof(GetTestCases))]
        public void TestFromSource(string testNumber)
        {
            SampleState = testNumber;
            Delay();
            Assert.AreEqual(testNumber, SampleState);
        }


        private static void Delay()
        {
            System.Threading.Thread.Sleep(DELAY);
        }
    }
}
