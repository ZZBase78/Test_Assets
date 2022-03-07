using System;
using UnityEngine;
using UnityEngine.UI;

namespace ExpressionParser
{
    public class FormCalculate : MonoBehaviour
    {
        public Text resultText;
        public InputField inputField;

        public void OnInputFieldValueChanged()
        {
            ExpressionString expressionString = new ExpressionString(inputField.text);
            Expression expression = new Expression();
            if (expression.ParseExpression(expressionString))
            {
                try
                {
                    resultText.text = expression.GetValue().ToString();
                }
                catch (Exception e)
                {
                    resultText.text = e.Message;
                }

            }
            else
            {
                resultText.text = ErrorList.ERROR_IN_EXPRESSION;
            }
        }
    }

}
