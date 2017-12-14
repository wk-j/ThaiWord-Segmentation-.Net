using System;
using System.Collections.Generic;
using THSplit;
using Xunit;

namespace UnitTestProject {
    public class Tests {

        [Fact]
        public void TestCtor() {
            Spliter spliter = new Spliter();
        }

        [Fact]
        public void TestSplit1() {
            Spliter spliter = new Spliter();
            string test = "นายจะไปไหนหรอ";
            var output = spliter.SegmentByDictionary(test);

            var asset = new List<string>
            {
                "นาย",
                "จะ",
                "ไป",
                "ไหน",
                "หรอ"
            };
            foreach (var variable in output) {
                Console.WriteLine(variable);
            }
            Assert.Equal(asset.Count, output.Count);
            Assert.Equal(asset[0], output[0]);
            Assert.Equal(asset[1], output[1]);
            Assert.Equal(asset[2], output[2]);
            Assert.Equal(asset[3], output[3]);
            Assert.Equal(asset[4], output[4]);
        }

        [Fact]
        public void TestSplit2() {
            Spliter spliter = new Spliter();
            string test = "ไอ้นี่ถ้าจะบ้า";
            var output = spliter.SegmentByDictionary(test);

            var asset = new List<string>
            {
                "ไอ้",
                "นี่",
                "ถ้า",
                "จะ",
                "บ้า"
            };

            Assert.Equal(asset.Count, output.Count);
            Assert.Equal(asset[0], output[0]);
            Assert.Equal(asset[1], output[1]);
            Assert.Equal(asset[2], output[2]);
            Assert.Equal(asset[3], output[3]);
            Assert.Equal(asset[4], output[4]);
        }
    }
}
