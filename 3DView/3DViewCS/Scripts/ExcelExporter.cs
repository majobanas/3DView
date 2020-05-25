using System;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ThreeDViewCS;

public class ExcelExporter {

    public static void GenerateExcel(List<List<Properties>> pModels, string pFullPath, MFilesObject[] pSelectedObjects) {
        //Console.WriteLine("GenerateExcel: " + pModels.Count);
        System.Data.DataSet dataset = new System.Data.DataSet();
        for (int i = 0; i < pModels.Count; i++) {// (List<Properties> list in pModels) {
            //Console.WriteLine("Add Table");
            dataset.Tables.Add(ConvertToDataTable(pModels[i], i + "list"));
        }

        // create a excel app along side with workbook and worksheet and give a name to it  
        Application excel = new Application();
        excel.Visible = true;
        Workbook workbook = excel.Workbooks.Add();

        // --------------------------------------
        for (int t = 0; t < dataset.Tables.Count; t++) {// (System.Data.DataTable table in dataset.Tables) {
            //Console.WriteLine("Foreach table: " + t);
            //Add a new worksheet to workbook with the Datatable name  
            Worksheet excelWorkSheet = workbook.Sheets.Add();
            //Console.WriteLine("SheetName: " + pSelectedObjects[t].Title);
            excelWorkSheet.Name = TrimSheetName(t + " " + pSelectedObjects[t].Title);
            //Console.WriteLine("Name done");

            // add all the columns  
            for (int i = 1; i < dataset.Tables[t].Columns.Count + 1; i++) {
                excelWorkSheet.Cells[1, i] = dataset.Tables[t].Columns[i - 1].ColumnName;
                excelWorkSheet.Cells[1, i].Font.Bold = true;
                excelWorkSheet.Columns.ColumnWidth = 50;
            }

            // add all the rows  
            for (int j = 0; j < dataset.Tables[t].Rows.Count; j++) {
                for (int k = 0; k < dataset.Tables[t].Columns.Count; k++) {
                    excelWorkSheet.Cells[j + 2, k + 1] = dataset.Tables[t].Rows[j].ItemArray[k].ToString();
                }
            }
        }
        
        /*pFullPath += pFileName + ".xlsx";
        workbook.SaveAs(pFullPath);
        workbook.Close();
        excel.Quit();
        ThreeDViewCS.Processes.Start(pFullPath, false, (s, e) => {
        });*/
    }

    private static System.Data.DataTable ConvertToDataTable(List<Properties> pModels, string pTableName) {
        // creating a data table instance and typed it as our incoming model   
        // as I make it generic, if you want, you can make it the model typed you want.  

        System.Data.DataTable dataTable = new System.Data.DataTable(pTableName);

        //Get all the properties of that model  
        PropertyInfo[] properties = typeof(Properties).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // Loop through all the properties              
        // Adding Column name to our datatable  
        foreach (PropertyInfo property in properties) {
            //Setting column names as Property names    
            dataTable.Columns.Add(property.Name);
        }
        // Adding Row and its value to our dataTable  
        foreach (Properties model in pModels) {
            var values = new object[properties.Length];
            for (int i = 0; i < properties.Length; i++) {
                //inserting property values to datatable rows    
                values[i] = properties[i].GetValue(model, null);
                //Console.WriteLine(values[i]);
            }
            // Finally add value to datatable    
            dataTable.Rows.Add(values);
        }
        return dataTable;
    }

    private static string TrimSheetName(string pSheetName) {
        if (pSheetName.Length < 1)
            pSheetName = "NAME MISSING";
        pSheetName = pSheetName
            .Replace("?", " ")
            .Replace("*", " ")
            .Replace("/", " ")
            .Replace("\"", " ")
            .Replace("[", " ")
            .Replace("]", " ")
            .Replace("_", " ")
            .Replace("-", " ")
            .Replace(":", " ")
            .Replace(".", " ");
        if (pSheetName.Length > 30)
            pSheetName = pSheetName.Substring(0, 27) + "...";
        return pSheetName;
    }

    [DataContract]
    public class Properties {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Value { get; set; }
        public Properties(string pName, string pValue) {
            Name = pName;
            Value = pValue;
        }
    }
}
