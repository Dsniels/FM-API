using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class SpecificationEvaluator<T> where T: Base
    {
        public static IQueryable<T> getQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            if(spec.Criteria != null)
            {
                inputQuery = inputQuery.Where(spec.Criteria);
            }
            if (spec.IsPagingEnabled)
            {
                inputQuery = inputQuery.Skip(spec.Skip).Take(spec.Take);
            }

            inputQuery = spec.Includes.Aggregate(inputQuery, (current, include) => current.Include(include));

            return inputQuery;




        }

    }
}
