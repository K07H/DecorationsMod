using System.Collections.Generic;

namespace DecorationsMod
{
    public static class DecorationItemsHelper
    {
        /// <summary>
        /// Returns decoration item TechType giving the list of decoration items and the item's classID.
        /// </summary>
        public static TechType getTechType(List<IDecorationItem> decorationItems, string classID)
        {
            foreach (IDecorationItem item in decorationItems)
            {
                if (item.ClassID.CompareTo(classID) == 0)
                    return item.TechType;
            }
            return 0;
        }
    }
}
