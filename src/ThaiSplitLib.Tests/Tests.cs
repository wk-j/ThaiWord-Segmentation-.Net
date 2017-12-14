using System;
using System.Collections.Generic;
using THSplit;
using Xunit;

namespace ThaiSplitLib.Tests {

    public class Tests {

        [Fact]
        public void TestCtor() {
            Spliter spliter = new Spliter();
        }

        [Fact]
        public void TestSplit1() {
            var spliter = new Spliter();
            var test = "นายจะไปไหนหรอ";
            var output = spliter.SegmentByDictionary(test);

            var expect = new List<string> {
                "นาย",
                "จะ",
                "ไป",
                "ไหน",
                "หรอ"
            };

            Assert.Equal(expect, output);
        }

        [Fact]
        public void TestSplit2() {
            var spliter = new Spliter();
            var test = "ไอ้นี่ถ้าจะบ้า";
            var output = spliter.SegmentByDictionary(test);

            var expect = new List<string> {
                "ไอ้",
                "นี่",
                "ถ้า",
                "จะ",
                "บ้า"
            };

            Assert.Equal(expect, output);
        }
    }
}