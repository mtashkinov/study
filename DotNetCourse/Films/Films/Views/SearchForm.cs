using Films.Controllers;
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
    public partial class SearchForm : Form, ISearchView
    {
        public event EventHandler<Dictionary<string, string>> SearchStarted;

        public SearchForm()
        {
            InitializeComponent();
        }

        private void searchNameTextBox_Validating(object sender, CancelEventArgs e)
        {
            String errorMsg;
            if (!Validator.IsNameValidForSearch(searchNameTextBox.Text, out errorMsg))
            {
                e.Cancel = true;
                searchNameTextBox.Select(0, searchNameTextBox.Text.Length);

                errorProvider.SetError(searchNameTextBox, errorMsg);
            }
        }

        private void searchCountryTextBox_Validating(object sender, CancelEventArgs e)
        {
            String errorMsg;
            if (!Validator.IsCountryValidForSearch(searchCountryTextBox.Text, out errorMsg))
            {
                e.Cancel = true;
                searchCountryTextBox.Select(0, searchCountryTextBox.Text.Length);

                errorProvider.SetError(searchCountryTextBox, errorMsg);
            }
        }

        private void searchYearTextBox_Validating(object sender, CancelEventArgs e)
        {
            String errorMsg;
            if (!Validator.IsYearValidForSearch(searchYearTextBox.Text, out errorMsg))
            {
                e.Cancel = true;
                searchYearTextBox.Select(0, searchYearTextBox.Text.Length);

                errorProvider.SetError(searchYearTextBox, errorMsg);
            }
        }

        private void searchDirectorTextBox_Validating(object sender, CancelEventArgs e)
        {
            String errorMsg;
            if (!Validator.IsHumanValidForSearch(searchDirectorTextBox.Text, out errorMsg))
            {
                e.Cancel = true;
                searchDirectorTextBox.Select(0, searchDirectorTextBox.Text.Length);

                errorProvider.SetError(searchDirectorTextBox, errorMsg);
            }
        }

        private void searchActorTextBox_Validating(object sender, CancelEventArgs e)
        {
            String errorMsg;
            if (!Validator.IsHumanValidForSearch(searchActorTextBox.Text, out errorMsg))
            {
                e.Cancel = true;
                searchActorTextBox.Select(0, searchActorTextBox.Text.Length);

                errorProvider.SetError(searchActorTextBox, errorMsg);
            }
        }

        private void searchNameTextBox_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(searchNameTextBox, "");
        }

        private void searchCountryTextBox_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(searchCountryTextBox, "");
        }

        private void searchYearTextBox_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(searchYearTextBox, "");
        }

        private void searchDirectorTextBox_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(searchDirectorTextBox, "");
        }

        private void searchActorTextBox_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(searchActorTextBox, "");
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            searchNameTextBox.Clear();
            searchCountryTextBox.Clear();
            searchYearTextBox.Clear();
            searchDirectorTextBox.Clear();
            searchActorTextBox.Clear();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            Dictionary<String, String> searchData = new Dictionary<String, String>();
            if (searchNameTextBox.Text.Length != 0)
            {
                searchData.Add("Name", searchNameTextBox.Text);
            }

            if (searchCountryTextBox.Text.Length != 0)
            {
                searchData.Add("Country", searchCountryTextBox.Text);
            }

            if (searchYearTextBox.Text.Length != 0)
            {
                searchData.Add("Year", searchYearTextBox.Text);
            }

            if (searchDirectorTextBox.Text.Length != 0)
            {
                searchData.Add("Director", searchDirectorTextBox.Text);
            }

            if (searchActorTextBox.Text.Length != 0)
            {
                searchData.Add("Actor", searchActorTextBox.Text);
            }

            if (searchData.Keys.Count > 0)
            {
                SearchStarted(this, searchData);
            }
        }
    }
}
