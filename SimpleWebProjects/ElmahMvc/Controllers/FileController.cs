using ElmahMvc.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.Protocols;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ElmahMvc.Controllers
{
    public class FileController : Controller
    {
        public static string FriendlyDomainToLdapDomain(string friendlyDomainName)
        {
            string ldapPath = null;
            try
            {
                DirectoryContext objContext = new DirectoryContext(
                    DirectoryContextType.Domain, friendlyDomainName);
                Domain objDomain = Domain.GetDomain(objContext);
                ldapPath = objDomain.Name;
            }
            catch (DirectoryServicesCOMException e)
            {
                ldapPath = e.Message.ToString();
            }
            return ldapPath;
        }

        public static bool fnValidateUser()
        {
            bool validation;
            try
            {
                DirectoryEntry de = new DirectoryEntry();
                de.Path = "LDAP://ec2-13-126-226-199.ap-south-1.compute.amazonaws.com/CN=Users,dc=demo,dc=demo,dc=com";
                de.Username = @"demo\vardhan";
                de.Password = "Active@1529";
                DirectorySearcher deSearch = new DirectorySearcher();
                deSearch.Filter = "(cn=jayavardhan)";
                deSearch.SearchRoot = de;
                SearchResult results = deSearch.FindOne();
                validation = true;
            }
            catch (LdapException ex)
            {
                validation = false;
            }
            return validation;
        }

        [HttpGet]
        public ActionResult UploadTutorial()
        {
            fnValidateUser();
            return View("Upload");
        }

        // GET: File
        [HttpPost]
        public ActionResult UploadTutorial(TutorialInfo info)
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase data)
        {
            if (data == null)
            {
                Response.StatusCode = 400;
                Response.Write("Invalid file data");
                return null;
            }

            var id = Guid.NewGuid().ToString();
            data.SaveAs(Server.MapPath("~/Tutorial_Files/" + id + "." + data.FileName.Split('.').Last()));
            return new JsonResult() { Data = new { id = id }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult Download(string filename)
        {
            var path = Server.MapPath("~/Tutorial_Files/" + filename);
            if(System.IO.File.Exists(path))
            {
                //Response.Headers.Add("Content-Disposition", "attachment; filename=receipt.pdf");
                return File(path, "text/plain", filename);
            }

            return new EmptyResult();
        }



    }
}