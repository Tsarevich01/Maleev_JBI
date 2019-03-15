using SnackBarServiceDAL.BindingModel;
using SnackBarServiceDAL.Interfaces;
using SnackBarServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace SnackBarView
{
    public partial class FormSnacks : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly ISnackService service;

        private int? id;

        private List<SnackProductViewModel> jbiSostavs;

        public FormSnacks(ISnackService service)
        {
            this.service = service;
            InitializeComponent();
        }

        private void FormSnacks_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    SnackViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.SnackName;
                        textBoxPrice.Text = view.Price.ToString();
                        jbiSostavs = view.SnackProducts;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                jbiSostavs = new List<SnackProductViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (jbiSostavs != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = jbiSostavs;
                    dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        jbiSostavs.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormSnack>();
                form.Model = jbiSostavs[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    jbiSostavs[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormSnack>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.SnackId = id.Value;
                    }
                    jbiSostavs.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (jbiSostavs == null || jbiSostavs.Count == 0)
            {
                MessageBox.Show("Заполните блюда", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<SnackProductBindingModel> jbiSostav = new List<SnackProductBindingModel>();
                for (int i = 0; i < jbiSostavs.Count; ++i)
                {
                    jbiSostav.Add(new SnackProductBindingModel
                    {
                        Id = jbiSostavs[i].Id,
                        SnackId = jbiSostavs[i].SnackId,
                        ProductId = jbiSostavs[i].ProductId,
                        Count = jbiSostavs[i].Count
                    });
                }
                if (id.HasValue)
                {
                    service.UpdElement(new SnackBindingModel
                    {
                        Id = id.Value,
                        SnackName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        SnackProduct = jbiSostav
                    });
                }
                else
                {
                    service.AddElement(new SnackBindingModel
                    {
                        SnackName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        SnackProduct = jbiSostav
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
