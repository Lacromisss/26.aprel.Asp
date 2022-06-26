using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WebApplication4.Utilites
{
    public static class ExtentionMethod
    {
        public static bool CheckSize( this IFormFile formFile,int kb )
        {
            if (formFile.Length/1024*1024>kb)
            {
                return true;


            }
            return false;
        }
        public static bool CheckType(this IFormFile formFile, string type)
        {
            if (formFile.ContentType.Contains(type))
            {
                return true;

            }
            return false;
        }
        public async static Task<string> SavaFileAsync(this IFormFile formFile, string pathh)
        {
            string Musi = Guid.NewGuid().ToString() + formFile.FileName;
            string path = Path.Combine(pathh, Musi);
            using (FileStream stream= new FileStream(path, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
            return Musi;
        }

    }
}
