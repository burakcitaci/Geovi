using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Geovi.Net.Enums
{
   public enum Enums
   {
   }

   public enum PagesEnum
   {
      GeoviMainPage, GeoviDetailPage, WfsDataPage, SettingsPage, SettingsDetailPage, FavoritesPage, CupPage, WfsDetailPage
   }

   public enum ServiceType
   {
      [Description("Wfs Service")]
      WfsService = 0,
      [Description("Wms Service")]
      WmsService = 1,
      [Description("Wmts Service")]
      WmtsService = 2,
      [Description("Feature Service")]
      FeatueService = 3
   }
}
