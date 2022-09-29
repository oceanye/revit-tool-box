using Autodesk.Revit.DB;
using BIM.Util.Base;
using System;

namespace BIM.Util
{
    public static class SelFilter
    {
        public static SelectionFilter Set(Func<Element, bool> func1, Func<Reference, bool> func2 = null)
        {
            return new SelectionFilter(func1, func2);
        }
    }
}
