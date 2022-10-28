using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_CRUD_ADO_OAUTH.DataAccessLayer;
using MVC_CRUD_ADO_OAUTH.Models;

namespace MVC_CRUD_ADO_OAUTH.Controllers
{
    public class ProductsController : Controller
    {
        
        ProductsDataAccess productsDataAccess =new ProductsDataAccess();
        // GET: Products
        public ActionResult Index() //get the data from the database
        {
            var productList =productsDataAccess.GetAllProducts();
            if(productList.Count ==0)
            {
                TempData["InfoMessage"] = "Currently products is not available in the database.";
            }
            return View(productList);   
            
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var products = productsDataAccess.GetProductByID(id).FirstOrDefault();
                if (products == null)
                {
                    TempData["InfoMessage"] = "Product not available with id" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(products);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Products/Create
        public ActionResult Create( )
        {
           return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(Products products)
        {
            try
            {
                bool IsInserted = false;
                if (ModelState.IsValid)
                {
                    IsInserted = productsDataAccess.InsertProduct(products);
                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Product details saved successfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to save the product details.";
                    }

                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            var products= productsDataAccess.GetProductByID(id).FirstOrDefault();
            if(products ==null)
            {
                TempData["InfoMessage"] = "Product not available with ID" + id.ToString();
                return RedirectToAction("Index");
            }
           
            return View(products);
        }

        // POST: Products/Edit/5
        [HttpPost,ActionName("Edit")]
        public ActionResult UpdateProduct(Products products)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = productsDataAccess.UpdateProduct(products);

                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Product details updated successfully..";

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to save the product details.";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var product = productsDataAccess.GetProductByID(id).FirstOrDefault();
                if (product == null)
                {
                    TempData["InfoMessage"] = "Product not available with id" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: Products/Delete/5
        [HttpPost ,ActionName("Delete")]
        public ActionResult DeleteConfirmation  (int id)
        {
            try
            {
                string result = productsDataAccess.DeleteProduct(id);
                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                 return View();
            }
            
        }
    }
}
