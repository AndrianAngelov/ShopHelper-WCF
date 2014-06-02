using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ShopHelper_WCF.API.DTO;

namespace ShopHelper_WCF.API.BLL
{
    [ServiceContract]
    public interface ITown
    {
        [OperationContract]
        List<TownDTO> GetTowns();
    }
}
