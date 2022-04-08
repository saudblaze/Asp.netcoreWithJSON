namespace FACTSERP.Models
{
    public class ERPFacts
    {
        public List<products> products { get; set; }
        public List<stockIns> stockIns { get; set; }
        public List<stockOuts> stockOuts { get; set; }
    }

    //public class products
    //{
    //    public int id  { get; set; }
    //    public string name { get; set; }
    //    public int qty { get; set; }
    //}
    //public class stockIns
    //{
    //    public int id { get; set; }
    //    public int productId { get; set; }
    //    public int qty { get; set; }
    //}
    //public class stockOuts
    //{
    //    public int id { get; set; }
    //    public int productId { get; set; }
    //    public int qty { get; set; }
    //}
}
