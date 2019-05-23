using SnackBarServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SnackBarView
{
    public partial class FormSnack : Form
    {
        public SnackProductViewModel Model { set { model = value; } get { return model; } }

        private SnackProductViewModel model;

        public FormSnack()
        {
            InitializeComponent();
        }

        private void FormSnackProduct_Load(object sender, EventArgs e)
        {
            try
            {
                List<ProductViewModel> list = APIClient.GetRequest<List<ProductViewModel>>("api/Product/GetList");
                if (list != null)
                {
                    comboBoxProduct.DisplayMember = "НазваниеПродукта";
                    comboBoxProduct.ValueMember = "Id";
                    comboBoxProduct.DataSource = list;
                    comboBoxProduct.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBoxProduct.Enabled = false;
                comboBoxProduct.SelectedValue = model.ProductId;
                textBoxCount.Text = model.Количество.ToString();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxProduct.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)

                {
                    model = new SnackProductViewModel
                    {
                        ProductId = Convert.ToInt32(comboBoxProduct.SelectedValue),
                        НазваниеПродукта = comboBoxProduct.Text,
                        Количество = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Количество = Convert.ToInt32(textBoxCount.Text);
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
