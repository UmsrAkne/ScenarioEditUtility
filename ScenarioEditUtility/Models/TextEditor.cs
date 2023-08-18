using System.Text.RegularExpressions;

namespace ScenarioEditUtility.Models
{
    public class TextEditor
    {
        /// <summary>
        /// 正規表現を利用して文字列を置き換えます。
        /// このメソッドによる置き換え処理は、target の中で最初にマッチした一回のみ行われます。
        /// target の中に pattern が複数含まれる場合は、２つ目以降は置き換えされません。
        /// </summary>
        /// <param name="target">置き換えを適用する文字列</param>
        /// <param name="pattern">置き換えるパターン</param>
        /// <param name="replaced">置き換え結果で入力される文字列</param>
        /// <returns>置き換えを適用した後の文字列を返す</returns>
        public string ReplaceOnce(string target, string pattern, string replaced)
        {
            var regex = new Regex(pattern);
            return regex.Replace(target, replaced);
        }
    }
}