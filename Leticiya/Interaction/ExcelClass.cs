using System;
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
            Excel.Range _excelCells;
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

            worksheet.Range["D3"].Value = "НАКЛАДНАЯ";
            worksheet.Range["E3"].Value = $"№{Id_Order} от {DateTime.Now.ToString("dd:MM:yyyy")} г.";

            worksheet.Range["B5"].Value = "Отправитель:";
            worksheet.Range["D5"].Value = "";

            worksheet.Range["B7"].Value = "Получатель:";
            worksheet.Range["D7"].Value = "";

            worksheet.Range["B11"].Value = "№";
            worksheet.Range["B12"].Value = "п/п";

            _excelCells = (Excel.Range)worksheet.get_Range("C11", "F11").Cells;
            _excelCells.Merge(Type.Missing);
            _excelCells = (Excel.Range)worksheet.get_Range("G11", "H11").Cells;
            _excelCells.Merge(Type.Missing);
            _excelCells = (Excel.Range)worksheet.get_Range("G12", "H12").Cells;
            _excelCells.Merge(Type.Missing);

            worksheet.Range["C11"].Value = "Наименование товарно-материальных ценностей";
            worksheet.Range["G11"].Value = "Количество,";
            worksheet.Range["G12"].Value = "шт.";
            worksheet.Range["I11"].Value = "Цена,";
            worksheet.Range["I12"].Value = "руб.";
            worksheet.Range["I11"].Value = "Сумма,";
            worksheet.Range["I12"].Value = "руб.";

            int n = 4 + 4;
            for (int i = 0; i < n; i++)
            {
                worksheet.Range[$"B{13 + i}"].Value = i + 1;
                _excelCells = (Excel.Range)worksheet.get_Range($"C{12 + i}", $"F{12 + i}").Cells;
                _excelCells.Merge(Type.Missing);
                _excelCells = (Excel.Range)worksheet.get_Range($"G{12 + i}", $"H{12 + i}").Cells;
                _excelCells.Merge(Type.Missing);
            }

            _excelCells = (Excel.Range)worksheet.get_Range($"C{12 + n}", $"F{12 + n}").Cells;
            _excelCells.Merge(Type.Missing);

            worksheet.Range[$"C{13 + n}"].Value = "Итого: ";






            worksheet.Columns.AutoFit();

            string path = saveFileDialogExp.FileName;
            workbook.SaveAs(path);
            workbook.Close();

            Marshal.ReleaseComObject(application);
        }
    }
}