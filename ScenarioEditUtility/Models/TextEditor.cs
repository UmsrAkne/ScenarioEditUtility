﻿using System.Collections.Generic;
using System.Linq;
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
            return regex.Replace(target, replaced, 1);
        }

        /// <summary>
        /// 正規表現を利用して文字列を検索し、マッチした文字列を引数の front, back で挟んだ文字列を取得します。
        /// このメソッドによる処理は、target の中で最初にマッチした一回のみ行われます。
        /// target の中に pattern が複数含まれる場合は、２つ目以降は処理されません。
        /// </summary>
        /// <param name="target">処理を適用する文字列</param>
        /// <param name="pattern">挟み込む文字列</param>
        /// <param name="front">pattern の前に付加する文字列</param>
        /// <param name="back">pattern の後ろに付加する文字列</param>
        /// <returns>このメソッドの処理を施した文字列</returns>
        public string SurroundOnce(string target, string pattern, string front, string back)
        {
            var regex = new Regex(pattern);
            return regex.Replace(target, $"{front}$0{back}", 1);
        }

        /// <summary>
        /// target 全体を elementName で作ったタグで挟み込みます。
        /// このメソッドは、target の中に、 elementName の要素が存在する場合は処理を行いません。
        /// その場合は、target をそのまま返します。
        /// </summary>
        /// <param name="target">処理を適用する文字列</param>
        /// <param name="elementName">要素名をタグ形式に変換して、 target を挟み込みます。</param>
        /// <returns>target 全体を elementName の要素で挟んだ文字列</returns>
        public string SurroundElement(string target, string elementName)
        {
            var open = $"<{elementName}>";
            var end = $"</{elementName}>";

            return Regex.IsMatch(target, open) && Regex.IsMatch(target, end)
                ? target
                : SurroundOnce(target, target, open, end);
        }

        /// <summary>
        /// target 全体を elementName で作ったタグ内の attributeName の値として挟み込みます。
        /// </summary>
        /// <param name="target">処理を適用する文字列</param>
        /// <param name="elementName">要素名をタグ形式に変換します。</param>
        /// <param name="attributeName">elementName の中の属性として挿入されます。</param>
        /// <returns>target 全体を、 elementName の中の attributeName の値として囲い込んだ文字列</returns>
        public string SurroundAttribute(string target, string elementName, string attributeName)
        {
            var open = $"<{elementName} {attributeName}=\"";
            var end = $"\" />";
            
            return Regex.IsMatch(target, "[<>\"/]")
                ? target
                : SurroundOnce(target, target, open, end);
        }

        /// <summary>
        /// 指定したリスト内の文字列から pattern にマッチした文字列を連番に置き換えます。
        /// 置き換える連番は 1 番から開始します。
        /// </summary>
        /// <param name="targets">置き換え対象を含む文字列を詰めたリスト</param>
        /// <param name="pattern">連番に置き換える文字列</param>
        /// <returns>pattern を連番に置き換えた文字列のリスト</returns>
        public IEnumerable<string> ReplaceToNumber(IEnumerable<string> targets, string pattern)
        {
            return targets.Select(
                (str, index) => ReplaceOnce(str, pattern, (index + 1).ToString("D3")));
        }
    }
}