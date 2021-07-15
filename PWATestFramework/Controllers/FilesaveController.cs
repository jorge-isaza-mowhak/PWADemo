
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Cors;
using System.Net.Http;
using System.Web.Http;
using Inlite.ClearImageNet;
using System.Xml;
using System.Xml.Linq;
using Inlite.Data;

namespace FileUpload.Controllers
{
    public class FilesaveController : ApiController
    {

        [EnableCors(origins: "http://localhost:49330", headers: "*", methods: "*")]
        [System.Web.Http.HttpPost()]
        public DriverInfo UploadFile()
        {
            int iUploadedCnt = 0;

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/locker/");

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            String fileName = "";
            fileName = sPath + Path.GetFileName(hfc[0].FileName);

            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                    if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                    {
                        fileName = sPath + Path.GetFileName(hpf.FileName);
                        // SAVE THE FILES IN THE FOLDER.
                        hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                        iUploadedCnt = iUploadedCnt + 1;
                    }
                }
            }

            // RETURN A MESSAGE (OPTIONAL).
            if (iUploadedCnt >= 0)
            {
                BarcodeReader reader = null;
                reader = new BarcodeReader();
                int page = 0;

                reader.Horizontal = true; reader.Vertical = true; reader.Diagonal = true;
                //configure types
                reader.DrvLicID = true;
                Barcode[] barcodes = reader.Read(fileName, page);
                string s = ""; int cnt = 0;
                foreach (Barcode bc in barcodes)
                {
                    cnt++;
                    if (bc.Type == BarcodeType.Pdf417)
                    {

                        DLDecoder decoder = new DLDecoder();
                        decoder.Decode(barcodes[0].Text);

                        s = decoder.first + " " + decoder.last;



                    }
                    //AddBarcode(ref s, cnt, bc);
                }

                DriverInfo dInf = new DriverInfo() { DriverName = s };
                return dInf;
            }
            else
            {
                DriverInfo dInf = new DriverInfo() { DriverName = "Fail" };
                return dInf;
            }
        }

        public class DriverInfo
        {
            public string DriverName { get; set; }
        }


        private void AddBarcode(ref string s, long i, Barcode Bc)
        {
            s = s + "Barcode#:" + i.ToString();
            if (Bc.File != "") s += "  File:" + Bc.File;
            s = s + " Page:" + Bc.Page.ToString() + Environment.NewLine;
            s = s + " Type:" + System.Enum.GetName(Bc.Type.GetType(), Bc.Type);
            s = s + " Lng:" + Bc.Length.ToString();
            // s = s + Environment.NewLine + "   "; 
            s = s + " Rect:" + Bc.Rectangle.Left.ToString() + ":" + Bc.Rectangle.Top.ToString() + "-" + Bc.Rectangle.Right.ToString() + ":" + Bc.Rectangle.Bottom.ToString();
            s = s + " Rotation:" + System.Enum.GetName(Bc.Rotation.GetType(), Bc.Rotation);
            // Try to decompress 2D Barcode data
            if (Bc.Type == BarcodeType.Pdf417 || Bc.Type == BarcodeType.DataMatrix || Bc.Type == BarcodeType.QR)
            {
                string decomp = Bc.Decode(BarcodeDecoding.compA);
                if (decomp != "")
                    s = s + Environment.NewLine + Environment.NewLine + "DECOMPRESSED BARCODE DATA (A):" + Environment.NewLine + decomp + Environment.NewLine;
                decomp = Bc.Decode(BarcodeDecoding.compI);
                if (decomp != "")
                    s = s + Environment.NewLine + Environment.NewLine + "DECOMPRESSED BARCODE DATA (I):" + Environment.NewLine + decomp + Environment.NewLine;
            }
            // Show raw  data
            s = s + Environment.NewLine + "RAW BARCODE DATA:" + Environment.NewLine + Bc.Text;
            s = s + Environment.NewLine + "--------------" + Environment.NewLine;
        }


    }
}