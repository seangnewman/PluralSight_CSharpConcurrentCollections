namespace SellShirts
{
    public enum TradeType { Sale, Purchase}
    public class Trade
    {
        public  bool IsSale => Type == TradeType.Sale;
        public TShirt Shirt { get; private set; }
        public int Quantity { get; internal set; }
        public SalesPerson Person { get;  private set; }

        public TradeType Type { get; private set; }

        public Trade(SalesPerson person, TShirt shirt, TradeType type, int quantitySold)
        {
            this.Person = person;
            this.Shirt = shirt;
            this.Type = type;
            this.Quantity = quantitySold;
        }

        public override string ToString()
        {
            string typeText = IsSale ? "bought" : "sold";
            return $"{Person} {typeText} {Quantity} {Shirt.Name}";
        }
    }
}