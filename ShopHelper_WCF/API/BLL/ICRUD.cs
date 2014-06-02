using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ShopHelper_WCF.API.DTO;

namespace ShopHelper_WCF.API.BLL
{
    [ServiceKnownType("GetKnownTypes", typeof(HelperDTO))]
    //[ServiceContract(Name = "NotIService", ConfigurationName = "IService")]
    //[ServiceContract(Namespace = "ServiceLocalhost.ITest")]
    [ServiceContract]
    public interface ICRUD:IProduct,ICategory,IVendor,IStore,ITown
    {
        [OperationContract]
        void Insert(BaseDTO dtoName);

        [OperationContract]
        void Update(BaseDTO dtoName);

        [OperationContract]
        void Delete(BaseDTO dtoName);

        [OperationContract]
        string GetResult();
    }
}
