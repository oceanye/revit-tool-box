using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using BIM.Util.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BIM.Util.Extensions
{
    public static class Extension
    {

        /// <summary>
        /// init
        /// </summary>
        public static UIDocument Init(this ExternalCommandData commandData)
        {
            try
            {
                XmlDoc.Instance.UIapp = commandData.Application;
                XmlDoc.Instance.Task = new RevitTask();
            }
            catch (Exception ex)
            {
                Log.Debug($"初始化程序失败:{ex.Message}");
            }
            return commandData.Application.ActiveUIDocument;
        }

        /// <summary>
        /// show 主窗口
        /// </summary>
        /// <param name="window"></param>
        public static void MainWindowHandle(this Window window)
        {
            new System.Windows.Interop.WindowInteropHelper(window)
            {
                Owner = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle
            };
        }

        /// <summary>
        /// mm to feet
        /// </summary>
        /// <param name="mm"></param>
        /// <returns></returns>
        public static double ToFeet(this double mm)
        {
            return mm / 304.8;
        }

        /// <summary>
        /// feet to mm
        /// </summary>
        /// <param name="feet"></param>
        /// <returns></returns>
        public static double ToMM(this double feet)
        {
            return feet * 304.8;
        }

        /// <summary>
        /// 获取缩放间距
        /// </summary>
        /// <param name="view"> 视图</param>
        /// <param name="distance"> 100比例时的间距：800mm</param>
        /// <returns> 范围值</returns>
        public static double ScaleDimension(this View view, double distance)
        {
            // 视图缩放 /100.0 * 间距
            return (view.Scale / 100.0) * distance;
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static Element ToElement(this ElementId id, Document doc)
        {
            return doc.GetElement(id);
        }

        /// <summary>
        /// 拍平
        /// </summary>
        /// <param name="xyz"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static XYZ Flatten(this XYZ xyz, double z = 0)
        {
            return new XYZ(xyz.X, xyz.Y, z);
        }

        /// <summary>
        /// 事务组
        /// </summary>
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
        /// 事务
        /// </summary>
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
        /// 警告
        /// </summary>
        private static void IgnoreFailure(Transaction t)
        {
            var fho = t.GetFailureHandlingOptions();
            fho.SetFailuresPreprocessor(new Base.FailuresPreprocessor());
            t.SetFailureHandlingOptions(fho);
        }

        /// <summary>
        /// 过滤器
        /// </summary>
        public static List<T> TCollector<T>(this Document doc)
        {
            return new FilteredElementCollector(doc).OfClass(typeof(T)).Cast<T>().ToList();
        }

        /// <summary>
        /// 过滤器-当前视图
        /// </summary>
        public static List<T> TCollector<T>(this Document doc, View view)
        {
            return new FilteredElementCollector(doc, view.Id).OfClass(typeof(T)).Cast<T>().ToList();
        }

        /// <summary>
        /// 过滤器-类别
        /// </summary>
        public static List<T> TCollector<T>(this Document doc, BuiltInCategory builtInCategory)
        {
            return new FilteredElementCollector(doc).OfClass(typeof(T)).OfCategory(builtInCategory).Cast<T>().ToList();
        }


        /// <summary>
        /// 转换
        /// </summary>
        public static T ToElement<T>(this Reference reference, Document doc) where T : Element
        {
            return doc.GetElement(reference) as T;
        }


    }
}
