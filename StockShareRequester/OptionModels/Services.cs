namespace StockShareRequester.OptionModels
{
    public class Services
    {
        public BankService BankService { get; set; }
        public PublicShareOwnerControl PublicShareOwnerControl { get; set; }
        public StockTraderBroker StockTraderBroker { get; set; }
        public TobinTaxer TobinTaxer { get; set; }
    }
}
