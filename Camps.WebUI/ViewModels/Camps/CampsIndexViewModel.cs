using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Comps.DomainLayer;

namespace Camps.WebUI.ViewModels.Camps
{
    public class CampsExistViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CampsIndexViewModel
    {
        public int Id { get; set; }
        public int AreaSize { get; set; }
        public string Name { get; set; }
        public   string State { get; set; }
 
    }
    public class CampsViewModel
    {
        public int Id { get; set; }
        public int AreaSize { get; set; }
        public string Name { get; set; }
        public virtual string State  { get; set; }
        public virtual AddressViewModel   AddressViewModel { get; set; }
        public virtual IList<PhoneViewModel> PhoneViewModels { get; set; }
        public virtual IList<GalleryViewModel> GallaryViewModels { get; set; }
        public virtual IList<SuiteViewModel> SuitesViewModels { get; set; }
        public virtual IList<CampFacilitieViewModel> CampFacilitiesViewModels { get; set; }
    }
    public class CampsCreateViewModel
    {
        public int AreaSize { get; set; }
        public string Name { get; set; }
        public virtual AddressViewModel Address { get; set; }
        public virtual ICollection<PhoneViewModel> Phones { get; set; }
        public virtual ICollection<GalleryShortViewModel> Galleries { get; set; }
        //public virtual IList<Suite> Suites { get; set; }
        //public virtual IList<CampFacilitie> CampFacilities { get; set; }
    }
    public class ProvinceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
    }
 
    public class AddressViewModel
    {
        public int Id { get; set; }
         [Required, MaxLength(520)]
        public string FullAddress { get; set; }

        [Required, MaxLength(3)]
        public string State { get; set; }
        [Required, MaxLength(20)]
        public string Zip { get; set; }
        public double Longitude { set; get; }
        public double Latitude { set; get; }
        public  CityViewModel City { get; set; }
    
    }
    public class PhoneViewModel
    {
        public string PhoneNumber { get; set; }

    }
    public class BaseFileViewModel
    {
        public string Name { get; set; }
        public string Extention { get; set; }
        public long? Size { get; set; }
        public DateTime UploadDate { get; set; }
        public Guid Guid { get; set; }
        public string Desctiption { get; set; }
    }
    public class PhotoViewModel : BaseFileViewModel
    {

    }
    public class AudioViewModel : BaseFileViewModel
    {

    }
    public class VideoViewModel : BaseFileViewModel
    {

    }
    public class FileViewModel : BaseFileViewModel
    {

    }

    public class GalleryShortViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class GalleryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IList<PhotoViewModel> Photos { get; set; }
        public virtual IList<AudioViewModel> Audios { get; set; }
        public virtual IList<VideoViewModel> Videos { get; set; }
    }
    public class SuiteViewModel
    {

        public string SuiteName { get; set; }
        public int SuiteNumber { get; set; }
        public string Description { get; set; }
        public GalleryViewModel Gallery { get; set; }
    }
    public class CampFacilitieViewModel
    {

    }

}