using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;


namespace ERP.Model
{
    public class RF_COMMON_FUNCTIONModel
    {        
        public void ExportDataSetToExcel(DataSet ds, string FileNameWithLocation)
        {            
            //Creae an Excel application instance
            Excel.Application excelApp = new Excel.Application();
            
            //Create an Excel workbook instance and open it from the predefined location
            Excel.Workbook excelWorkBook;// = excelApp.Workbooks.Open(@"E:\ord.xlsx");
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            excelWorkBook = excelApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)excelWorkBook.Worksheets.get_Item(1);
            //xlWorkSheet.Cells[1, 1] = "Sheet 1 content";

            
            foreach (DataTable table in ds.Tables)
            {                
                if (table.TableName != "OUTPARAM")
                {
                    //Add a new worksheet to workbook with the Datatable name
                    Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();
                    excelWorkSheet.Name = table.TableName;

                    for (int i = 1; i < table.Columns.Count + 1; i++)
                    {
                        excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                    }

                    for (int j = 0; j < table.Rows.Count; j++)
                    {
                        for (int k = 0; k < table.Columns.Count; k++)
                        {
                            excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                        }
                    }
                }
            }
            
            excelApp.DisplayAlerts = false;
            excelWorkBook.SaveAs(FileNameWithLocation, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing);// ("d:\\csharp-Excel.xlsx");
            excelWorkBook.Close();
            excelApp.Quit();                      
        }

        

    }
}