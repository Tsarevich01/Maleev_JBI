using System.ComponentModel;

namespace SnackBarServiceDAL.ViewModel
{
    public class StockProductViewModel
    {
        public int Id { get; set; }

        public int StockId { get; set; }
        public int ProductId { get; set; }
        [DisplayName("Название компонента")]
        public string НазваниеПродукта { get; set; }
        [DisplayName("Количество")]
        public int Количество { get; set; }
    }
}