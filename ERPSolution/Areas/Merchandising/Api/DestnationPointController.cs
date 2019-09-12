using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ERPSolution.Areas.Merchandising.Api
{

    [RoutePrefix("api/mrc")]
    public class DestnationPointController : ApiController
    {

        [Route("DestnationPoint/SelectAll")]
        [HttpGet]
        // GET :  mrc/api/ColourMaster/SelectAll
        public IHttpActionResult SelectAll()
        {
            var obList = new MC_DEST_POINTModel().SelectAll();
            return Ok(obList);
        }

        //[Route("ColourMaster/Select/{ID:int}")]
        //[HttpGet]
        //// GET :  mrc/api/ColourMaster/Select
        //public IHttpActionResult Select(Int64 ID)
        //{
        //    var ob = new MC_COLORModel().Select(ID);
        //    return Ok(ob);
        //}

        //[Route("ColourMaster/Save")]
        //[HttpPost]
        //[Authorize]

        //// GET :  mrc/api/ColourMaster/Save
        //public IHttpActionResult Save([FromBody] MC_COLORModel ob)
        //{
        //    string jsonStr = "";

        //    if (ob.ATT_FILE != null && ob.ATT_FILE.ContentLength > 0)
        //    {
        //        var image = Image.FromStream(ob.ATT_FILE.InputStream);
        //        var thumbnailBitmap = new Bitmap(270, 300);

        //        var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
        //        thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
        //        thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
        //        thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;

        //        var imageRectangle = new Rectangle(0, 0, 270, 330);
        //        thumbnailGraph.DrawImage(image, imageRectangle);
        //        thumbnailBitmap.Save(ob.ATT_FILE.FileName, image.RawFormat);
        //        thumbnailGraph.Dispose();
        //        thumbnailBitmap.Dispose();
        //        image.Dispose();

        //        ob.COL_IMAGE = ImageToBase64(image, image.RawFormat);
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            jsonStr = ob.Save();
        //        }
        //        catch (Exception e)
        //        {
        //            ModelState.AddModelError("", e.Message);
        //        }
        //    }
        //    else
        //    {
        //        var errors = new Hashtable();
        //        foreach (var pair in ModelState)
        //        {
        //            if (pair.Value.Errors.Count > 0)
        //            {
        //                errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
        //            }
        //        }
        //        return Ok(new { success = false, errors });
        //    }

        //    return Ok(new { success = true, jsonStr });

        //}


        
    }
}
