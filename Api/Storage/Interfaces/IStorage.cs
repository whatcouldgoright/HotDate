using System.Collections.Generic;
using HotDate.Model;

namespace HotDate.Storage
{
    public interface IStorage
    {
        List<AdHocHoliday> GetAdHocHolidays();
        AdHocHoliday AddAdHocHoliday(AdHocHoliday holiday);
    }
}
