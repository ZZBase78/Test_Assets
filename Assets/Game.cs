using System;
using UnityEngine;

namespace ExpressionParser
{
    public sealed class Game
    {
        public void Start()
        {
            ExpressionString expressionString = new ExpressionString("(((4+4)/(2+2)-0.5)+((1.5)*3))/(1+(2*1))");
            //ExpressionString expressionString = new ExpressionString("(((4+4)/(2+2)-0.5)+((1.5)*3))/"); //test error expression
            //ExpressionString expressionString = new ExpressionString("(((4+4)/(2+2)-0.5)((1.5)*3))/(1+(2*1))"); //test error expression
            //ExpressionString expressionString = new ExpressionString("1/0"); // error result
            //ExpressionString expressionString = new ExpressionString("()"); // test empty sub expression
            Expression expression = new Expression();
            if (expression.ParseExpression(expressionString))
            {
                try
                {
                    Debug.Log(expression.GetValue());
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }

            }
            else
            {
                Debug.Log(ErrorList.ERROR_IN_EXPRESSION);
            }

        }
    }

}
