using System;
using System.Xml.Serialization;

namespace Comps.DomainLayer
{
    [XmlRoot(Namespace = "app")]
    [Serializable]
    public class AppSetting
    {
        public AppSetting()
        {
            DesktopImagePath = "/content/DesktopImage";
        }
        [XmlElement("DesktopImagePath")]
        public string DesktopImagePath { get; set; }

        [XmlElement("DefaultDesktopImage")]
        public string DefaultDesktopImage  { get; set; }

        public string DesktopThumbImagePath { get { return DesktopImagePath + "/thumb"; } }
    }
}