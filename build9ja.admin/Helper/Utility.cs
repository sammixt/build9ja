using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.admin.Helper
{
    public class Utility 
    {
        
        public static string ProcessUploadedFile(IFormFile model,string path)
        {
            string uniqueFileName = null;

            if (model != null)
            {
                uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.FileName);
                string filePath = Path.Combine(path, uniqueFileName);
                //filePath = filePath.Replace("\\/", "\\");
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}