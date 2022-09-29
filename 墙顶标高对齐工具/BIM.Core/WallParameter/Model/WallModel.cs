using Autodesk.Revit.DB;
using BIM.Util.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace BIM.Core.WallParameter.Model
{
    public class WallModel
    {
        /// <summary>
        /// 更新对象
        /// </summary>
        public Wall MainWall { get; set; }

        /// <summary>
        /// 中心点
        /// </summary>
        public XYZ Center => (MainWall.get_BoundingBox(View3D).Min + MainWall.get_BoundingBox(null).Max).Multiply(0.5);

        /// <summary>
        /// 视图
        /// </summary>
        private View3D View3D { get; }

        /// <summary>
        /// 底部偏移
        /// </summary>
        /// <returns></returns>
        public XYZ GetTopPoint()
        {
            var filter = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFraming);
            var refIntersector = new ReferenceIntersector(filter, FindReferenceTarget.Face, View3D)
            {
                FindReferencesInRevitLinks = true,
            };
            var referenceWithContext = refIntersector.FindNearest(Center, XYZ.BasisZ);
            if (referenceWithContext == null) refIntersector.SetFilter(new ElementClassFilter(typeof(Floor)));
            referenceWithContext = refIntersector.FindNearest(Center, XYZ.BasisZ);
            if (referenceWithContext == null) return default;
            var reference = referenceWithContext.GetReference();
            return reference.GlobalPoint;
        }

        /// <summary>
        /// 顶部偏移
        /// </summary>
        /// <returns></returns>
        public XYZ GetBasePoint()
        {
            var filter = new ElementClassFilter(typeof(Floor));
            var refIntersector = new ReferenceIntersector(filter, FindReferenceTarget.Face, View3D)
            {
                FindReferencesInRevitLinks = true,
            };
            ReferenceWithContext referenceWithContext = refIntersector.FindNearest(Center, -XYZ.BasisZ);
            if (referenceWithContext == null) return default;
            Reference reference = referenceWithContext.GetReference();
            return reference.GlobalPoint;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="wall"></param>
        public WallModel(Wall wall)
        {
            MainWall = wall;
            View3D = wall.Document.TCollector<View3D>().FirstOrDefault(o => !o.IsTemplate);
        }

    }
}
