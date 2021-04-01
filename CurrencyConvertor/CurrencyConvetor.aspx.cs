using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data;

namespace CurrencyConvertor
{
    public partial class CurrencyConvetor : System.Web.UI.Page
    {
        string strLogFileName = "ConversionLog";
        string strCrrencyfilePath = ConfigurationManager.AppSettings["filePath"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblRate.Text = DateTime.Now.ToString();
            //Utility.Logging.writteLog("Test", "DailyLog");
            bindGrid();
        }

        protected void buttonConvert_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                string strCurrencySource = currencyA.SelectedItem.Value.ToString();
                string strCurrencyTO = currencyB.SelectedItem.Value.ToString();
                int iAmount = Convert.ToInt32(currencyAmount.Text);
                float iReceivedAmount= ConvertCurrency(strCurrencySource, strCurrencyTO, iAmount);
                resultAmount.Text = iReceivedAmount.ToString();
            }
        }

        private float ConvertCurrency(string fromCurrency, string toCurrency, int Amount)
        {
            // If currency's are empty abort
            if (fromCurrency == null || toCurrency == null)
                return 0;

            try
            {
                float toRate = GetCurrencyRateTO(toCurrency);

               // return Convert.ToInt32(toRate);
                //float fromRate = GetCurrencyRateFROM(fromCurrency);

                return Amount * toRate;
          
            }
            catch(Exception ex) {
                Utility.Logging.writteLog(ex.Message, strLogFileName);
                return 0; }
        }
        private float SellCurrency(string fromCurrency, string toCurrency, int Amount)
        {
            // If currency's are empty abort
            if (fromCurrency == null || toCurrency == null)
                return 0;

            try
            {
                float fromRate = GetCurrencyRateSell(toCurrency);

                return Amount / fromRate;

            }
            catch (Exception ex)
            {
                Utility.Logging.writteLog(ex.Message, strLogFileName);
                return 0;
            }
        }
        private float GetCurrencyRateTO(string strCurrency)
        {
           
             float iRate=0f;
            if(strCurrency=="HKD")
             iRate= 1.3392f; 
            if(strCurrency=="USD")
                iRate = 0.1738f;

            return iRate;
        }
        private float GetCurrencyRateSell(string strCurrency)
        {
            float iRate = 0f;
            if (strCurrency == "HKD")
                iRate = 0.1698f;
            if (strCurrency == "USD")
                iRate = 1.3574f;

            return iRate;
        }
        private DataTable readCSV()
        {
            DataTable dt= new DataTable();
           string fileName = strCrrencyfilePath + "Currency.csv";

            StreamReader sr = new StreamReader(fileName);
            if (!File.Exists(fileName))
            {
                Utility.Logging.writteLog("Currency File Does not exist " + fileName, strLogFileName);
            }
            else
            {
                while (!sr.EndOfStream)
                {
                    string strLine = sr.ReadLine();
                    if (!String.IsNullOrWhiteSpace(strLine))
                    {
                        string[] values = strLine.Split(',');
                        if (values.Length >= 3)
                        {
                            foreach (string header in values)
                            {
                                dt.Columns.Add(header);
                            }
                            while (!sr.EndOfStream)
                            {
                                string[] rows = sr.ReadLine().Split(',');
                                DataRow dr = dt.NewRow();
                                for (int i = 0; i < values.Length; i++)
                                {
                                    dr[i] = rows[i];
                                }
                                dt.Rows.Add(dr);
                            }
                        }
                    }


                }
            }
            return dt;

        }
        protected void btnSell_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                string strCurrencySource = currencyA.SelectedItem.Value.ToString();
                string strCurrencyTO = currencyB.SelectedItem.Value.ToString();
                int iAmount = Convert.ToInt32(currencyAmount.Text);
                float iReceivedAmount = SellCurrency(strCurrencySource, strCurrencyTO, iAmount);
                resultAmount.Text = iReceivedAmount.ToString();
            }
        }
        private void bindGrid()
        {
            DataTable dtGrid = readCSV();
            if (dtGrid.Rows.Count > 0)
            {
                grdCurrency.DataSource = dtGrid;
                grdCurrency.DataBind();
            }
            else
            {
                Utility.Logging.writteLog("No Value found in datatable", strLogFileName);
            }
        }
    }
}