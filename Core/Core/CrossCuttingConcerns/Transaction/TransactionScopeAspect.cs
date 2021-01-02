using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Unilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Core.CrossCuttingConcerns.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (var transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    transactionScope.Complete();
                }
                catch (Exception e)
                {
                    transactionScope.Dispose();
                    throw;
                }
            }
        }
    }
}
