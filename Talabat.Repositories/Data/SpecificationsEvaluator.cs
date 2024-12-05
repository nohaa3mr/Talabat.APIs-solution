using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public static class SpecificationsEvaluator<T> where T: BaseEntity
    {
        public static IQueryable<T> QueryEvaluator(IQueryable<T> inputQuery , ISpecifications<T> specifications)
        {
            var Query = inputQuery;   //_dbcontext.Set<T>()


            if (specifications.Criteria is not null)
            {
                Query = Query.Where(specifications.Criteria);    
            }     
           Query = specifications.Includes.Aggregate(Query, (currentQuery, IncludeExpression) => currentQuery.Include(IncludeExpression));

            return Query;
        }
    }
}
