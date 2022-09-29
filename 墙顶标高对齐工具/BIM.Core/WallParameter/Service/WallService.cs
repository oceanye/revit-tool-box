using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using BIM.Core.WallParameter.Model;
using BIM.Util;
using BIM.Util.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BIM.Core.WallParameter.Service
{
    internal class WallService : IWallService
    {
        /// <summary>
        /// uidoc
        /// </summary>
        public UIDocument UIdoc { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="uidoc"></param>
        public WallService(UIDocument uidoc)
        {
            UIdoc = uidoc;
        }

        public bool ArrangeMode_UP;
        public bool ArrangeMode_DOWN;

        /// <summary>
        /// 获取墙
        /// </summary>
        /// <returns></returns>
        public List<Wall> GetWalls()
        {
            #region 手动多选左上角完成 "PickObjuects多选，完成，取消三个按钮"
            //try
            //{
            //    var sel = UIdoc.Selection;
            //    var doc = UIdoc.Document;
            //    var refs = sel.PickObjects(ObjectType.Element, SelFilter.Set(o => o is Wall), "选择需要对齐的墙");
            //    return refs.Select(o => o.ToElement<Wall>(doc)).ToList();
            //}
            //catch
            //{
            //    return default;
            //}
            #endregion

            #region 自动选择当前视图
            //var doc = UIdoc.Document;
            //return doc.TCollector<Wall>(doc.ActiveView);
            #endregion

            #region 新增选择界面

    

            var doc = UIdoc.Document;
            var sel = UIdoc.Selection;
            var refs = sel.PickObjects(ObjectType.Element, SelFilter.Set(o => o is Wall), "选择需要对齐的墙");

            var form = new Wall_Beam_Form();
            form.ShowDialog();
            ArrangeMode_UP = form.ArrangeMode_UP;
            ArrangeMode_DOWN = form.ArrangeMode_DOWN;


            return refs.Select(o => o.ToElement<Wall>(doc)).ToList();
            #endregion

        }

        /// <summary>
        /// 更新墙
        /// </summary>
        /// <param name="walls"></param>
        /// <param name="ids"></param>
        public int SetWalls(List<Wall> walls)
        {
            var count = 0;
            var doc = UIdoc.Document;
            doc.InvokeGroup(tg =>
            {
                walls.ForEach(wall => SetWall(doc, wall, ref count));
            }, "墙对齐");
            return count;
        }

        /// <summary>
        /// 更新墙
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="wall"></param>
        private void SetWall(Document doc, Wall wall, ref int count)
        {
            try
            {
                doc.Invoke(t =>
                {
                    var wallModel = new WallModel(wall);
                    var center = wallModel.Center;
                    var basePnt = wallModel.GetBasePoint();
                    var topPnt = wallModel.GetTopPoint();
                    var id = wall.get_Parameter(BuiltInParameter.WALL_BASE_CONSTRAINT).AsElementId();
                    var level = id.ToElement(doc) as Level;



                    if (ArrangeMode_DOWN)
                    {
                        if (basePnt == null)
                            basePnt = wall.get_BoundingBox(null).Min;
                        {
                            // 底部偏移
                            var p = wall.get_Parameter(BuiltInParameter.WALL_BASE_OFFSET);
                            p.Set(basePnt.Z - level.Elevation);
                        }
                    }


                    if (ArrangeMode_UP)
                    {
                        if (topPnt != null)
                        {
                            // 顶部偏移
                            var p = wall.get_Parameter(BuiltInParameter.WALL_TOP_OFFSET);
                            if (p.IsReadOnly)
                            {
                                // 无法链接高度
                                p = wall.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM);
                                p.Set(topPnt.Z - basePnt.Z);
                            }
                            else
                            {
                                id = wall.get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE).AsElementId();
                                level = id.ToElement(doc) as Level;
                                p.Set(topPnt.Z - level.Elevation);
                            }
                        }
                    }
                 });
                count++;
            }
            catch (Exception ex)
            {
                UI.Print($"更新墙:{wall.Id},失败:{ex.Message}");
                Log.Debug($"更新墙:{wall.Id},失败:{ex}");
            }
        }
    }
}
