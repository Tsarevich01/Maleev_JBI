using SnackBarServiceDAL.BindingModel;
using SnackBarServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SnackBarView
{
    public partial class FormPutOnStock : Form
    {
        public FormPutOnStock()
        {
            InitializeComponent();
        }
        private void FormPutOnStock_Load(object sender, EventArgs e)
        {
            try
            {
                List<ProductViewModel> listC = APIClient.GetRequest<List<ProductViewModel>>("api/Product/GetList");
                if (listC != null)
                {
                    comboBoxProduct.DisplayMember = "НазваниеПродукта";
                    comboBoxProduct.ValueMember = "Id";
                    comboBoxProduct.DataSource = listC;
                    comboBoxProduct.SelectedItem = null;
                }
                List<StockViewModel> listS = APIClient.GetRequest<List<StockViewModel>>("api/Stock/GetList");
                if (listS != null)
                {
                    comboBoxStock.DisplayMember = "НазваниеСклада";
                    comboBoxStock.ValueMember = "Id";
                    comboBoxStock.DataSource = listS;
                    comboBoxStock.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxProduct.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (comboBoxStock.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                APIClient.PostRequest<StockProductBindingModel, bool>("api/Stock/PutProductOnStore", new StockProductBindingModel
                {
                    ProductId = Convert.ToInt32(comboBoxProduct.SelectedValue),
                    StockId = Convert.ToInt32(comboBoxProduct.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
