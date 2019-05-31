using SnackBarServiceDAL.BindingModel;
using SnackBarServiceDAL.Interfaces;
using SnackBarServiceDAL.ViewModel;
using System;
using System.Windows.Forms;

namespace SnackBarView
{
    public partial class FormProduct : Form
    {
        public int Id { set { id = value; } }

        private readonly IProductService service;

        private int? id;

        public FormProduct()
        {
            InitializeComponent();
        }

        private void FormProduct_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ProductViewModel view = APIClient.GetRequest<ProductViewModel>("api/Product/Get/" + id.Value);
                    if (view != null)
                    {
                        textBoxProductName.Text = view.НазваниеПродукта;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxProductName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    APIClient.PostRequest<ProductBindingModel, bool>("api/Product/UpdElement", new ProductBindingModel
                   {
                       Id = id.Value,
                       ProductName = textBoxProductName.Text
                   });
                }
                else
                {
                    APIClient.PostRequest<ProductBindingModel,
                   bool>("api/Product/AddElement", new ProductBindingModel
                   {
                       ProductName = textBoxProductName.Text
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
