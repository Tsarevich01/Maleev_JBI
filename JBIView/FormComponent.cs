using JBIServiceDAL.BindingModel;
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
    public partial class FormComponent : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly ISostavService service;

        private int? id;

        public FormComponent(ISostavService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormSostav_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    SostavViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxSostavName.Text = view.SostavName;
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
            if (string.IsNullOrEmpty(textBoxSostavName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new SostavBindingModel
                    {
                        Id = id.Value,
                        SostavName = textBoxSostavName.Text
                    });
                }
                else
                {
                    service.AddElement(new SostavBindingModel
                    {
                        SostavName = textBoxSostavName.Text
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
