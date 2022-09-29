using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;

namespace BIM.Util.Extensions
{
    public static class FilterCollectorExtension
    {
        /// <summary>
        /// 过滤器
        /// </summary>
        /// <typeparam name="T"> 类型</typeparam>
        /// <param name="doc"> 文档</param>
        /// <returns> list 类型</returns>
        public static List<T> TCollector<T>(this Document doc)
        {
            return new FilteredElementCollector(doc).OfClass(typeof(T)).Cast<T>().ToList();
        }

        /// <summary>
        /// 过滤器-当前视图
        /// </summary>
        /// <typeparam name="T"> 类型</typeparam>
        /// <param name="doc"> 文档</param>
        /// <param name="view"> 视图</param>
        /// <returns> list 类型</returns>
        public static List<T> TCollector<T>(this Document doc, View view)
        {
            return new FilteredElementCollector(doc, view.Id).OfClass(typeof(T)).Cast<T>().ToList();
        }

        /// <summary>
        /// 过滤器-类别
        /// </summary>
        /// <typeparam name="T"> 类型</typeparam>
        /// <param name="doc"> 文档</param>
        /// <param name="builtInCategory"> 类型</param>
        /// <returns> list类型</returns>
        public static List<T> TCollector<T>(this Document doc, BuiltInCategory builtInCategory)
        {
            return new FilteredElementCollector(doc).OfClass(typeof(T)).OfCategory(builtInCategory).Cast<T>().ToList();
        }

    }
}
