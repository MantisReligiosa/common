using System;
using System.Windows.Forms;

namespace SmartTechnologiesM.Activation
{
    public partial class KeygenForm : Form
    {
        private readonly string _key;
        private readonly string _iv;

        public KeygenForm(string key, string iv)
        {
            InitializeComponent();
            _key = key;
            _iv = iv;
        }

        private void PasteFromClipboardHandler(object sender, EventArgs e)
        {
            var iData = Clipboard.GetDataObject();
            if (iData.GetDataPresent(DataFormats.Text))
            {
                var text = (string)iData.GetData(DataFormats.Text);
                serialNumberInputControl1.SerialNumber = text;
            }
        }

        private void CopyToClipboardHandler(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(serialNumberInputControl2.SerialNumber);
        }

        private void GenerateActivationCodeHahdler(object sender, EventArgs e)
        {
            var licenseManager = new ActivationManager(_key, _iv, null, null, null);
            var requestCode = serialNumberInputControl1.SerialNumber;
            var data = DateTime.Now;
            if (radioButton1.Checked)
                data = data.AddDays((int)numericUpDown1.Value);
            if (radioButton2.Checked)
                data = data.AddMonths((int)numericUpDown2.Value);
            if (radioButton3.Checked)
                data = data.AddYears((int)numericUpDown3.Value);
            var licenseInfo = new LicenseInfo
            {
                ExpirationDate = data,
                RequestCode = requestCode
            };
            serialNumberInputControl2.SerialNumber = licenseManager.GetActivationKey(licenseInfo);
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = radioButton1.Checked;
            numericUpDown2.Enabled = radioButton2.Checked;
            numericUpDown3.Enabled = radioButton3.Checked;
        }
    }
}
