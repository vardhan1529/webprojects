using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Web;

namespace ElmahMvc.Game
{
    public class StringTrimmerInterceptor : IDbCommandTreeInterceptor
    {
        public void TreeCreated(DbCommandTreeInterceptionContext context)
        {
            if (context.OriginalResult.DataSpace == DataSpace.SSpace)
            {
                switch (context.Result.CommandTreeKind)
                {
                    case DbCommandTreeKind.Insert:
                        context.Result = InsertCommandTree(context.Result);
                        break;
                    case DbCommandTreeKind.Update:
                        context.Result = UpdateCommandTree(context.Result);
                        break;
                }
            }
        }

        private DbCommandTree InsertCommandTree(DbCommandTree command)
        {
            var insertCommand = command as DbInsertCommandTree;
            if (insertCommand == null)
            {
                return command;
            }

            return new DbInsertCommandTree(
                    insertCommand.MetadataWorkspace,
                    insertCommand.DataSpace,
                    insertCommand.Target,
                    new ReadOnlyCollection<DbModificationClause>(ModifyClause(insertCommand.SetClauses, insertCommand.Target.VariableType, insertCommand.Target.VariableName)),
                    insertCommand.Returning);
        }

        private DbCommandTree UpdateCommandTree(DbCommandTree command)
        {
            var updateCommand = command as DbUpdateCommandTree;
            if (updateCommand == null)
            {
                return command;
            }

            return new DbUpdateCommandTree(
                    updateCommand.MetadataWorkspace,
                    updateCommand.DataSpace,
                    updateCommand.Target,
                    updateCommand.Predicate,
                    new ReadOnlyCollection<DbModificationClause>(ModifyClause(updateCommand.SetClauses, updateCommand.Target.VariableType, updateCommand.Target.VariableName)),
                    updateCommand.Returning);
        }

        private List<DbModificationClause> ModifyClause(IList<DbModificationClause> clauses, TypeUsage variableType, string variableName)
        {
            return clauses.Select(m =>
            {
                var dbSetClause = m as DbSetClause;
                if (dbSetClause != null)
                {
                    var dbPropertyExpression = dbSetClause.Property as DbPropertyExpression;

                    if (dbPropertyExpression != null)
                    {
                        var edmProperty = dbPropertyExpression.Property as EdmProperty;
                        if (edmProperty != null && (edmProperty.TypeName.Contains("char") || edmProperty.TypeName.Contains("nchar") || edmProperty.TypeName.Contains("varchar") || edmProperty.TypeName.Contains("nvarchar")))
                        {
                            var dbConstantExpression = dbSetClause.Value as DbConstantExpression;
                            if (dbConstantExpression != null && dbConstantExpression.Value != null)
                            {
                                var value = dbConstantExpression.Value.ToString();
                                if (!string.IsNullOrEmpty(value))
                                {
                                    return DbExpressionBuilder.SetClause(
                                        DbExpressionBuilder.Property(
                                            DbExpressionBuilder.Variable(variableType, variableName),
                                            dbPropertyExpression.Property.Name),
                                    dbConstantExpression.Value.ToString().Trim());
                                }

                            }
                        }
                    }
                }
                return m;
            }).ToList();
        }

        private class StringTrimmerQueryVisitor : DefaultExpressionVisitor
        {
            private static readonly string[] _typesToTrim = { "nvarchar", "varchar", "char", "nchar", "String" };

            public override DbExpression Visit(DbNewInstanceExpression expression)
            {
                var arguments = expression.Arguments.Select(a =>
                {
                    var propertyArg = a as DbPropertyExpression;
                    if (propertyArg != null && _typesToTrim.Contains(propertyArg.Property.TypeUsage.EdmType.Name))
                    {
                        return EdmFunctions.Trim(a);
                    }

                    return a;
                });

                var temp = new List<DbExpression>();
                foreach (var a in expression.Arguments)
                {
                    var propertyArg = a as DbPropertyExpression;
                    if (propertyArg != null && _typesToTrim.Contains(propertyArg.Property.TypeUsage.EdmType.Name))
                    {
                        temp.Add(EdmFunctions.Trim(a));
                    }
                    else
                    {
                        temp.Add(a);
                    }
                }

                return DbExpressionBuilder.New(expression.ResultType, temp);
            }
        }
    }
}