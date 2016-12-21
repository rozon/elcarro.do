using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using ElCarro.Web.Models;
using System.Threading.Tasks;
using System.Web;
using ElCarro.Web.StringResource.Store;
using System;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;

namespace ElCarro.Web.Controllers
{
    [Authorize(Roles = Constants.CompanyRole)]
    public class StoresController : CustomController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Stores
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Manage");
        }

        // GET: Stores/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = await db.Stores.FindAsync(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            if (string.IsNullOrEmpty(store.Logo))
                store.Logo = "static/user.png";

            store.PhoneNumber = FormatPhoneNumber(store.PhoneNumber);

            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
                TempData["SuccessMessage"] = null;
            }
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
                TempData["ErrorMessage"] = ViewBag.SuccessMessage = TempData["SuccessMessage"] = null;
            }

            return View(store);
        }

        // GET: Stores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Store store)
        {
            if (ModelState.IsValid)
            {
                store.Logo = SavePhoto(store.LogoFile);
                store.PhoneNumber = OnlyNumbers(store.PhoneNumber);
                string userId = GetUserId();
                store.Company = await db.Company.SingleAsync(c => c.Admin.Id.Equals(userId));
                store = db.Stores.Add(store);

                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Manage");
            }

            return View(store);
        }

        // GET: Stores/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Store store = await db.Stores.FindAsync(id);
            store.PhoneNumber = FormatPhoneNumber(store.PhoneNumber);

            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Store model)
        {
            if (ModelState.IsValid)
            {
                model.PhoneNumber = OnlyNumbers(model.PhoneNumber);
                string actualPhotoPath = model.Logo;
                if (null != model.LogoFile)
                {
                    model.Logo = SavePhoto(model.LogoFile);
                    DeletePhoto(actualPhotoPath);
                }

                db.Entry(model).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Stores", new { id = model.StoreID });
            }
            return View(model);
        }

        // GET: Stores/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = await db.Stores.FindAsync(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Store store = await db.Stores.FindAsync(id);
            string urlImage = store.Logo;
            db.Stores.Remove(store);
            await db.SaveChangesAsync();
            DeletePhoto(urlImage);
            return RedirectToAction("Index", "Manage");
        }

        /// <summary>
        /// this method recive an excel file for save new parts for an store
        /// </summary>
        /// <param name="UploadExcel"></param>
        /// <param name="StoreID"></param>
        /// <param name="Email"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddExcelFile(HttpPostedFileBase UploadExcel, int StoreID)
        {
            InsertVehParts(UploadExcel, StoreID);
            // redirect to the detail page of the store
            return RedirectToAction("Details", "Stores", new { id = StoreID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InsertVehParts(HttpPostedFileBase file, int storeId)
        {
            if (null == file)
                return;

            try
            {
                Pos_Data_Col pdc = new Pos_Data_Col();
                bool validate = false;
                IWorkbook hssfwb;
                ISheet sheet;
                ICellStyle styleSuccess;
                ICellStyle styleError;
                List<string> cols = new List<string>();
                cols = pdc.GetAllKeys();

                // Get the type of file uploaded
                var contenttype = file.ContentType.ToLowerInvariant();
                // Get the extension fo the file
                var pathFile = Server.MapPath(file.FileName);
                // Get the anme of the file
                var fileName = Path.GetFileName(pathFile);
                // get the extension of the file
                string fileExt = Path.GetExtension(fileName);

                // validate the file, checking the type of file
                if (contenttype.Contains("excel") || contenttype.Contains("spreadsheet"))
                {
                    // Validate the extension of the file
                    if (fileExt.Equals(".xls"))
                    {
                        // Create the workbook to handle the sheet
                        hssfwb = new HSSFWorkbook(file.InputStream);
                        styleSuccess = (HSSFCellStyle)hssfwb.CreateCellStyle();
                        styleError = (HSSFCellStyle)hssfwb.CreateCellStyle();
                        validate = true;
                    }
                    else if (fileExt.Equals(".xlsx"))
                    {
                        // Create the workbook to handle the sheet
                        hssfwb = new XSSFWorkbook(file.InputStream);
                        styleSuccess = (XSSFCellStyle)hssfwb.CreateCellStyle();
                        styleError = (XSSFCellStyle)hssfwb.CreateCellStyle();
                        validate = true;
                    }
                    else
                    {
                        // set the error message for incorrect file format
                        TempData["ErrorMessage"] = Store_SR.format_error_excel;
                        return;
                    }

                    styleSuccess.FillForegroundColor = IndexedColors.LightGreen.Index;
                    styleSuccess.FillPattern = FillPattern.SolidForeground;
                    styleError.FillForegroundColor = IndexedColors.Red.Index;
                    styleError.FillPattern = FillPattern.SolidForeground;

                    if (validate)
                    {
                        // iterate all sheets to get the sheet with the column that we need
                        for (int c = 0; c < hssfwb.NumberOfSheets; c++)
                        {
                            // get the sheet at position 'c'
                            sheet = hssfwb.GetSheetAt(c);
                            pdc = new Pos_Data_Col();
                            // get the first row to validate the name of the columns
                            IRow headerRow = sheet.GetRow(0);
                            int cellCount = headerRow.LastCellNum;
                            // iterate all columns to see the name of each one
                            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                            {
                                // get the column at position 'i'
                                var cell = headerRow.GetCell(i);
                                if (null != cell)
                                {
                                    // get the value of the column
                                    string value = headerRow.GetCell(i).StringCellValue;
                                    value = FormatString(value);
                                    // if the value of the column is a valid Name set the position
                                    if (pdc.columns.ContainsKey(value))
                                    {
                                        pdc.columns[value] = i;
                                        cols.Remove(value);
                                    }
                                }
                            }

                            // If I find the sheet with the columns begin to save the data
                            if (pdc.IsValid())
                            {
                                // search the store that is saving the list of objects
                                var _store = db.Stores.FirstOrDefault(s => s.StoreID == storeId);
                                // get the numbers of row that the sheet has
                                int rowCount = sheet.LastRowNum;

                                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                                {
                                    // get the specific row
                                    IRow row = sheet.GetRow(i);
                                    row.RowStyle = styleError;
                                    foreach (var cell in row.Cells)
                                        cell.CellStyle = styleError;

                                    try
                                    {
                                        // get the model of the vehicle
                                        var _model = HelperMethods.FormatNameString(GetValueFromRow(row, pdc.columns["modelo_vehiculo"]));
                                        // get the make of the vehicle
                                        var _make = HelperMethods.FormatNameString(GetValueFromRow(row, pdc.columns["marca_vehiculo"]));
                                        // get the name of the part
                                        var _part = HelperMethods.FormatNameString(GetValueFromRow(row, pdc.columns["nombre"]));
                                        // get the year of the model of the vehicle
                                        var _year = Convert.ToInt32(GetValueFromRow(row, pdc.columns["año_vehiculo"]));
                                        // get the price of the part
                                        var _price = Convert.ToDouble(GetValueFromRow(row, pdc.columns["precio"]));

                                        if (!string.IsNullOrWhiteSpace(_model) && !string.IsNullOrWhiteSpace(_make)
                                            && !string.IsNullOrWhiteSpace(_part))
                                        {
                                            // try to get the make but I create one
                                            Make make = db.Makes.FirstOrDefault(
                                                p => p.Name.Equals(_make, StringComparison.CurrentCultureIgnoreCase));

                                            if (null == make)
                                            {
                                                make = db.Makes.Add(new Make()
                                                {
                                                    Name = _make
                                                });
                                            }

                                            // try to get the model but I create one
                                            Model model = db.Models.FirstOrDefault(
                                                p => p.Name.Equals(_model, StringComparison.CurrentCultureIgnoreCase)
                                                && p.Make.Id == make.Id);

                                            if (null == model && null != make)
                                            {
                                                model = db.Models.Add(new Model()
                                                {
                                                    Name = _model,
                                                    Make = make
                                                });
                                            }

                                            // try to get the part but I create one
                                            VehiclePart part = db.VehiclePart.FirstOrDefault(
                                                p => p.Name.Equals(_part, StringComparison.CurrentCultureIgnoreCase)
                                                && p.Model.Id == model.Id && p.Year == _year);

                                            if (null == part && null != model && null != make)
                                            {
                                                db.VehiclePart.Add(new VehiclePart()
                                                {
                                                    Name = _part,
                                                    Price = _price,
                                                    Year = _year,
                                                    LastView = DateTime.Now,
                                                    Popularity = 0,
                                                    Model = model,
                                                    Store = _store
                                                });

                                                // Save the list of objects
                                                db.SaveChanges();

                                                row.RowStyle = styleSuccess;
                                                foreach (var cell in row.Cells)
                                                    cell.CellStyle = styleSuccess;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        row.RowStyle = styleError;
                                        foreach (var cell in row.Cells)
                                            cell.CellStyle = styleError;
                                    }
                                }

                                bool result = false;
                                using (MemoryStream memoryStream = new MemoryStream())
                                {
                                    hssfwb.Write(memoryStream);

                                    memoryStream.Seek(0, SeekOrigin.Begin);
                                    ContentType contentType = new ContentType();
                                    contentType.MediaType = contenttype;
                                    contentType.Name = file.FileName;

                                    EmailSender.EmailSender email = new EmailSender.EmailSender()
                                    {
                                        toAddress = new MailAddress(_store.Email),
                                        text = "Este correo tiene el archivo excel que usted subio con el resultado del proceso." +
                                        "\nLas filas con el fondo de color rojo no se pudieron guardar." +
                                        "\nLas filas con el fondo de color verde se guardaron exitosamente.",
                                        subject = Store_SR.subject_excel_upload,
                                        attachFile = new Attachment(memoryStream, contentType)
                                    };
                                    result = email.SendEmailFromElCarro();
                                }

                                if (result)
                                {
                                    // set the success message
                                    TempData["SuccessMessage"] = Store_SR.success_upload_excel + " " + _store.Email;
                                }
                                else
                                {
                                    // set the success message
                                    TempData["SuccessMessage"] = Store_SR.success_upload_excel_without_email;
                                }

                                return;
                            }
                        }
                    }

                    // set the error message for incorrect file format
                    TempData["ErrorMessage"] = Store_SR.columns_are_not_valid + " "
                        + string.Join(", ", cols.ToArray());
                    // the file do not contain column with the name required
                    return;
                }
                else
                {
                    // set the error message for incorrect file format
                    TempData["ErrorMessage"] = Store_SR.format_error_excel;
                    return;
                }
            }
            catch (Exception ex)
            {
                // set the error message for incorrect functionality in the application
                TempData["ErrorMessage"] = Store_SR.error_upload_excel;
            }
        }

        /// <summary>
        /// this metho get the value of an specific cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        private string GetValueFromRow(IRow row, int cell)
        {
            // validate the row and the number of the cell
            if (null == row && cell < 0)
                return string.Empty;

            try
            {
                // try to get the value of the cell
                if (row.GetCell(cell) != null)
                    return row.GetCell(cell).ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

            return string.Empty;
        }

        /// <summary>
        /// this method format a string from something like this " Marca Pieza " to "marca_pieza"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string FormatString(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            // Eliminate the space at the End and Start f the string
            value = value.Trim();
            // Convert the string to lower case
            value = value.ToLowerInvariant();
            // Change all white space with "_"
            string pattern = "\\s+";
            string replacement = "_";
            Regex rgx = new Regex(pattern);
            value = rgx.Replace(value, replacement);

            return value;
        }
    }

    public class Pos_Data_Col
    {
        public Dictionary<string, int> columns { get; set; }

        public Pos_Data_Col()
        {
            columns = new Dictionary<string, int>();
            columns.Add("nombre", -1);
            columns.Add("precio", -1);
            columns.Add("marca_vehiculo", -1);
            columns.Add("modelo_vehiculo", -1);
            columns.Add("año_vehiculo", -1);
        }

        public List<string> GetAllKeys()
        {
            List<string> keys = new List<string>();
            foreach (var item in columns)
                keys.Add(item.Key);
            return keys;
        }

        /// <summary>
        /// this method validate that the object is complete for get the needed data form the file
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            // validate if the sheet is more than -1 and
            // there are no columns with position equal to -1
            foreach (var item in columns)
            {
                if (item.Value == -1)
                    return false;
            }

            return true;
        }

        // Indexer
        public int this[string key]
        {
            get { return columns[key]; }
            set { columns[key] = value; }
        }
    }
}
