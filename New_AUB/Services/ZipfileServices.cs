using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Windows.Forms;
using System.IO;
using New_AUB.Models;

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

        public void ZipFileS(string _processby, frmMain main)
        {

            string sPath = Application.StartupPath + "\\Output\\" + frmMain.outputFolder;
            string dPath = Application.StartupPath+ "\\Output\\AFT_" + frmMain.batch+ "_" + _processby + ".zip";
            DeleteFiles(".zip",Application.StartupPath + "\\Output");
            ZipFile.CreateFromDirectory(sPath, dPath);
            Ionic.Zip.ZipFile zips = new Ionic.Zip.ZipFile(dPath);
            //Adding order file to zip file
            zips.AddItem(Application.StartupPath + "\\Head");

            zips.Save();

        }
        public void ZipFileRb(string _processby, frmMain main,List<OrderModelRb> _orders)
        {

            string sPath = sPath = Application.StartupPath + "\\Output\\";
            string dPath = Application.StartupPath + "\\Output\\AFT_" + main.batchfile + "_" + _processby + ".zip";
            DeleteFiles(".zip", Application.StartupPath + "\\Output");
            ZipFile.CreateFromDirectory(Application.StartupPath + "\\Head", dPath);
            Ionic.Zip.ZipFile zips = new Ionic.Zip.ZipFile(dPath);
            string path = "";

            //Adding order file to zip file
            //  zips.AddItem(Application.StartupPath + "\\Head");
            for (int i = 0; i < _orders.Count; i++)
            {
               
                    sPath = Application.StartupPath + "\\Output\\" + _orders[i].BankName;

                if (sPath == path )
                    //sPath = Application.StartupPath + "\\Output\\" + _orders[i].BankName;
                    i++;

                else
                {
                    zips.AddDirectory(sPath, _orders[i].BankName);
                    zips.Save();
                    zips.Dispose();
                    path = Application.StartupPath + "\\Output\\" + _orders[i].BankName;
                }
            }

           

        }
        public void DeleteFiles(string _ext,string _path)
        {

            DirectoryInfo di = new DirectoryInfo(_path);
            FileInfo[] files = di.GetFiles("*" +_ext)
                     .Where(p => p.Extension == _ext).ToArray();
            foreach (FileInfo file in files)
            {
                file.Attributes = FileAttributes.Normal;
                File.Delete(file.FullName);
            }
        }
        public void CopyZipFile(string _processby, frmMain main)
        {
            string dPath = @"K:\Zips\AUB\Test" + @"\" + DateTime.Now.Year +"\\"+frmMain.outputFolder+ @"\AFT_" + frmMain.batch + "_" + _processby + ".zip";
            string sPath = Application.StartupPath + "\\Output\\AFT_" + frmMain.batch + "_" + _processby + ".zip";
            File.Copy(sPath, dPath, true);
          
        }
        public static void CopyPrinterFile(string _processby, frmMain main, string _filename)
        {
            string dPath = @"R:\AUB\Test\" + DateTime.Now.Year+"\\"+ frmMain.outputFolder + "\\" + _filename;
            string sPath = Application.StartupPath  + "\\Output\\" + frmMain.outputFolder + "\\" + _filename;
            File.Copy(sPath, dPath, true);
            //string dPath2 = "\\\\192.168.0.254\\PrinterFiles\\ISLA\\2019\\";
            //string sPath2 = "\\\\192.168.0.254\\captive\\Auto\\IslaBank\\Test\\";

        }
        public static void CopyPacking(string _processby, frmMain main)
        {

            string dPath = @"Z:\AUB\Test\" + DateTime.Now.Year + "\\"  +frmMain.outputFolder +   "\\";
            string sPath = Application.StartupPath +"\\Output\\" + frmMain.outputFolder + "\\Packing.dbf";
            {
                Directory.CreateDirectory(dPath + main.batchfile);
            }
            string dpath2 = dPath + "\\" + main.batchfile;

            File.Copy(sPath, dpath2 + "\\Packing.dbf", true);
            //string dPath2 = "\\\\192.168.0.254\\PrinterFiles\\ISLA\\2019\\";
            //string sPath2 = "\\\\192.168.0.254\\captive\\Auto\\IslaBank\\Test\\";

        }
    }
}
