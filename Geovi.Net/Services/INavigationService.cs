using Geovi.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geovi.Net.Services
{
   public interface INavigationService
   {
      void Push(PagesEnum page, params object[] parameters);
      void Pop();
   }
}
