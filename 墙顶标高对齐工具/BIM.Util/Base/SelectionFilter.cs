using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;

namespace BIM.Util.Base
{
    public class SelectionFilter : ISelectionFilter
    {
        /// <summary>
        /// 对象委托
        /// </summary>
        private readonly Func<Element, bool> ElFunc;

        /// <summary>
        /// 参照委托
        /// </summary>
        private readonly Func<Reference, bool> RefFunc;

        /// <summary>
        /// 创建选择过滤器
        /// </summary>
        /// <param name="func"> 对象委托</param>
        /// <param name="func1"> 参照委托</param>
        public SelectionFilter(Func<Element, bool> func, Func<Reference, bool> func1)
        {
            ElFunc = func;
            RefFunc = func1;
        }

        /// <summary>
        /// 允许对象
        /// </summary>
        /// <param name="elem"> 对象</param>
        /// <returns></returns>
        public bool AllowElement(Element elem)
        {
            return RefFunc != null || ElFunc(elem);
        }

        /// <summary>
        /// 允许参照
        /// </summary>
        /// <param name="reference"> 参照</param>
        /// <param name="position"> 定位点</param>
        /// <returns></returns>
        public bool AllowReference(Reference reference, XYZ position)
        {
            return RefFunc != null && RefFunc(reference);
        }
    }
}
