using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM.Util.Extensions
{
    public static  class ElementIdExtension
    {
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


    }
}
