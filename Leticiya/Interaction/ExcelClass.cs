using System;
using System.Collections.Generic;
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
            Excel.Range excelCells = null;

            List<string> dataOrder = servicesUser.DataOrder(Id_Order);
            List<string>[] dataOrderProduct = servicesUser.DataOrderProduct(Id_Order);
            int n = dataOrderProduct.Length;

            StyleDocyment(worksheet, excelCells, n);

            worksheet.Range["E3"].Value = $"НАКЛАДНАЯ №{Id_Order} от {DateTime.Now.ToString("dd:MM:yyyy")} г.";
            worksheet.Range["E5"].Value = $"ИП Утин Сергей Николаевич ";
            worksheet.Range["E7"].Value = $"{dataOrder[0]} {dataOrder[1]}" +
                $" {dataOrder[2]} {dataOrder[3]}";

            for (int i = 0; i < n; i++)
            {
                worksheet.Range[$"C{13 + i}"].Value = $"{dataOrderProduct[i][0]} {dataOrderProduct[i][1]}";
                worksheet.Range[$"G{13 + i}"].Value = dataOrderProduct[i][2];
                worksheet.Range[$"I{13 + i}"].Value = Convert.ToDouble(dataOrderProduct[i][3]);
                worksheet.Range[$"J{13 + i}"].Formula = $"=SUM(G{13 + i}*I{13 + i})";
            }

            worksheet.Range[$"J{13 + n + 4}"].Value = Convert.ToDouble(dataOrder[4]);

            worksheet.Columns.AutoFit();

            string path = saveFileDialogExp.FileName;
            workbook.SaveAs(path);
            workbook.Close();

            Marshal.ReleaseComObject(application);
        }

        private void StyleDocyment(Excel.Worksheet worksheet, Excel.Range excelCells, int n)
        {
            worksheet.Cells.Font.Name = "Arial Cyr";
            worksheet.Cells.Font.Size = 10;
            worksheet.Cells.Interior.Color = System.Drawing.Color.White;

            worksheet.Range["E3"].Font.Bold = true;
            worksheet.Range["C5"].Value = "Отправитель:";
            worksheet.Range["C7"].Value = "Получатель:";
            worksheet.Range["B11"].Value = "№";
            worksheet.Range["B12"].Value = "п/п";

            excelCells = (Excel.Range)worksheet.get_Range("C11", "F11").Cells;
            excelCells.Merge(Type.Missing);
            excelCells = (Excel.Range)worksheet.get_Range("G11", "H11").Cells;
            excelCells.Merge(Type.Missing);
            excelCells = (Excel.Range)worksheet.get_Range("G12", "H12").Cells;
            excelCells.Merge(Type.Missing);

            worksheet.Range["C11"].Value = "Наименование товарно-материальных ценностей";
            worksheet.Range["G11"].Value = "Количество,";
            worksheet.Range["G12"].Value = "шт.";
            worksheet.Range["I11"].Value = "Цена,";
            worksheet.Range["I12"].Value = "руб.";
            worksheet.Range["J11"].Value = "Сумма,";
            worksheet.Range["J12"].Value = "руб.";


            for (int i = 0; i < n + 4; i++)
            {
                worksheet.Range[$"B{13 + i}"].Value = i + 1;
                excelCells = (Excel.Range)worksheet.get_Range($"C{12 + i}", $"F{12 + i}").Cells;
                excelCells.Merge(Type.Missing);
                excelCells = (Excel.Range)worksheet.get_Range($"G{12 + i}", $"H{12 + i}").Cells;
                excelCells.Merge(Type.Missing);
            }
            excelCells = (Excel.Range)worksheet.get_Range($"C{12 + n + 4}", $"F{12 + n + 4}").Cells;
            excelCells.Merge(Type.Missing);
            excelCells = (Excel.Range)worksheet.get_Range($"G{12 + n + 4}", $"H{12 + n + 4}").Cells;
            excelCells.Merge(Type.Missing);
            excelCells = (Excel.Range)worksheet.get_Range($"C{13 + n + 4}", $"F{13 + n + 4}").Cells;
            excelCells.Merge(Type.Missing);
            excelCells = (Excel.Range)worksheet.get_Range($"G{13 + n + 4}", $"H{13 + n + 4}").Cells;
            excelCells.Merge(Type.Missing);

            worksheet.Range[$"C{13 + n + 4}"].Value = $"Итого:";
            worksheet.Range[$"G{13 + n + 4}"].Formula = $"=SUM(G{13}:G{13 + n})";

            worksheet.Range[$"C{13 + n + 7}"].Value = "Сдал";
            worksheet.Range[$"C{13 + n + 9}"].Value = "Принял";
            worksheet.Range[$"E{13 + n + 8}"].Value = "(ФИО)";
            worksheet.Range[$"E{13 + n + 10}"].Value = "(ФИО)";
            worksheet.Range[$"E{13 + n + 8}"].Font.Size = 8;
            worksheet.Range[$"E{13 + n + 10}"].Font.Size = 8;

            worksheet.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            worksheet.Range["E3"].Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            worksheet.Range["E5"].Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            worksheet.Range["E7"].Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            worksheet.Range["C5"].Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            worksheet.Range["C7"].Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            worksheet.Range[$"C{13 + n + 4}"].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            for (int i = 0; i < n + 4; i++)
                worksheet.Range[$"C{13 + i}"].Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            worksheet.Range[$"C{13 + n + 7}"].Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            worksheet.Range[$"C{13 + n + 9}"].Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

            excelCells = (Excel.Range)worksheet.get_Range("D5", "F5");
            Microsoft.Office.Interop.Excel.XlBordersIndex BorderIndex;

            BorderIndex = Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom;
            excelCells.Borders[BorderIndex].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            excelCells.Borders[BorderIndex].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            excelCells.Borders[BorderIndex].ColorIndex = 0;

            excelCells = (Excel.Range)worksheet.get_Range("D7", "F7");

            BorderIndex = Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom;
            excelCells.Borders[BorderIndex].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            excelCells.Borders[BorderIndex].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            excelCells.Borders[BorderIndex].ColorIndex = 0;

            excelCells = (Excel.Range)worksheet.get_Range("B11", "J12");

            BorderIndex = Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft;
            excelCells.Borders[BorderIndex].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            excelCells.Borders[BorderIndex].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            excelCells.Borders[BorderIndex].ColorIndex = 0;

            BorderIndex = Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop;
            excelCells.Borders[BorderIndex].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            excelCells.Borders[BorderIndex].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            excelCells.Borders[BorderIndex].ColorIndex = 0;

            BorderIndex = Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom;
            excelCells.Borders[BorderIndex].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            excelCells.Borders[BorderIndex].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            excelCells.Borders[BorderIndex].ColorIndex = 0;

            BorderIndex = Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight;
            excelCells.Borders[BorderIndex].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            excelCells.Borders[BorderIndex].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            excelCells.Borders[BorderIndex].ColorIndex = 0;

            BorderIndex = Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical;
            excelCells.Borders[BorderIndex].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            excelCells.Borders[BorderIndex].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            excelCells.Borders[BorderIndex].ColorIndex = 0;

            excelCells = (Excel.Range)worksheet.get_Range("B13", $"J{13 + n + 4}");

            BorderIndex = Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft;
            excelCells.Borders[BorderIndex].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            excelCells.Borders[BorderIndex].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            excelCells.Borders[BorderIndex].ColorIndex = 0;

            BorderIndex = Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop;
            excelCells.Borders[BorderIndex].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            excelCells.Borders[BorderIndex].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            excelCells.Borders[BorderIndex].ColorIndex = 0;

            BorderIndex = Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom;
            excelCells.Borders[BorderIndex].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            excelCells.Borders[BorderIndex].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            excelCells.Borders[BorderIndex].ColorIndex = 0;

            BorderIndex = Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight;
            excelCells.Borders[BorderIndex].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            excelCells.Borders[BorderIndex].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            excelCells.Borders[BorderIndex].ColorIndex = 0;

            BorderIndex = Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal;
            excelCells.Borders[BorderIndex].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            excelCells.Borders[BorderIndex].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            excelCells.Borders[BorderIndex].ColorIndex = 0;

            BorderIndex = Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical;
            excelCells.Borders[BorderIndex].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            excelCells.Borders[BorderIndex].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            excelCells.Borders[BorderIndex].ColorIndex = 0;

            excelCells = (Excel.Range)worksheet.get_Range($"D{13 + n + 7}", $"F{13 + n + 7}");

            BorderIndex = Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom;
            excelCells.Borders[BorderIndex].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            excelCells.Borders[BorderIndex].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            excelCells.Borders[BorderIndex].ColorIndex = 0;

            excelCells = (Excel.Range)worksheet.get_Range($"D{13 + n + 9}", $"F{13 + n + 9}");

            BorderIndex = Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom;
            excelCells.Borders[BorderIndex].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            excelCells.Borders[BorderIndex].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            excelCells.Borders[BorderIndex].ColorIndex = 0;

            worksheet.Columns.AutoFit();
        }
    }
}