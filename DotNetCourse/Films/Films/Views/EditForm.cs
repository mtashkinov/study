using Films.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Films.Views
{
    public partial class EditForm : Form, IEditView
    {
        public event EventHandler<Dictionary<string, string>> EditStarted;
        private String sourceFilmName;

        public EditForm()
        {
            InitializeComponent();
        }

        public void SetFilmInfo(String name, String country, String year)
        {
            sourceFilmName = name;
            editNameTextBox.Text = name;
            editCountryTextBox.Text = country;
            editYearTextBox.Text = year;
        }

        public new void Close()
        {
            AsyncHelper.InvokeIfRequired(() => (this as Form).Close(), this as Form);
        }

        private void OnEditNameTextBoxValidating(object sender, CancelEventArgs e)
        {
            String errorMsg;
            if (!Validator.IsNameValid(editNameTextBox.Text, out errorMsg))
            {
                e.Cancel = true;
                editNameTextBox.Select(0, editNameTextBox.Text.Length);

                errorProvider.SetError(editNameTextBox, errorMsg);
            }
        }

        private void OnEditNameTextBoxValidated(object sender, EventArgs e)
        {
            errorProvider.SetError(editNameTextBox, "");
        }

        private void OnEditCountryTextBoxValidating(object sender, CancelEventArgs e)
        {
            String errorMsg;
            if (!Validator.IsCountryValid(editCountryTextBox.Text, out errorMsg))
            {
                e.Cancel = true;
                editNameTextBox.Select(0, editCountryTextBox.Text.Length);

                errorProvider.SetError(editCountryTextBox, errorMsg);
            }
        }

        private void OnEditCountryTextBoxValidated(object sender, EventArgs e)
        {
            errorProvider.SetError(editCountryTextBox, "");
        }

        private void OnEditYearTextBoxValidating(object sender, CancelEventArgs e)
        {
            String errorMsg;
            if (!Validator.IsYearValid(editYearTextBox.Text, out errorMsg))
            {
                e.Cancel = true;
                editYearTextBox.Select(0, editYearTextBox.Text.Length);

                errorProvider.SetError(editYearTextBox, errorMsg);
            }
        }

        private void OnEditYearTextBoxValidated(object sender, EventArgs e)
        {
            errorProvider.SetError(editYearTextBox, "");
        }

        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            Dictionary<String, String> editData = new Dictionary<String, String>();
            editData.Add(ColumnsNameHelper.SourceName, sourceFilmName);
            editData.Add(ColumnsNameHelper.Name, editNameTextBox.Text);
            editData.Add(ColumnsNameHelper.Country, editCountryTextBox.Text);
            editData.Add(ColumnsNameHelper.Year, editYearTextBox.Text);

            EditStarted(this, editData);
        }
    }
}
