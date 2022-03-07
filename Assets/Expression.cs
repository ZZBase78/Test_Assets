using System;

namespace ExpressionParser
{
    public sealed class Expression
    {
        private float simpleNumber;
        private Operations operation;
        private Expression operand1;
        private Expression operand2;

        private bool StartOperand1(ExpressionString expressionString)
        {
            if (expressionString.FindNextNumber(out float operand1result))
            {
                operand1 = new Expression(operand1result);
                return StartOperation(expressionString);
            }
            else if (expressionString.FindBeginSubExpression())
            {
                operand1 = new Expression();
                if (!operand1.Parse(expressionString))
                {
                    return false;
                }
                if (expressionString.FindEndSubExpression())
                {
                    return StartOperation(expressionString);
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
        private bool StartOperand2(ExpressionString expressionString)
        {
            if (expressionString.FindNextNumber(out float operand2result))
            {
                operand2 = new Expression(operand2result);
                return true;
            }
            else if (expressionString.FindBeginSubExpression())
            {
                operand2 = new Expression();
                if (!operand2.Parse(expressionString))
                {
                    return false;
                }
                return expressionString.FindEndSubExpression();
            }

            return false;
        }
        private bool StartOperation(ExpressionString expressionString)
        {
            if (expressionString.FindNextOperation(out Operations operationResult))
            {
                operation = operationResult;

                if (operation == Operations.Sum || operation == Operations.Sub)
                {
                    operand2 = new Expression();
                    return operand2.Parse(expressionString);
                }
                else
                {
                    return StartOperand2(expressionString);
                }
            }
            else
            {
                operation = Operations.SimpleExpression;
                return true;
            }
        }

        private bool Parse(ExpressionString expressionString)
        {
            if (StartOperand1(expressionString))
            {
                while (!expressionString.EndOfExpression())
                {
                    if (expressionString.SeekEndSubExpression())
                    {
                        return true;
                    }
                    operand1 = new Expression(operand1, operand2, operation);
                    operand2 = null;
                    operation = Operations.None;
                    return StartOperation(expressionString);
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool ParseExpression(ExpressionString expressionString)
        {
            return Parse(expressionString) && expressionString.EndOfExpression();
        }

        public Expression()
        {

        }

        public Expression(float value)
        {
            simpleNumber = value;
            operation = Operations.SimpleNumber;
        }
        public Expression(Expression operand1, Expression operand2, Operations operation)
        {
            this.operand1 = operand1;
            this.operand2 = operand2;
            this.operation = operation;
        }

        public float GetValue()
        {
            float result = 0;
            switch (operation)
            {
                case Operations.SimpleNumber:
                    result = simpleNumber;
                    break;
                case Operations.SimpleExpression:
                    result = operand1.GetValue();
                    break;
                case Operations.Sum:
                    result = operand1.GetValue() + operand2.GetValue();
                    break;
                case Operations.Sub:
                    result = operand1.GetValue() - operand2.GetValue();
                    break;
                case Operations.Multi:
                    result = operand1.GetValue() * operand2.GetValue();
                    break;
                case Operations.Div:
                    float operand2Value = operand2.GetValue();
                    if (operand2Value == 0) throw new DivideByZeroException(ErrorList.DEVISION_BY_ZERO);
                    result = operand1.GetValue() / operand2Value;
                    break;
                default:
                    result = 0;
                    break;

            }

            return result;
        }
    }

}
