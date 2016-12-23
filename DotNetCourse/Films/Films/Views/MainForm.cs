using Films.Controllers;
using Films.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Films.Views
{
    public partial class MainForm : Form, IFilmsView
    {
        private BindingSource bindingSource;

        public event EventHandler<String[]> DeleteStarted;
        public event EventHandler LoadStarted;
        public event EventHandler<FormLocationWithFilmInfo> EditRequested;
        public event EventHandler<FormLocationInfo> SearchRequested;

        public MainForm()
        {
            InitializeComponent();
            bindingSource = new BindingSource();
            filmsGridView.AllowUserToAddRows = false;
            filmsGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            filmsGridView.RowHeadersVisible = false;
            filmsGridView.BackgroundColor = Color.White;
            filmsGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            filmsGridView.RowTemplate.Height = 150;
        }

        public void InformDataLoaded(DataTable data)
        {
            UpdateGridView(data);
        }

        public void InformUpdateFinished(DataTable data)
        {
            UpdateGridView(data);
        }

        public void InformSearchFinished(DataTable data)
        {
            UpdateGridView(data);
        }

        public void InformDeleteFinished(DataTable data)
        {
            UpdateGridView(data);
        }

        public void DisableMenu()
        {
            fileToolStripMenuItem.Enabled = false;
            editToolStripMenuItem.Enabled = false;
        }

        public void EnableMenu()
        {
            AsyncHelper.InvokeIfRequired(() =>
            {
                fileToolStripMenuItem.Enabled = true;
            }, fileToolStripMenuItem.GetCurrentParent());
            AsyncHelper.InvokeIfRequired(() =>
            {
                editToolStripMenuItem.Enabled = true;
            }, editToolStripMenuItem.GetCurrentParent());

        }   

        private void UpdateGridView(DataTable data)
        {
            AsyncHelper.InvokeIfRequired(() =>
            {
                bindingSource.DataSource = data;
                filmsGridView.DataSource = bindingSource;
                ResizeDataGridView();
            }, filmsGridView);
        }

        private void ResizeDataGridView()
        {
            foreach (DataGridViewColumn column in filmsGridView.Columns)
            {
                if (column is DataGridViewImageColumn)
                {
                    ((DataGridViewImageColumn)column).ImageLayout = DataGridViewImageCellLayout.Zoom;
                }
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }


        private void deleteFilmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArrayList names = new ArrayList();
            foreach (DataGridViewRow row in filmsGridView.SelectedRows)
            {
                names.Add(row.Cells[ColumnsNameHelper.Name].Value);
                DialogResult confirmResult =  MessageBox.Show(String.Format("{0} {1}?", Properties.Resources.ConfirmDeleteText, names[names.Count - 1]),
                                     Properties.Resources.ConfirmDeleteTitle,
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.No)
                {
                    return;
                }
            }

            DeleteStarted(this, names.ToArray(typeof(String)) as String[]);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutForm form = new AboutForm())
            {
                form.ShowDialog();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult confirmResult = MessageBox.Show(Properties.Resources.ConfirmExitText,
                                     Properties.Resources.ConfirmExitTitle,
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void openDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadStarted(this, EventArgs.Empty);
        }

        private void findFilmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchRequested(this, new FormLocationInfo(Location, Width, Height));
        }

        private void editFilmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filmsGridView.SelectedRows.Count != 1)
            {
                DialogResult confirmResult = MessageBox.Show(Properties.Resources.SelectSingleRowText,
                                     Properties.Resources.SelectSingleRowTitle,
                                     MessageBoxButtons.OK);
                return;
            }
            DataGridViewRow row = filmsGridView.SelectedRows[0];
            EditRequested(this, new FormLocationWithFilmInfo(new FormLocationInfo(Location, Width, Height), row.Cells[ColumnsNameHelper.Name].Value as String,
                row.Cells[ColumnsNameHelper.Country].Value as String, row.Cells[ColumnsNameHelper.Year].Value as String));
        }
    }
}
