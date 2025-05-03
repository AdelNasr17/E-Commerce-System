using System.Linq.Expressions;

namespace Domain.Contracts
{
    public abstract class Specifications<T> where T : class
    {
        
        public Expression<Func<T, bool>>? Criteria { get; }//Where     
        public List<Expression<Func<T, object>>> IncludeExpressions { get; } = new();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take  { get; private set; }
        public int Skip { get; private set; }

        public bool IsPaginated { get;private set; }
        protected Specifications(Expression<Func<T, bool>>? criteria)
        {
            Criteria = criteria;
        }

        // Method to add include expressions
        protected void AddInclude(Expression<Func<T, object>> expression)
        
          =>  IncludeExpressions.Add(expression);


        protected void SetOrderBy(Expression<Func<T, object>> expression)

          => OrderBy = expression;


        protected void SetOrderByDescending(Expression<Func<T, object>> expression)

      => OrderByDescending = expression;

        protected void ApplyPagination(int pageIndex , int PageSize)
        {
            IsPaginated = true;
            Take = PageSize;
            Skip=(pageIndex-1)* PageSize;
        }
    }
}
//Where 
//_dbCOntext.Set<T>().Where(Ex).Include().OrderBy()
//Where ==>Expression< Func<T,bool>>
//Include=>List<Expression< Func<T,object>>>
//< Func<T,bool>>===> Excutes in memery (C# Code) 
//Expression<Func<T, object>>===> Translates To SQL 
//Filter ==> BrandId, TypeId
//Sort===> Price(Desc,Asc),Name(Desc,ASC)
//Skip , Take ==> int
