namespace SnackBarModel
{
    public class SnackProduct
    {
        public int Id { get; set; }

        public int SnackId { get; set; }

        public int ProductId { get; set; }

        public int Count { get; set; }

        public virtual Product Product { get; set; }

        public virtual Snack Snack { get; set; }
    }
}
