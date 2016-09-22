using EoraMarketpalce.Web.Common.Constants;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace EoraMarketpalce.Web.Common
{
    public class ImageManager
    {
        private const string FULL_PATH_DIVIDER = "//";
        private const string REL_PATH_DIVIDER = "\\";
        private const string IMAGE_BASE64_PREFIX = "";
        private const string CONTENT_ROOT_PATH = "Content";
        private const string IMAGES_ROOT_PATH = "Images";
        private const string PRODUCTS_IMAGES_ROOT_PATH = "Products";
        private readonly string _rootPath = "";
        private readonly string _imageRootPath = "";

        /// <summary>
        ///     Ctor.
        /// </summary>
        public ImageManager()
        {
            _rootPath = HostingEnvironment.MapPath("/");
            _imageRootPath = Path.Combine(_rootPath, CONTENT_ROOT_PATH, IMAGES_ROOT_PATH, PRODUCTS_IMAGES_ROOT_PATH);
        }

        public static ImageManager GetSaverInstance()
        {
            return Activator.CreateInstance<ImageManager>();
        }

        public static string ToFullPath(string relativePath)
        {
            if(Path.IsPathRooted(relativePath))
                return relativePath;

            return HttpContext.Current.Server.MapPath(relativePath);
        }

        public static string ToVirtualPath(string fullPath)
        {
            if(Path.IsPathRooted(fullPath))
            {
                string path = Path.Combine(fullPath);
                var root = HostingEnvironment.ApplicationPhysicalPath;

                path = path.Replace(root, "")
                    .Replace(FULL_PATH_DIVIDER, REL_PATH_DIVIDER);

                return string.Format("~/{0}", path);
            }

            return fullPath;
        }

        public static string UrlToHtmlValid(string virtpath)
        {
            return virtpath.Replace("~", "");
        }

        public string SaveImage(string base64data)
        {
            Image image = ReadImage(base64data);

            string imagePath = Path.Combine(_imageRootPath, GetUniqueName());

            var rel = ToVirtualPath(imagePath);

            try
            {
                image.Save(imagePath, image.RawFormat);
            }
            catch(Exception exc)
            {
                return AppConsts.DEFAULT_IMAGE_URL;
            }

            return imagePath;
        }

        private string GetUniqueName()
        {
            return string.Format("product-{0}.png", DateTime.Now.Ticks);
        }

        private Image ReadImage(string data)
        {
            data = data.Split(',')[1];

            byte[] bytes = Convert.FromBase64String(data);
            Image image;
            using(MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }
            return image;
        }
    }
}
