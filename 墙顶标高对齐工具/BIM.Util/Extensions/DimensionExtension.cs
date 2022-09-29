using Autodesk.Revit.DB;

namespace BIM.Util.Extensions
{
    public static class DimensionExtension
    {
        /// <summary>
        /// 偏移距离
        /// </summary>
        /// <param name="dimension"></param>
        /// <param name="doc"></param>
        /// <param name="translation"></param>
        public static void Transform(this Dimension dimension, Document doc, XYZ translation)
        {
            doc.Regenerate();
            ElementTransformUtils.MoveElement(doc, dimension.Id, translation);
        }
    }
}
