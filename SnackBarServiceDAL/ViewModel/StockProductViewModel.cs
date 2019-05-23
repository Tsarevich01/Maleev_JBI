using System.ComponentModel;
using System.Runtime.Serialization;

namespace SnackBarServiceDAL.ViewModel
{
    [DataContract]
    public class StockProductViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int StockId { get; set; }

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        [DisplayName("Название компонента")]
        public string НазваниеПродукта { get; set; }

        [DataMember]
        [DisplayName("Количество")]
        public int Количество { get; set; }
    }
}