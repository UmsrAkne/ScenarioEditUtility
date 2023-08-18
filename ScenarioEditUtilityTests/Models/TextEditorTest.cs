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
    }
}