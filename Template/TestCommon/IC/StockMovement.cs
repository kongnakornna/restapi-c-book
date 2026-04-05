namespace TestCommon.IC
{
    public class StockMovement
    {
        public string DocuemntID { get; set; }
        public DateTime DocumentDate { get; set; }

        public string SKU { get; set; }

        public string LotNo { get; set; }

        public int QtyIn { get; set; }

        public int QtyOut { get; set; }
    }
}
