using System.Linq.Expressions;

namespace Day21
{
    public class BinaryExpressionReducer : ExpressionVisitor
    {
        protected override Expression VisitBinary(BinaryExpression node)
        {
            var left = node.Left is ParameterExpression or ConstantExpression ? node.Left : Visit(node.Left);
            var right = node.Right is ParameterExpression or ConstantExpression ? node.Right : Visit(node.Right);

            if (left is ConstantExpression ceLeft && right is ConstantExpression ceRight)
            {
                return node.NodeType switch
                {
                    ExpressionType.Add => Expression.Constant((long)ceLeft.Value! + (long)ceRight.Value!, typeof(long)),
                    ExpressionType.Subtract => Expression.Constant((long)ceLeft.Value! - (long)ceRight.Value!, typeof(long)),
                    ExpressionType.Multiply => Expression.Constant((long)ceLeft.Value! * (long)ceRight.Value!, typeof(long)),
                    ExpressionType.Divide => Expression.Constant((long)ceLeft.Value! / (long)ceRight.Value!, typeof(long)),
                    _ => base.VisitBinary(node)
                };
            }

            return node.NodeType switch
            {
                ExpressionType.Add => Expression.Add(left, right),
                ExpressionType.Subtract => Expression.Subtract(left, right),
                ExpressionType.Multiply => Expression.Multiply(left, right),
                ExpressionType.Divide => Expression.Divide(left, right),
                _ => base.VisitBinary(node)
            };
        }
    }

    public class SimpleAlgebraicSolver : ExpressionVisitor
    {
        protected override Expression VisitBinary(BinaryExpression node)
        {
            var reducer = new BinaryExpressionReducer();
            if (node.NodeType == ExpressionType.Equal)
            {
                node = (BinaryExpression)reducer.Visit(node);
            }

            var lhs = node.Left;
            var rhs = node.Right;

            while (lhs is BinaryExpression be)
            {
                var lhsIsConstant = be.Left.NodeType == ExpressionType.Constant;
                var constant = lhsIsConstant ? be.Left : be.Right;
                var nodeType = lhs.NodeType;

                if (lhs is ConstantExpression && rhs is ParameterExpression ||
                    lhs is ParameterExpression && rhs is ConstantExpression)
                {
                    break;
                }

                rhs = nodeType switch
                {
                    ExpressionType.Add => Expression.Subtract(rhs, constant),
                    ExpressionType.Subtract when !lhsIsConstant => Expression.Add(rhs, constant),
                    ExpressionType.Subtract when lhsIsConstant => Expression.Subtract(constant, rhs),
                    ExpressionType.Multiply => Expression.Divide(rhs, constant),
                    ExpressionType.Divide => Expression.Multiply(rhs, constant),
                    _ => throw new Exception("Invalid node type.")
                };

                lhs = lhsIsConstant ? be.Right : be.Left;

                lhs = reducer.Visit(lhs);
                rhs = reducer.Visit(rhs);
            }

            return Expression.Equal(lhs, rhs);
        }
    }

}
