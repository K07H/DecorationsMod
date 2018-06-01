using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecorationsFabricator
{
    public static class DecorationItemsHelper
    {
        public static TechType getTechType(List<DecorationItem> decorationItems, string classID)
        {
            foreach (DecorationItem item in decorationItems)
            {
                if (item.ClassID.CompareTo(classID) == 0)
                    return item.TechType;
            }
            return 0;
        }
    }
}
