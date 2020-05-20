using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Windows.Forms;
using System.IO;

namespace New_AUB.Services
{
    class ZipfileServices
    {
        public void CreateZipFile(string _sourcePath, string _destinationPath)
        {

            ZipFile.CreateFromDirectory(_sourcePath, _destinationPath);
        }
        public void ExtractZipFile(string sourcePath, string destinationPath)
        {

            ZipFile.ExtractToDirectory(sourcePath, destinationPath);
        }

        public void ZipFileS(string _processby)
        {

            string sPath = Application.StartupPath + "\\Output";
            string dPath = "K:\\Zips\\isla\\Test\\AFT_" + DateTime.Now.ToString("MMddyyyy") + "_" + _processby + ".zip";
            DeleteZipfile();
            ZipFile.CreateFromDirectory(sPath, dPath);

            ///  CopyZipFile(_processby,);



        }
        public void DeleteZipfile()
        {

            DirectoryInfo di = new DirectoryInfo(Application.StartupPath);
            FileInfo[] files = di.GetFiles("*.zip")
                     .Where(p => p.Extension == ".zip").ToArray();
            foreach (FileInfo file in files)
            {
                file.Attributes = FileAttributes.Normal;
                File.Delete(file.FullName);
            }
        }

    }
}
