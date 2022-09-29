using Autodesk.Revit.DB;
using System;

namespace BIM.Util.Extensions
{

    public static class TransactionExtension
    {
        /// <summary>
        /// 封装事务组方法
        /// </summary>
        /// <param name="doc"> doc</param>
        /// <param name="action"> 委托</param>
        /// <param name="name"> 事务组名称</param>
        public static void InvokeGroup(this Document doc, Action<TransactionGroup> action,
            string name = "transactionGroup")
        {
            using (TransactionGroup tg = new TransactionGroup(doc, name))
            {
                tg.Start();
                action(tg);
                if (tg.HasStarted())
                {
                    if (tg.GetStatus() == TransactionStatus.Started)
                    {
                        tg.Assimilate();
                    }
                    else
                    {
                        tg.RollBack();
                    }
                }
            }
        }

        /// <summary>
        /// 封装事务方法
        /// </summary>
        /// <param name="doc"> doc</param>
        /// <param name="action"> 委托</param>
        /// <param name="name"> 事务名称</param>
        /// <param name="ignorefailure"> 忽略错误</param>
        public static void Invoke(this Document doc, Action<Transaction> action,
            string name = "transaction", bool ignorefailure = true)
        {
            using (Transaction tr = new Transaction(doc, name))
            {
                tr.Start();
                if (ignorefailure) IgnoreFailure(tr);
                action(tr);
                if (tr.HasStarted())
                {
                    if (tr.GetStatus() == TransactionStatus.Started)
                    {
                        tr.Commit();
                    }
                    else
                    {
                        tr.RollBack();
                    }
                }
            }
        }




        /// <summary>
        /// 忽略警告弹窗提示
        /// </summary>
        /// <param name="t"> 事务</param>
        private static void IgnoreFailure(Transaction t)
        {
            var fho = t.GetFailureHandlingOptions();
            fho.SetFailuresPreprocessor(new Base.FailuresPreprocessor());
            t.SetFailureHandlingOptions(fho);
        }
    }
}
