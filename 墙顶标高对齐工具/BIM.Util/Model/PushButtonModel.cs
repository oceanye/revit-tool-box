using System.Drawing;
using System.IO;

namespace BIM.Util.Model
{
    public class PushButtonModel
    {
        private string inName = System.Guid.NewGuid().ToString();
        private string buttonName;
        private string nameSpace;
        private string assemblyName = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "BIM.Core.dll");
        private string tooltip;
        private Image imageName;
        private Image stackedImageName;
        private Image tooltipImage;
        private string description;

        /// <summary>
        /// 内部名称
        /// </summary>
        public string InName
        {
            get { return inName; }
            set { inName = value; }
        }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string ButtonName
        {
            get { return buttonName; }
            set { buttonName = value; }
        }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace
        {
            get { return nameSpace; }
            set { nameSpace = value; }
        }

        /// <summary>
        /// 程序集路径
        /// </summary>
        public string AssemblyName
        {
            get { return assemblyName; }
            set { assemblyName = value; }
        }

        /// <summary>
        /// 提示文字
        /// </summary>
        public string Tooltip
        {
            get { return tooltip; }
            set { tooltip = value; }
        }

        /// <summary>
        /// 大图名称
        /// 
        /// 32*32
        /// </summary>
        public Image ImageName
        {
            get { return imageName; }
            set { imageName = value; }
        }

        /// <summary>
        /// 小图名称
        /// 
        /// 16*16
        /// </summary>
        public Image StackedImageName
        {
            get { return stackedImageName; }
            set { stackedImageName = value; }
        }

        /// <summary>
        /// 长提示
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// 提示图片
        /// 
        /// 355*355
        /// </summary>
        public Image TooltipImage
        {
            get { return tooltipImage; }
            set { tooltipImage = value; }
        }

    }
}
