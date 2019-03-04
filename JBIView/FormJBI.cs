using JBIServiceDAL.Interfaces;
using JBIServiceDAL.ViewModel;
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

namespace JBIView
{
    public partial class FormJBI : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public JbiSostavViewModel Model { set { model = value; } get { return model; } }

        private readonly ISostavService service;

        private JbiSostavViewModel model;

        public FormJBI(ISostavService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormJbiSostav_Load(object sender, EventArgs e)
        {
            try
            {
                List<SostavViewModel> list = service.GetList();
                if (list != null)
                {
                    comboBoxSostav.DisplayMember = "SostavName";
                    comboBoxSostav.ValueMember = "Id";
                    comboBoxSostav.DataSource = list;
                    comboBoxSostav.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBoxSostav.Enabled = false;
                comboBoxSostav.SelectedValue = model.SostavId;
                textBoxCount.Text = model.Count.ToString();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxSostav.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)

                {
                    model = new JbiSostavViewModel
                    {
                        SostavId = Convert.ToInt32(comboBoxSostav.SelectedValue),
                        SostavName = comboBoxSostav.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxCount.Text);
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
