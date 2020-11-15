﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ORDER_MANAGEMENT.Data
{
    public static class DatatableExtensions
    {
        public static DataResult<T> ToDataResult<T>(this IQueryable<T> query, DataRequest request) where T : class
        {

            var result = new DataResult<T>
            {
                draw = request.draw
            };
            try
            {
                result.recordsTotal = result.recordsFiltered = query.Count();

                foreach (var item in request.filters)
                {
                    var exp = GetExpression<T>((Operand)item.Operand, item.Field, item.Value);
                    if (exp != null) query = query.Where(exp);
                }

                if (!string.IsNullOrEmpty(request.search?.value))
                {
                    Expression<Func<T, bool>> exp = null;
                    var listExp = new List<FilterDefinition>();
                    foreach (var item in request.columns.Where(a => a.searchable))
                    {
                        ParameterExpression param = Expression.Parameter(typeof(T), "t");
                        MemberExpression member = Expression.Property(param, item.data);
                        var operand = member.Type == typeof(string) ? Operand.Contains : Operand.Equal;
                        listExp.Add(new FilterDefinition { Operand = operand, Field = item.data, Value = request.search.value });
                    }
                    exp = ExpressionBuilder.GetExpression<T>(listExp);
                    if (exp != null) query = query.Where(exp);
                }

                if (!string.IsNullOrEmpty(request.search?.value) || request.filters.Any())
                {
                    result.recordsFiltered = query.Count();
                }

                if (request.draw > 0)
                {
                    if (!request.order.Any())
                    {
                        query = query.OrderBy(request.columns[0].data);
                    }
                    else
                    {
                        foreach (var item in request.order)
                        {
                            if (item.dir == "asc")
                            {
                                query = query.OrderBy(request.columns[item.column].data);
                            }
                            else
                            {
                                query = query.OrderByDescending(request.columns[item.column].data);
                            }
                        }
                    }
                    query = query.Skip(request.start).Take(request.length);
                }

                result.data = query.ToList();
                return result;
            }
            catch(Exception e)
            {
                result.data = new List<T>();
                return result;
            }
        }

        private static Expression<Func<T, bool>> GetExpression<T>(Operand operand, string field, string value)
        {
            return ExpressionBuilder
                .GetExpression<T>(new FilterDefinition
                {
                    Operand = operand,
                    Field = field,
                    Value = value
                });
        }

        private static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string memberName)
        {
            return OrderByCreate(query, memberName, "OrderBy");
        }

        private static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string memberName)
        {
            return OrderByCreate(query, memberName, "OrderByDescending");
        }

        private static IOrderedQueryable<T> OrderByCreate<T>(this IQueryable<T> query, string memberName, string direction)
        {
            var typeParams = new ParameterExpression[] { Expression.Parameter(typeof(T), "") };
            var pi = typeof(T).GetProperty(memberName);
            return (IOrderedQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    direction,
                    new Type[] { typeof(T), pi.PropertyType },
                    query.Expression,
                    Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams))
            );
        }
    }
}
