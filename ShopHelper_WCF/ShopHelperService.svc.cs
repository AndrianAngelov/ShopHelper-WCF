using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ShopHelper_WCF.API.BLL;
using ShopHelper_WCF.API.DAL;
using ShopHelper_WCF.API.DTO;

namespace ShopHelper_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    //[ServiceBehavior(Name = "NotService", ConfigurationName = "Service")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ShopHelperService : ICRUD
    {
        private static string resultMessage;

        public List<ProductDTO> GetProducts()
        {
            ProductsDAL getProducts = new ProductsDAL();
            List<ProductDTO> allProducts = getProducts.GetAllProducts();
            return allProducts;
        }

        public List<CategoryDTO> GetCategories()
        {
            CategoriesDAL getCategories = new CategoriesDAL();
            List<CategoryDTO> allCategories = getCategories.GetAllCategories();
            return allCategories;
        }

        public List<VendorDTO> GetVendors()
        {
            VendorsDAL getVendors = new VendorsDAL();
            List<VendorDTO> allVendors = getVendors.GetAllVendors();
            return allVendors;
        }

        public List<StoreDTO> GetStores()
        {
            StoresDAL getStores = new StoresDAL();
            List<StoreDTO> allStores = getStores.GetAllStores();
            return allStores;
        }

        public List<TownDTO> GetTowns()
        {
            TownsDAL getTowns = new TownsDAL();
            List<TownDTO> allTowns = getTowns.GetAllTowns();
            return allTowns;
        }

        public void Insert(BaseDTO dtoName)
        {
            Type currentType = dtoName.GetType();

            if (currentType.Name.Equals("ProductDTO"))
            {
                ProductsDAL newProduct = new ProductsDAL();
                if (newProduct.IsProductNameExistInDb((dtoName as ProductDTO).ProductNewName))
                {
                    resultMessage = BaseDAL.Messages(MessageFor.ItemExistInDatabase, "Product");
                }
                else
                {
                    newProduct.InsertNewProduct((dtoName as ProductDTO).ProductNewName,
                                                (dtoName as ProductDTO).CategoryName,
                                                (dtoName as ProductDTO).VendorName,
                                                (dtoName as ProductDTO).ProductPrice,
                                                (dtoName as ProductDTO).Quantity,
                                                (dtoName as ProductDTO).StoreName,
                                                (dtoName as ProductDTO).TownName);
                    resultMessage = newProduct.GetCRUDResult();
                }
            }
            if (currentType.Name.Equals("VendorDTO"))
            {
                VendorsDAL newVendor = new VendorsDAL();
                if (newVendor.IsVendorNameExistInDb((dtoName as VendorDTO).VendorName))
                {
                    resultMessage = BaseDAL.Messages(MessageFor.ItemExistInDatabase,"Vendor");
                }
                else
                {
                    newVendor.InsertNewVendor((dtoName as VendorDTO).VendorName);
                    resultMessage = newVendor.GetCRUDResult();
                }
            }
            if (currentType.Name.Equals("CategoryDTO"))
            {
                CategoriesDAL newCategory = new CategoriesDAL();
                if (newCategory.IsCategoryNameExistInDb((dtoName as CategoryDTO).CategoryName))
                {
                    resultMessage = BaseDAL.Messages(MessageFor.ItemExistInDatabase, "Category");
                }
                else
                {
                    newCategory.InsertNewCategory((dtoName as CategoryDTO).CategoryName);
                    resultMessage = newCategory.GetCRUDResult();
                }
            }
            else
            {
                //MessageBox.Show("Pone stigna do tuk");

            }
        }

        public void Update(BaseDTO dtoName)
        {
            Type currentType = dtoName.GetType();

            if (currentType.Name.Equals("ProductDTO"))
            {
                ProductsDAL updateProduct = new ProductsDAL();
                updateProduct.UpdateProduct((dtoName as ProductDTO).ProductOldName,
                                            (dtoName as ProductDTO).ProductNewName,
                                            (dtoName as ProductDTO).CategoryName,
                                            (dtoName as ProductDTO).VendorName,
                                            (dtoName as ProductDTO).ProductPrice,
                                            (dtoName as ProductDTO).Quantity,
                                            (dtoName as ProductDTO).StoreName,
                                            (dtoName as ProductDTO).TownName);
                resultMessage = updateProduct.GetCRUDResult();
            }
            if (currentType.Name.Equals("VendorDTO"))
            {
                VendorsDAL updateVendor = new VendorsDAL();
                updateVendor.UpdateVendor((dtoName as VendorDTO).VendorOldName, (dtoName as VendorDTO).VendorName);
                resultMessage = updateVendor.GetCRUDResult();
            }
            if (currentType.Name.Equals("CategoryDTO"))
            {
                CategoriesDAL updateCategory = new CategoriesDAL();
                updateCategory.UpdateCategory((dtoName as CategoryDTO).CategoryOldName, (dtoName as CategoryDTO).CategoryName);
                resultMessage = updateCategory.GetCRUDResult();
            }
            else
            {
                //MessageBox.Show("Pone stigna do tuk v update seckciqta");
            }
        }

        public void Delete(BaseDTO dtoName)
        {
            Type currentType = dtoName.GetType();

            if (currentType.Name.Equals("ProductDTO"))
            {
                ProductsDAL deleteProduct = new ProductsDAL();
                deleteProduct.DeleteProduct((dtoName as ProductDTO).ProductNewName);
                resultMessage = deleteProduct.GetCRUDResult();
            }
            if (currentType.Name.Equals("VendorDTO"))
            {
                VendorsDAL deleteVendor = new VendorsDAL();
                deleteVendor.DeleteVendor((dtoName as VendorDTO).VendorName);
                resultMessage = deleteVendor.GetCRUDResult();
            }
            if (currentType.Name.Equals("CategoryDTO"))
            {
                CategoriesDAL deleteCategory = new CategoriesDAL();
                deleteCategory.DeleteCategory((dtoName as CategoryDTO).CategoryName);
                resultMessage = deleteCategory.GetCRUDResult();
            }
            else
            {
                //MessageBox.Show("Pone stigna do tuk v update seckciqta");
            }
        }

        public string GetResult()
        {
            return resultMessage;
        }
    }
}
