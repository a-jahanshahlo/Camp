using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Camps.WebUI.ViewModels.Galleries
{
    public class File
    {
        public string FileId { get; set; }
    }
    public class AddedFileToGalleryViewModel
    {
        public int Id { get; set; }
        public File[] Files { get; set; }
    }
    public class EditGalleryViewModel
    {
        public int Id { get; set; }
        public string GalleryName { get; set; }
    }
}