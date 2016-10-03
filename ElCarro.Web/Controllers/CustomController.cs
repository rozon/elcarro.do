using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ElCarro.Web.Controllers
{
    public class CustomController : Controller
    {
        protected string GetUserId() => User.Identity.GetUserId();

        protected string SavePhoto(HttpPostedFileBase Photo)
        {
            if (null == Photo)
                return string.Empty;

            var path = ConfigurationManager.AppSettings["FileUplodasFolder"];
            var name = Guid.NewGuid().ToString() + Path.GetExtension(Photo.FileName);
            var fullPath = "~/" + path + "/" + name;
            using (var f = new FileStream(ControllerContext.HttpContext.Server.MapPath(fullPath), FileMode.CreateNew))
            {
                Photo
                    .InputStream
                    .CopyTo(f);
            }
            return fullPath;
        }

        protected bool DeletePhoto(string actualPhotoPath)
        {
            if (!string.IsNullOrEmpty(actualPhotoPath))
            {
                System.IO.File.Delete(ControllerContext.HttpContext.Server.MapPath(actualPhotoPath));
                return true;
            }
            return false;
        }

        protected string OnlyNumbers(string _string)
        {
            if (string.IsNullOrEmpty(_string))
                return string.Empty;

            string pattern = "[^0-9]*";
            string replacement = "";
            Regex rgx = new Regex(pattern);
            return rgx.Replace(_string, replacement);
        }

        protected string FormatPhoneNumber(string PhoneNumber)
        {
            if (string.IsNullOrEmpty(PhoneNumber))
                return string.Empty;

            PhoneNumber = OnlyNumbers(PhoneNumber);

            int counter = 2;
            if (PhoneNumber.Length == 10)
                counter = 4;

            while (counter < 11)
            {
                if (PhoneNumber.Length >= counter)
                    PhoneNumber = PhoneNumber.Insert((counter - 1), "-");
                counter += 4;
            }

            return PhoneNumber;
        }
    }
}
