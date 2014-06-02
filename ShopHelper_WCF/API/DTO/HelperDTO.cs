using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace ShopHelper_WCF.API.DTO
{
    static class HelperDTO
    {
        public static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider provider)
        //public static IEnumerable<Type> GetKnownTypes()
        {
            List<Type> knownTypes = new List<Type>();
            // Add any types to include here.
            knownTypes.Add(typeof(BaseDTO));
            knownTypes.Add(typeof(CommonDTO));
            knownTypes.Add(typeof(ProductDTO));
            knownTypes.Add(typeof(CategoryDTO));
            knownTypes.Add(typeof(VendorDTO));
            knownTypes.Add(typeof(StoreDTO));
            knownTypes.Add(typeof(TownDTO));
            return knownTypes;
        }
    }
}