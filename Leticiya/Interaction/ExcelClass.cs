using Leticiya.Interaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Leticiya.Interaction
{
    internal class ExcelClass
    {
        private readonly ServicesUser servicesUser = new ServicesUser();
        public static SaveFileDialog saveFileDialogExp = new SaveFileDialog();

        public void ExpExcel(int Id_Order)
        {
            Excel.Application application = new Excel.Application();
            Excel.Workbook workbook = application.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.Sheets[1];

            DataTable dataTable = new DataTable();

            //dataTable = servicesUser.StudentDebtors(Id_Order);
            // Заппрос на вывод данныз для заполнения их в таблиц Excel

            /*SELECT o."ORDER_ID", "CUSTOMER_SURNAME", "CUSTOMER_NAME", "CUSTOMER_PATRONYMIC", "ORDER_PRICE", 
            "CUSTOMER_ORGANIZATION", "ACCOUNTANT_SURNAME", "ACCOUNTANT_NAME", "ACCOUNTANT_PATRONYMIC"
            FROM public."Order" o, public."Customer" cu, public."Accountant" ac
            WHERE o."ORDER_ID" = 1 AND o."CUSTOMER_ID" = cu."CUSTOMER_ID" AND o."ACCOUNTANT_ID" = ac."ACCOUNTANT_ID"

            SELECT "CATEGORY_NAME", "PRODUCT_NAME", "PRODUCT_PRICE", "ORDER_PRODUCT_COUT"
            FROM public."Order" o, public."Product" p, public."Order_Product" op, public."Category" c
            WHERE o."ORDER_ID" = 1 AND o."ORDER_ID" = op."ORDER_ID" AND op."PRODUCT_ID" = p."PRODUCT_ID" AND p."CATEGORY_ID" = c."CATEGORY_ID"
            */

            Excel.Range _excelCells1 = (Excel.Range)worksheet.get_Range("A1", "F1").Cells;
            _excelCells1.Merge(Type.Missing);

            List<string> names = new List<string>();
            List<string> number = new List<string>();
            List<string> debtors = new List<string>();

            foreach (DataRow row in dataTable.Rows)
            {
                names.Add(row["STUDENT_SURNAME"].ToString().Trim() + " " + row["STUDENT_NAME"].ToString().Trim() + " "
                    + row["STUDENT_PATRONUMIC"].ToString().Trim());
                number.Add(row["STUDENT_NUM_RECORD_BOOK"].ToString().Trim());
                debtors.Add("Количество задолжностей: " + row["Количество задолжностей"].ToString().Trim());
            }

            worksheet.Range["A1"].Value = $"Список должников группы {Id_Order} {DateTime.Now}";
            for (int i = 2; i < names.Count + 2; i++)
            {
                Excel.Range _excelCells2 = (Excel.Range)worksheet.get_Range($"B{i + 1}", $"E{i + 1}").Cells;
                _excelCells2.Merge(Type.Missing);
                worksheet.Cells[i + 1, 2].Value = names[i - 2];
                worksheet.Cells[i + 1, 6].Value = number[i - 2];
                worksheet.Cells[i + 1, 7].Value = debtors[i - 2];
            }

            worksheet.Columns.AutoFit();

            string path = saveFileDialogExp.FileName;
            workbook.SaveAs(path);
            workbook.Close();

            Marshal.ReleaseComObject(application);
        }
    }
}