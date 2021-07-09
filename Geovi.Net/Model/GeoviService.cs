
using Esri.ArcGISRuntime.ArcGISServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Geovi.Net.Model
{
   public class GeoviService
   {
      [Key]
      public int GeoviServiceID { get; set; }

      /// <summary>
      /// 
      /// </summary>
      public string Title { get; set; }

      /// <summary>
      /// 
      /// </summary>
      public Uri ServiceUrl { get; set; }

      /// <summary>
      /// 
      /// </summary>
      public ServiceType ServiceType { get; set; }

      /// <summary>
      /// 
      /// </summary>
      public string LayerName { get; set; }

      /// <summary>
      /// 
      /// </summary>
      public string Description { get; set; }

      /// <summary>
      /// 
      /// </summary>
      public string ParentName { get; set; }

      /// <summary>
      /// 
      /// </summary>
      public GeoviService() { }
   }
}
