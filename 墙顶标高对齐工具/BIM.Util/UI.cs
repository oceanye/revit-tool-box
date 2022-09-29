using Autodesk.Revit.UI;

namespace BIM.Util
{
    public static class UI
    {
        /// <summary>
        /// title
        /// </summary>
        private const string Title = "弹窗";

        /// <summary>
        /// Instruction & Icon
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="caption"></param>
        /// <param name="icon"></param>
        private static void Init(WindowType windowType, ref string caption, ref TaskDialogIcon icon)
        {
            switch (windowType)
            {
                case WindowType.Information:
                    caption = "提示";
                    icon = TaskDialogIcon.TaskDialogIconInformation;
                    break;
                case WindowType.Warning:
                    caption = "警告";
                    icon = TaskDialogIcon.TaskDialogIconWarning;
                    break;
                case WindowType.Error:
                    caption = "错误";
                    icon = TaskDialogIcon.TaskDialogIconError;
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 提示
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="windowType"></param>
        public static bool Print<T>(T message,
            WindowType windowType = WindowType.Information)
        {
            var icon = TaskDialogIcon.TaskDialogIconNone;
            var caption = string.Empty;
            Init(windowType, ref caption, ref icon);
            var window = new TaskDialog(Title)
            {
                MainInstruction = caption,
                MainContent = $"{message}",
                MainIcon = icon,
                CommonButtons = TaskDialogCommonButtons.Ok
            };
            var tResult = window.Show();
            return tResult == TaskDialogResult.Ok;
        }

    }

    /// <summary>
    /// 类型
    /// </summary>
    public enum WindowType
    {
        /// <summary>
        /// 提示
        /// </summary>
        Information = 0,

        /// <summary>
        /// 警告
        /// </summary>
        Warning = 1,

        /// <summary>
        /// 错误
        /// </summary>
        Error = 2,
    }
}
