using Autodesk.Revit.UI;

namespace BIM.Util
{
    public class XmlDoc
    {

        private XmlDoc() { }
        private static readonly XmlDoc Global_data = new XmlDoc();

        /// <summary>
        /// instance
        /// </summary>
        public static XmlDoc Instance => Global_data ?? new XmlDoc();



        /// <summary>
        /// uidoc
        /// </summary>
        public UIApplication UIapp { get; set; }

        /// <summary>
        /// IExternalEventHandler
        /// </summary>
        public Base.RevitTask Task { get; set; }

    }
}
