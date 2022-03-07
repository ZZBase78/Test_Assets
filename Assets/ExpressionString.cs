using System;
using System.Collections.Generic;

namespace ExpressionParser
{
    public sealed class ExpressionString
    {
        private const int MIN_DIGIT_TO_NUMBER = 3;
        private const string DOT_STRING = ".";
        private const string COMMA_STRING = ",";
        private const char CHAR_SUM = '+';
        private const char CHAR_SUB = '-';
        private const char CHAR_MULTI = '*';
        private const char CHAR_DIV = '/';
        private const char BEGIN_SUB_EXPRESSION = '(';
        private const char END_SUB_EXPRESSION = ')';

        private List<Char> chars;
        private int index;

        public ExpressionString(string str)
        {
            index = 0;
            chars = new List<char>();
            chars.AddRange(str.Trim());
        }

        public bool FindBeginSubExpression()
        {
            if (EndOfExpression()) return false;

            if (chars[index] == BEGIN_SUB_EXPRESSION)
            {
                index++;
                SkipSpaces();
                return true;
            }

            return false;
        }

        public bool FindEndSubExpression()
        {
            if (EndOfExpression()) return false;

            if (chars[index] == END_SUB_EXPRESSION)
            {
                index++;
                SkipSpaces();
                return true;
            }

            return false;
        }

        public bool SeekEndSubExpression()
        {
            if (EndOfExpression()) return false;

            return chars[index] == END_SUB_EXPRESSION;
        }

        public bool EndOfExpression()
        {
            return index >= chars.Count;
        }

        private string DotChange(string str)
        {
            return str.Replace(DOT_STRING, COMMA_STRING);
        }

        private bool TryGetNextNumber(int digitCount, out float result)
        {
            result = 0;
            List<Char> numberList = chars.GetRange(index, digitCount);
            return float.TryParse(DotChange((string.Concat(numberList)).Trim()), out result);
        }

        public void SkipSpaces()
        {
            while (index < chars.Count && Char.IsWhiteSpace(chars[index]))
            {
                index++;
            }
        }

        public bool FindNextNumber(out float result)
        {
            result = 0;
            int resultcount = 0;
            bool methodResult = false;

            int count = 0;

            bool repeating = true;

            while (repeating && ((index + count) < chars.Count))
            {
                count++;
                if (TryGetNextNumber(count, out float tempResult))
                {
                    result = tempResult;
                    resultcount = count;
                    methodResult = true;
                }
                else
                {
                    if (count > MIN_DIGIT_TO_NUMBER) repeating = false;
                }
            }
            if (methodResult)
            {
                index += resultcount;
                SkipSpaces();
            }

            return methodResult;
        }

        public bool FindNextOperation(out Operations result)
        {
            result = Operations.None;
            bool methodResult = false;

            if (index < chars.Count)
            {
                char nextChar = chars[index];

                switch (nextChar)
                {
                    case CHAR_SUM:
                        result = Operations.Sum;
                        methodResult = true;
                        break;
                    case CHAR_SUB:
                        result = Operations.Sub;
                        methodResult = true;
                        break;
                    case CHAR_MULTI:
                        result = Operations.Multi;
                        methodResult = true;
                        break;
                    case CHAR_DIV:
                        result = Operations.Div;
                        methodResult = true;
                        break;
                    default:
                        result = Operations.None;
                        break;
                }
            }

            if (methodResult)
            {
                index++;
                SkipSpaces();
            }

            return methodResult;
        }
    }

}
