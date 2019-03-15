using System.ComponentModel;

namespace SnackBarServiceDAL.ViewModel
{
    public class StockProductViewModel
    {
        public int Id { get; set; }

        public int StockId { get; set; }
        public int ProductId { get; set; }
        [DisplayName("Название компонента")]
        public string ProductName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}