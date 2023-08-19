using ScenarioEditUtility.Models;

namespace ScenarioEditUtilityTests.Models
{
    public class TextEditorTest 
    {
        [TestCase("","")] 
        [TestCase("a","")] 
        [TestCase("a","b")] 
        public void ReplaceOnceTest_空白入力(string pattern, string repl)
        {
            Assert.That(
                new TextEditor().ReplaceOnce(string.Empty, pattern, repl),
                Is.EqualTo(string.Empty));
        }
        
        /// <summary>
        /// 通常入力テスト
        /// </summary>
        [TestCase("abc","zzc")] 
        [TestCase("abc,abc","zzc,abc")] 
        [TestCase("test","test")] 
        public void ReplaceOnceTest_通常入力(string input, string result)
        {
            Assert.That(
                new TextEditor().ReplaceOnce(input, "ab", "zz"),
                Is.EqualTo(result));
        }

        [Test]
        [TestCase("abc","b", "<", ">", "a<b>c")] 
        [TestCase("abc,abc","b", "<", ">", "a<b>c,abc")] 
        [TestCase("b","b", "<", ">", "<b>")] 
        [TestCase("a0c",@"\d", "<", ">", "a<0>c")] 
        [TestCase("a0c,a0c",@"\d", "<", ">", "a<0>c,a0c")] 
        public void SurroundOnceTest(string input, string pattern, string front, string back, string result)
        {
            Assert.That(
                new TextEditor().SurroundOnce(input, pattern, front, back),
                Is.EqualTo(result));
        }

        [Test]
        [TestCase("abc","d","<d>abc</d>")] 
        [TestCase("<d>abc</d>","d","<d>abc</d>")] 
        [TestCase("<e>abc</e>","d","<d><e>abc</e></d>")] 
        public void SurroundElementTest(string input, string elementName, string result)
        {
            Assert.That(
                new TextEditor().SurroundElement(input, elementName),
                Is.EqualTo(result));
        }
        
        [TestCase("abc","d","e","<d e=\"abc\" />")] // 正常系
        [TestCase("a<bc","d","e","a<bc")]           // 不正な文字を含むテスト
        [TestCase("a>bc","d","e","a>bc")]           // 不正な文字を含むテスト
        [TestCase("\"abc\"","d","e","\"abc\"")]     // 不正な文字を含むテスト
        public void SurroundAttributeTest(string input, string elementName, string attributeName, string result)
        {
            Assert.That(
                new TextEditor().SurroundAttribute(input, elementName, attributeName),
                Is.EqualTo(result));
        }
    }
}